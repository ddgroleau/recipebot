using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.ListComponent
{
    public class ListBuilder : IListBuilder
    {
        private readonly IListDayDTO _listDayDTO;
        private readonly IRecipeDTO _recipeDTO;
        public ListBuilder(IListDayDTO listDayDTO, IRecipeDTO recipeDTO)
        {
            _listDayDTO = listDayDTO;
            _recipeDTO = recipeDTO;
        }

        public IListDayDTO Build(IEnumerable<IRecipeDTO> userRecipes)
        {
            var dayDTO = _listDayDTO;

            dayDTO.Date = DateTime.Today.Date;
            dayDTO.Breakfast = GenerateRandomRecipeByType(userRecipes, "Breakfast");
            dayDTO.Lunch = GenerateRandomRecipeByType(userRecipes, "Lunch");
            dayDTO.Dinner = GenerateRandomRecipeByType(userRecipes, "Dinner");
            
            return _listDayDTO;
        }

        public IRecipeDTO GenerateRandomRecipeByType(IEnumerable<IRecipeDTO> userRecipes, string recipeType)
        {
            try
            {
                var typeRecipes = userRecipes.Where(x => x.RecipeType == recipeType);

                int typeCount = Math.Max(typeRecipes.Count() - 1, 0);

                var recipe = typeRecipes.ElementAt(new Random().Next(0, typeCount));

                return recipe;
            }
            catch (Exception)
            {
                return _recipeDTO;
            }
        }
    }
}
