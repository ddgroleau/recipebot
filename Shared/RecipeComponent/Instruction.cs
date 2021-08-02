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
        private string RecipeModelId { get; set; }
        public void SetRecipeModelId(string id)
        {
            RecipeModelId = id;
        }

        public int StepNumber { get; set; }
        public string InstructionDescription { get; set; }
    }
}
