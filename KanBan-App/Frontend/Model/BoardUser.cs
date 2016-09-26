using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frontend
{
    public class BoardUser
    {
        public long BoardId { get; set; }

        public string UserEMail { get; set; }
    }
}
