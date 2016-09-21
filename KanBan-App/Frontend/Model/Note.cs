using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend
{
    public class Note
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AppointedPerson { get; set; }
        public long? BoardId { get; set; }
    }
}
