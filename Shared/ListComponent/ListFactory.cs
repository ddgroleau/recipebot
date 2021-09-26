using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.ListComponent
{
    public class ListFactory : AbstractListFactory
    {
        public override ListEntity Make()
        {
            return new ListEntity();
        }

        public override ListDay MakeDay()
        {
            return new ListDay();
        }
    }
}
