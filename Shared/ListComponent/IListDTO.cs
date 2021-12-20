﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Shared.ListComponent
{
   public interface IListDTO
    {
        public int ListEntityId { get; set; }
        public int Days { get; set; }
        public IEnumerable<IListDayDTO> ListDays { get; set; }
    }
}
