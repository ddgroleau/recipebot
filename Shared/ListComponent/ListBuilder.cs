using Recipebot.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Shared.ListComponent
{
    public class ListBuilder : IListBuilder
    {
        private readonly IListDayDTO _listDayDTO;
        private readonly IRecipeDTO _recipeDTO;
        private readonly IListDTO _listDTO;
        public ListBuilder(IListDayDTO listDayDTO, IRecipeDTO recipeDTO, IListDTO listDTO)
        {
            _listDayDTO = listDayDTO;
            _recipeDTO = recipeDTO;
            _listDTO = listDTO;
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

        public IListDTO Build(IListGeneratorDTO listGeneratorDTO)
        {
            IListDTO listDTO = _listDTO;
            listDTO.Days = listGeneratorDTO.Days;
            foreach(var day in listGeneratorDTO.GeneratedDays)
            {
                var appendedList = listDTO.ListDays.Append(day);
                listDTO.ListDays = appendedList;
            }
            return listDTO;
        }

        public RecipeDTO GenerateRandomRecipeByType(IEnumerable<IRecipeDTO> userRecipes, string recipeType)
        {
            try
            {
                var typeRecipes = userRecipes.Where(x => x.RecipeType.Equals(recipeType));

                int typeCount = Math.Max(typeRecipes.Count() - 1, 0);

                var recipe = typeRecipes.ElementAt(new Random().Next(0, typeCount));

                return (RecipeDTO)recipe;
            }
            catch (Exception)
            {
                return (RecipeDTO)_recipeDTO;
            }
        }
    }
}
