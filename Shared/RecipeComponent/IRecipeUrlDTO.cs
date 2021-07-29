using PBC.Shared.DOM_Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.RecipeComponent
{
    public interface IRecipeUrlDTO
    {
        public string RecipeUrlDtoId { get; set; }
        public string URL { get; set; }

        public void ResetURL();
    }
}
