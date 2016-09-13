using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("User_Note")]
    public partial class UserNote
    {
        [Column("UserID")]
        [Key]
        public long UserId { get; set; }
        [Column("NoteID")]
        public long? NoteId { get; set; }

        [ForeignKey("NoteId")]
        [InverseProperty("UserNote")]
        public virtual Note Note { get; set; }
    }
}
