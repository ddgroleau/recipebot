using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Shared.Common
{
    public interface IBuilder<T1>
    {
        public T1 Build(T1 UiLayerDTO);
    }
}
