using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public partial class Board
    {
        public Board()
        {
            BoardUser = new HashSet<BoardUser>();
            Note = new HashSet<Note>();
        }

        [Column("ID")]
        public long Id { get; set; }
        public string Admin { get; set; }
        public string Name { get; set; }

        [InverseProperty("Board")]
        public virtual ICollection<BoardUser> BoardUser { get; set; }
        [InverseProperty("Board")]
        public virtual ICollection<Note> Note { get; set; }
        [ForeignKey("Admin")]
        [InverseProperty("Board")]
        public virtual User AdminNavigation { get; set; }
    }
}
