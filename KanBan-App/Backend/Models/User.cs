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
            Note = new HashSet<Note>();
        }

        public long Id { get; set; }
        [Required]
        [Column("eMail")]
        public string EMail { get; set; }
        public string Password { get; set; }

        [InverseProperty("AppointedPerson")]
        public virtual ICollection<Note> Note { get; set; }
    }
}
