using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.ListComponent
{
    public class ListEntity
    {
        public int ListEntityId { get; set; }
        [Range(1,7)]
        public int Days { get; set; }
        public ICollection<ListDay> ListDays { get; set; } = new List<ListDay>();
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
