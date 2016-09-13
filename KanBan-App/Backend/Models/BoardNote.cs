using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("Board_Note")]
    public partial class BoardNote
    {
        [Column("BoardID")]
        [Key]
        public long BoardId { get; set; }
        [Column("NoteID")]
        public long? NoteId { get; set; }

        [ForeignKey("BoardId")]
        [InverseProperty("BoardNote")]
        public virtual Board Board { get; set; }
        [ForeignKey("NoteId")]
        [InverseProperty("BoardNote")]
        public virtual Note Note { get; set; }
    }
}
