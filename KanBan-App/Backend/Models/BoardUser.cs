using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("Board_User")]
    public partial class BoardUser
    {
        [Column("PK")]
        public long Pk { get; set; }
        [Column("Board_ID")]
        public long BoardId { get; set; }
        [Required]
        [Column("User_eMail")]
        public string UserEMail { get; set; }

        [ForeignKey("BoardId")]
        [InverseProperty("BoardUser")]
        public virtual Board Board { get; set; }
        [ForeignKey("UserEMail")]
        [InverseProperty("BoardUser")]
        public virtual User UserEMailNavigation { get; set; }
    }
}
