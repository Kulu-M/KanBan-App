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
        public async static Task<string> createNewNote(string email, string password)
        {
            using (var client = new HttpClient())
            {
                var adress = "http://localhost:5000/api/board/note/create";

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var note = new Note {Description = "FinalTestNote", Name = "SuperNote", BoardId = 1};
                var json = JsonConvert.SerializeObject(note);

                HttpResponseMessage response =
                    await client.PostAsync(adress, new StringContent(json, Encoding.UTF8, "application/json"));

                return response.ReasonPhrase;
            }
        }
    }
}
