using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.ListComponent
{
    public class List
    {
        public int ListId { get; set; }
        [Range(1,7)]
        public int Days { get; set; }
        public ICollection<ListDay> ListDays { get; set; }
    }
}
