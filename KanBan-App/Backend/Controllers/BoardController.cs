using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    public class BoardController : Controller
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

        // GET api/board/
        [HttpGet("all/")]
        public IQueryable<Board> GetAllBoards()
        {
            IQueryable<Board> results;
            using (var db = new APIAppDbContext())
            {
                results = from boards in db.Board select boards;
                if (!results.Any()) return null;
                return results;
            }
        }

        #endregion GET

        #region POST

        // POST api/board/create/{}-{password}
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

        // DELETE api/user/5
        [HttpDelete("{id}")]
        public void DeleteUserFromDatabase(int id)
        {
        }

        #endregion DELETE
    }
}
