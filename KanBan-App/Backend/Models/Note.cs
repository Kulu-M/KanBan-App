using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public partial class Note
    {
        public Note()
        {
            BoardNote = new HashSet<BoardNote>();
            UserNote = new HashSet<UserNote>();
        }

        [Column("ID")]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AppointedPerson { get; set; }

        [InverseProperty("Note")]
        public virtual ICollection<BoardNote> BoardNote { get; set; }
        [InverseProperty("Note")]
        public virtual ICollection<UserNote> UserNote { get; set; }
        [ForeignKey("AppointedPerson")]
        [InverseProperty("Note")]
        public virtual User AppointedPersonNavigation { get; set; }
    }
}
