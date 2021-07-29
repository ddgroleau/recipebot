using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.RecipeComponent
{
    public class InstructionFactory : IFactory<IInstruction>
    {
        public IInstruction Make()
        {
            return new Instruction();
        }
    }
}
