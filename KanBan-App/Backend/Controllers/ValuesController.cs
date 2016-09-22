using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Remotion.Linq.Clauses;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        #region EXAMPLES

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2", "value3" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return id.ToString();
        }

        // GET api/values/getuser/-userMail-
        [HttpGet("getuser/{eMail}")]
        public string GetDatabaseUser(string eMail)
        {
            User resultUser;
            using (var db = new APIAppDbContext())
            {
                resultUser = db.User.First(u => u.EMail == eMail);
            }
            return resultUser.Password;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        #endregion EXAMPLES

        #region GET

        // GET api/values/login/{eMail}-{password}
        [HttpGet("login/{eMail}-{password}")]
        public string UserLogin(string eMail, string password)
        {
            var resultUser = new User();
            using (var db = new APIAppDbContext())
            {
                resultUser = db.User.FirstOrDefault(u => u.EMail == eMail);
            }
            if (resultUser == null) return "User not registered!";

            if (resultUser.Password.Equals(password))
            {
                return "You are now logged in!";
            }
            return "Wrong password!";
        }

        #endregion GET

        #region POST

        // POST api/values/register/{eMail}-{password}
        [HttpPost("register/{eMail}-{password}")]
        public string RegisterNewUser(string eMail, string password)
        {
            var existingUser = new User();
            using (var db = new APIAppDbContext())
            {
                existingUser = db.User.FirstOrDefault(u => u.EMail == eMail);
            }
            if (existingUser != null) return "User already exists!";
            using (var db = new APIAppDbContext())
            {
                var newUser = new User
                {
                    EMail = eMail,
                    Password = password
                };
                db.User.Add(newUser);
                db.SaveChanges();
            }
            return "You are now registered!";
        }

        #endregion POST

        #region PUT

        #endregion PUT

        #region DELETE

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void DeleteUserFromDatabase(int id)
        {
        }

        #endregion DELETE
    }
}
