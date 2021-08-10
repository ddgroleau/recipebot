using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.RecipeComponent
{
    public class InstructionFactory : IFactory<Instruction>
    {
        private readonly Instruction _instruction;

        public InstructionFactory(Instruction instruction)
        {
            _instruction = instruction;
        }
        public Instruction Make()
        {
            Instruction newInstruction = _instruction;
            return newInstruction;
        }
    }
}
