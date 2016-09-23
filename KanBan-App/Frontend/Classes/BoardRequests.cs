using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Frontend
{
    public class BoardRequests
    {
        public static async Task<List<Board>> createNewBoard(string email, string password, string boardName)
        {
            var board = new Board
            {
                Admin = email,
                Name = boardName
            };
            var json = JsonConvert.SerializeObject(board);

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("http://localhost:5000/api/board/create"),
                    Method = HttpMethod.Post,
                };

                request.Headers.Add("username", email);
                request.Headers.Add("pw", password);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.SendAsync(request);

                var i = await response.Content.ReadAsStringAsync();

                string s = i.Substring(i.IndexOf("Result = ") + 1);

                return JsonConvert.DeserializeObject<List<Board>>(s);
            }
        }

        public async static Task<List<Note>> createNewNote(string email, string password, long? boardId)
        {
            var note = new Note { Description = "", Name = "Bitte Namen eingeben", BoardId = boardId };
            var json = JsonConvert.SerializeObject(note);

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("http://localhost:5000/api/board/note/create"),
                    Method = HttpMethod.Post,
                };

                request.Headers.Add("username", email);
                request.Headers.Add("pw", password);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.SendAsync(request);

                var i = await response.Content.ReadAsStringAsync();

                string s = i.Substring(i.IndexOf("Result = ") + 1);

                return JsonConvert.DeserializeObject<List<Note>>(s);
            }
        }
    }
}
