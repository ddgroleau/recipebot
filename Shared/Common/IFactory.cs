using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Shared.Common
{
    public interface IFactory<T> where T : class
    {
        public T Make();
    }
}
