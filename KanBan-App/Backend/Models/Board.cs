using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public partial class Board
    {
        [Column("ID")]
        public long Id { get; set; }
        public string Admin { get; set; }
        public string Name { get; set; }

        [InverseProperty("Board")]
        public virtual BoardNote BoardNote { get; set; }
        [InverseProperty("Board")]
        public virtual BoardUser BoardUser { get; set; }
        [ForeignKey("Admin")]
        [InverseProperty("Board")]
        public virtual User AdminNavigation { get; set; }
    }
}
