using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Authentification;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
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

        // GET api/user/count/
        [HttpGet("count")]
        public string GetUserCount()
        {
            using (var db = new APIAppDbContext())
            {
                return db.User.Count().ToString();
            }
        }

        // GET api/user/all/
        [HttpGet("all")]
        public string GetAllUsers()
        {
            List<User> userList = new List<User>();
            using (var db = new APIAppDbContext())
            {
                foreach (var user in db.User)
                {
                    user.Password = "";
                    userList.Add(user);
                }
            }
            return JsonConvert.SerializeObject(userList);
        }

        #endregion GET

        #region POST

        // POST api/user/login
        [HttpPost("login/")]
        public string UserLogin([FromBody]JObject value)
        {
            var username = value.SelectToken("email").ToString();
            var password = value.SelectToken("pw").ToString();
  
            var resultUser = new User();
            using (var db = new APIAppDbContext())
            {
                resultUser = db.User.FirstOrDefault(u => u.EMail == username);
            }
            if (resultUser == null) return "User not registered!";

            //TODO Hash/Salt password
            if (resultUser.Password.Equals(password))
            {
                return User_Authentification.generateUserKey(username);
            }
            return "Wrong password!";
        }

        // POST api/user/register/
        [HttpPost("register/")]
        public string RegisterNewUser([FromBody]JObject value)
        {
            var username = value.First.Last.ToString();
            var password = value.Last.Last.ToString();

            var existingUser = new User();
            using (var db = new APIAppDbContext())
            {
                existingUser = db.User.FirstOrDefault(u => u.EMail == username);
            }
            if (existingUser != null) return "User already exists!";

            //TODO Hash/Salt password
            using (var db = new APIAppDbContext())
            {
                var newUser = new User
                {
                    EMail = username,
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

        // DELETE api/user/delete/
        [HttpDelete("delete/")]
        public string DeleteUser([FromBody]JObject value)
        {
            //TODO delete foreign keys first

            var username = value.First.Last.ToString();
            var password = value.Last.Last.ToString();

            try
            {
                var existingUser = new User();
                using (var db = new APIAppDbContext())
                {
                    //delete from user table
                    existingUser = db.User.FirstOrDefault(u => u.EMail == username);
                    if (existingUser == null) return "Error!";
                    db.User.Remove(existingUser);
                    db.SaveChanges();
                    return "Success!";
                }
            }
            catch (Exception e )
            {
                throw e;
                return "Error!";
            }
        }
       
        #endregion DELETE
    }
}
