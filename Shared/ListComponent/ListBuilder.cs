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
        public ListBuilder(IListDayDTO listDayDTO)
        {
            _listDayDTO = listDayDTO;
        }

        public IListDayDTO Build(IEnumerable<IRecipeDTO> userRecipes)
        {
            var breakfast = GenerateRandomRecipeByType(userRecipes, "Breakfast");
            var lunch = GenerateRandomRecipeByType(userRecipes, "Lunch");
            var dinner = GenerateRandomRecipeByType(userRecipes, "Dinner");
            return _listDayDTO;
        }

        public IRecipeDTO GenerateRandomRecipeByType(IEnumerable<IRecipeDTO> userRecipes, string recipeType)
        {
            var typeRecipes = userRecipes.Where(x => x.RecipeType == recipeType);
            int typeCount = typeRecipes.Count() - 1;
            var recipe = typeRecipes.ElementAt(new Random().Next(0, typeCount));
            return recipe;
        }
    }
}
