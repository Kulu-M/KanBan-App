using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public partial class Note
    {
        [Column("ID")]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long? AppointedPersonId { get; set; }

        [ForeignKey("AppointedPersonId")]
        [InverseProperty("Note")]
        public virtual User AppointedPerson { get; set; }
    }
}
