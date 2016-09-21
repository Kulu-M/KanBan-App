using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Frontend
{
    public class MyWebRequests
    {
        public async static Task<bool> requestIsBoardAdmin(string email, string boardId)
        {
            return true;
        }

        public async static Task<string> requestLogin(string email, string password)
        {
            string url = "http://localhost:5000/api/user"; //email pw
            var test = JsonConvert.SerializeObject(new { email = email, password = password });

            HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(url);

            webrequest.Method = "GET";
            webrequest.ContentType = "application/json";

            using (var stream = new StreamWriter(await webrequest.GetRequestStreamAsync()))
            {
                stream.Write(test);
            }
            return "fail";
        }

        public async static Task<string> requestRegister(string email, string password)
        {
            string url = "http://localhost:5000/api/user/register/" + email + "-" + password;

            HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(url);
            webrequest.Method = "POST";
            HttpWebResponse response = (HttpWebResponse)await webrequest.GetResponseAsync();
            StreamReader streamReader1 = new StreamReader(response.GetResponseStream());

            return streamReader1.ReadToEnd();
        }
    }
}
