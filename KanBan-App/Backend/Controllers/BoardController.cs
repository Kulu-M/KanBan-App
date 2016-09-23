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

            if (!User_Authentification.validateUserKey(username, password)) return null;

            var boardList = queryBoardsForUser(username);

            return JsonConvert.SerializeObject(boardList);
        }

        public List<Board> queryBoardsForUser(string userEmail)
        {
            using (var db = new APIAppDbContext())
            {
                var boardList = new List<Board>();
                var resultsNew = from boards in db.BoardUser
                    where boards.UserEMail.Equals(userEmail)
                    select boards.Board;

                foreach (var board in resultsNew)
                {
                    boardList.Add(board);
                }

                if (!boardList.Any()) return null;
                
                return boardList;
            }
        }

        // GET api/board/notes/
        [HttpGet("notes/")]
        public string GetAllNotesFromBoard()
        {
            var username = Request.Headers["username"].ToString();
            var password = Request.Headers["pw"].ToString();
            var boardId = Int64.Parse(Request.Headers["boardId"].ToString());

            if (!User_Authentification.validateUserKey(username, password)) return null;

            return JsonConvert.SerializeObject(getAllNotesByBoardID(boardId));
        }

        #endregion GET

        #region POST

        // POST api/board/create
        [HttpPost("create")]
        public string CreateBoard([FromBody]JObject value)
        {
            var username = Request.Headers["username"].ToString();
            var password = Request.Headers["pw"].ToString();

            if (!User_Authentification.validateUserKey(username, password)) return null;

            var jsonBoard = JsonConvert.DeserializeObject<Board>(value.ToString());

            using (var db = new APIAppDbContext())
            {
                db.Board.Add(jsonBoard);
                db.BoardUser.Add(new BoardUser {BoardId = jsonBoard.Id, UserEMail = username});

                db.SaveChanges();
                
                return JsonConvert.SerializeObject(queryBoardsForUser(username));
            }
        }

        // POST api/board/note/create
        [HttpPost("note/create")]
        public string CreateNoteInBoard([FromBody]JObject value)
        {
            var username = Request.Headers["username"].ToString();
            var password = Request.Headers["pw"].ToString();

            if (!User_Authentification.validateUserKey(username, password)) return null;

            var jsonNote = JsonConvert.DeserializeObject<Note>(value.ToString());

            using (var db = new APIAppDbContext())
            {
                db.Note.Add(jsonNote);
                db.SaveChanges();
                return JsonConvert.SerializeObject(getAllNotesByBoardID(jsonNote.BoardId));
            }
        }
        
        public List<Note> getAllNotesByBoardID(long? boardId)
        {
            var noteList = new List<Note>();
            using (var db = new APIAppDbContext())
            {
                noteList.AddRange(db.Note.Where(note => note.BoardId == boardId));
                return noteList;
            }
        }

        // POST api/board/users/
        [HttpPost("users/")]
        public string GetAllUsersFromBoard([FromBody]JObject value)
        {
            var username = value.SelectToken("user").SelectToken("email").ToString();
            var password = value.SelectToken("user").SelectToken("pw").ToString();
            var boardId = Int64.Parse(value.SelectToken("content").SelectToken("boardId").ToString());

            if (!User_Authentification.validateUserKey(username, password)) return null;

            var userEmailList = new List<string>();
            using (var db = new APIAppDbContext())
            {
                userEmailList = (from userIds in db.BoardUser where userIds.BoardId == boardId select userIds.UserEMail).ToList();
            }

            return JsonConvert.SerializeObject(userEmailList);
        }

        // POST api/board/user/add
        [HttpPost("user/add")]
        public string AddUserToBoard([FromBody]JObject value)
        {
            var username = Request.Headers["username"].ToString();
            var password = Request.Headers["pw"].ToString();

            if (!User_Authentification.validateUserKey(username, password)) return null;

            var jsonBoardUser = JsonConvert.DeserializeObject<BoardUser>(value.ToString());

            using (var db = new APIAppDbContext())
            {
                var existingUser = from users in db.User where users.EMail == jsonBoardUser.UserEMail select users;

                if (!existingUser.Any()) return "User does not exists";

                var existingBoardUser = from search in db.BoardUser
                    where search.UserEMail == jsonBoardUser.UserEMail && search.BoardId == jsonBoardUser.BoardId
                    select search;

                if (existingBoardUser.Any()) return "User already added to Board";

                db.BoardUser.Add(jsonBoardUser);
                db.SaveChanges();
            }
            return "User added to Board";
        }

        #endregion POST

        #region PUT

        #endregion PUT

        #region DELETE

        // DELETE api/board/user/remove
        [HttpDelete("note/delete")]
        public string deleteNote([FromBody]JObject value)
        {
            var username = Request.Headers["username"].ToString();
            var password = Request.Headers["pw"].ToString();

            if (!User_Authentification.validateUserKey(username, password)) return null;

            var jsonNote = JsonConvert.DeserializeObject<Note>(value.ToString());

            using (var db = new APIAppDbContext())
            {
                db.Note.Remove(jsonNote);
                db.SaveChanges();
            }
            return JsonConvert.SerializeObject(getAllNotesByBoardID(jsonNote.BoardId));
        }

        // DELETE api/board/user/remove
        [HttpDelete("user/remove")]
        public string removeUserFromBoard([FromBody]JObject value)
        {
            var username = Request.Headers["username"].ToString();
            var password = Request.Headers["pw"].ToString();

            if (!User_Authentification.validateUserKey(username, password)) return null;

            var jsonBoardUser = JsonConvert.DeserializeObject<BoardUser>(value.ToString());

            using (var db = new APIAppDbContext())
            {
                var existingUser = from users in db.User where users.EMail == jsonBoardUser.UserEMail select users;

                if (!existingUser.Any()) return "User does not exists";

                var existingBoardUser = (from search in db.BoardUser
                                        where search.UserEMail == jsonBoardUser.UserEMail && search.BoardId == jsonBoardUser.BoardId
                                        select search).First();

                if (existingBoardUser == null) return "User has no access to Board";

                db.BoardUser.Remove(existingBoardUser);
                db.SaveChanges();
            }
            return "User removed from Board";
        }

        #endregion DELETE
    }
}
