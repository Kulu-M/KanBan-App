using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public partial class User
    {
        public User()
        {
            Board = new HashSet<Board>();
            Note = new HashSet<Note>();
        }

        [Column("eMail")]
        public string EMail { get; set; }
        public string Password { get; set; }
        public string VerificationKey { get; set; }

        [InverseProperty("AdminNavigation")]
        public virtual ICollection<Board> Board { get; set; }
        [InverseProperty("AppointedPersonNavigation")]
        public virtual ICollection<Note> Note { get; set; }
    }
}
