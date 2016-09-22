using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Frontend
{
    public class UserRequests
    {
        public async static Task<string> registerNewUser(string email, string password)
        {
            using (var client = new HttpClient())
            {
                var adress = "http://localhost:5000/api/user/register/";
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var User = new User {EMail = email, Password = password};
                var jsonUser = JsonConvert.SerializeObject(User);

                HttpResponseMessage response =
                    await client.PostAsync(adress, new StringContent(jsonUser, Encoding.UTF8, "application/json"));

                return response.ReasonPhrase;
            }
        }
    }
}
