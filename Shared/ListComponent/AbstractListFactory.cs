using PBC.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.ListComponent
{
    public abstract class AbstractListFactory : IFactory<ListEntity>
    {
        public abstract ListEntity Make();

        public abstract ListDay MakeDay();
    
    }
}
