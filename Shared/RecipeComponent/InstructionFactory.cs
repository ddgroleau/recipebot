using Recipebot.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Shared.RecipeComponent
{
    public class InstructionFactory : IFactory<Instruction>
    {
  
        public Instruction Make()
        {
            return new Instruction();
        }
    }
}
