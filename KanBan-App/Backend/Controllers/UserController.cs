using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Authentification;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
                    userList.Add(user);
                }
            }
            return JsonConvert.SerializeObject(userList);
        }

        // GET api/user/login/{eMail}-{password}
        [HttpGet("login/{eMail}-{password}")]
        public string UserLogin(string eMail, string password)
        {
            var resultUser = new User();
            using (var db = new APIAppDbContext())
            {
                resultUser = db.User.FirstOrDefault(u => u.EMail == eMail);
            }
            if (resultUser == null) return "User not registered!";

            //TODO Hash/Salt password
            if (resultUser.Password.Equals(password))
            {
                return User_Authentification.generateUserKey(eMail);
            }
            return "Wrong password!";
        }

        #endregion GET

        #region POST

        // POST api/user/register/{eMail}-{password}
        [HttpPost("register/{eMail}-{password}")]
        public string RegisterNewUser(string eMail, string password)
        {
            var existingUser = new User();
            using (var db = new APIAppDbContext())
            {
                existingUser = db.User.FirstOrDefault(u => u.EMail == eMail);
            }
            if (existingUser != null) return "User already exists!";

            //TODO Hash/Salt password
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

        // GET api/user/delete/{eMail}-{password}
        [HttpGet("delete/{eMail}-{password}")]
        public string DeleteUser(string eMail, string password)
        {
            try
            {
                var existingUser = new User();
                using (var db = new APIAppDbContext())
                {
                    //delete from 


                    //delete from user table
                    existingUser = db.User.FirstOrDefault(u => u.EMail == eMail);
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
