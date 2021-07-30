using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.RecipeComponent
{
    public interface IRepository<T> where T : class
     {
        public void InsertOne(Object record);
    }
}
