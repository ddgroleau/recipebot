using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Shared.Common
{
    public interface IBuilder<T1,T2>
    {
        public T1 Build(T2 UiLayerDTO);
        public T2 Build(T1 ApplicationLayerDTO);
    }
}
