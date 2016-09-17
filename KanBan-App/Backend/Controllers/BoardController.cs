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

        // GET api/board/all/{eMail}-{key}
        //[HttpGet("all/{eMail}-{key}")]
        //public string GetAllBoardsForSingleUser(string eMail, string key)
        //{
        //    //if (!User_Authentification.validateUserKey(eMail, key)) return null;

        //    IQueryable<BoardUser> results;
        //    var boardList = new List<BoardUser>();
        //    using (var db = new APIAppDbContext())
        //    {
        //        results = from boards in db.BoardUser where boards.UserId.Equals(eMail) select boards;
        //        foreach (var board in results)
        //        {
        //             boardList.Add(board);
        //        }
        //        if (!boardList.Any()) return null;

        //        var json = JsonConvert.SerializeObject(new
        //        {
        //            operations = boardList
        //        });
        //        return json;
        //    }
        //}

        // GET api/board/notes/{boardId}/{eMail}-{key}
        [HttpGet("notes/{boardId}/{eMail}-{key}")]
        public string GetAllNotesFromBoard(long boardId, string eMail, string key)
        {
            if (!User_Authentification.validateUserKey(eMail, key)) return null;

            List<Note> noteList = new List<Note>();

            using (var db = new APIAppDbContext())
            {
                noteList.AddRange(db.Note.Where(note => note.BoardId == boardId));

                return JsonConvert.SerializeObject(noteList);
            }
        }

        // GET api/board/users/{boardId}/{eMail}-{key}
        [HttpGet("users/{boardId}/{eMail}-{key}")]
        public string GetAllUsersFromBoard(long boardId, string eMail, string key)
        {
            if (!User_Authentification.validateUserKey(eMail, key)) return null;

            var userEmailList = new List<string>();
            using (var db = new APIAppDbContext())
            {
                userEmailList = (from userIds in db.BoardUser where userIds.BoardId == boardId select userIds.UserEMail).ToList();
            }

            return JsonConvert.SerializeObject(userEmailList);
        }

        #endregion GET

        #region POST

        #endregion POST

        #region PUT

        #endregion PUT

        #region DELETE

        #endregion DELETE
    }
}
