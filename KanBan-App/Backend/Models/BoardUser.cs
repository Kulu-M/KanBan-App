using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("Board_User")]
    public partial class BoardUser
    {
        [Column("BoardID")]
        [Key]
        public long BoardId { get; set; }
        [Column("UserID")]
        public long? UserId { get; set; }

        [ForeignKey("BoardId")]
        [InverseProperty("BoardUser")]
        public virtual Board Board { get; set; }
    }
}
