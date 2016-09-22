using System;
using System.Collections.Generic;
using System.Linq;
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

        // GET api/board/all/
        [HttpGet("all/")]
        public string GetAllBoardsForSingleUser()
        {
            var username = Request.Headers["username"].ToString();
            var password = Request.Headers["pw"].ToString();

            //if (!User_Authentification.validateUserKey(username, key)) return null;

            IQueryable<string> results;
            var boardList = new List<string>();
            using (var db = new APIAppDbContext())
            {
                results = from boards in db.BoardUser where boards.UserEMail.Equals(username) select boards.Board.Name;
                foreach (var board in results)
                {
                    boardList.Add(board);
                }
                if (!boardList.Any()) return null;

                var json = JsonConvert.SerializeObject(new
                {
                    Boards = boardList
                });
                return json;
            }
        }

        #endregion GET

        #region POST

        // POST api/board/notes/
        [HttpPost("notes/")]
        public string GetAllNotesFromBoard([FromBody]JObject value)
        {
            var username = value.SelectToken("user").SelectToken("email").ToString();
            var password = value.SelectToken("user").SelectToken("pw").ToString();
            var boardId = Int64.Parse(value.SelectToken("content").SelectToken("boardId").ToString());

            //if (!User_Authentification.validateUserKey(eMail, key)) return null;

            var noteList = new List<Note>();

            using (var db = new APIAppDbContext())
            {
                noteList.AddRange(db.Note.Where(note => note.BoardId == boardId));
                return JsonConvert.SerializeObject(noteList);
            }
        }

        // POST api/board/users/
        [HttpPost("users/")]
        public string GetAllUsersFromBoard([FromBody]JObject value)
        {
            var username = value.SelectToken("user").SelectToken("email").ToString();
            var password = value.SelectToken("user").SelectToken("pw").ToString();
            var boardId = Int64.Parse(value.SelectToken("content").SelectToken("boardId").ToString());

            //if (!User_Authentification.validateUserKey(username, password)) return null;

            var userEmailList = new List<string>();
            using (var db = new APIAppDbContext())
            {
                userEmailList = (from userIds in db.BoardUser where userIds.BoardId == boardId select userIds.UserEMail).ToList();
            }

            return JsonConvert.SerializeObject(userEmailList);
        }

        #endregion POST

        #region PUT

        #endregion PUT

        #region DELETE

        #endregion DELETE
    }
}
