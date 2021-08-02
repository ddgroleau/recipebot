using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.RecipeComponent
{
    public interface IInstruction
    {
        public string InstructionId { get; set; }
        public void SetRecipeModelId(string id);
        public int StepNumber { get; set; }
        public string InstructionDescription { get; set; }
    }
}
