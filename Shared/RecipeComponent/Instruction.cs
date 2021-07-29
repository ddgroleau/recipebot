using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.RecipeComponent
{
    public class Instruction : IInstruction
    {
        public string InstructionId { get; set; } 
        public string RecipeModelId { get; set; }
        public int StepNumber { get; set; }
        public string InstructionDescription { get; set; }
    }
}
