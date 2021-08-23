using PBC.Shared.RecipeComponent;
using PBC.Shared.SubscriptionComponent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PBC.Shared.ListComponent
{

    public class ListService : IListService
    {
        private readonly IListBuilder _listBuilder;
        private readonly IListDayDTO _listDayDTO;
        private readonly IListDTO _listDTO;
        private readonly IListRepository _listRepository;
        private readonly HttpClient _http;
        private readonly ISubscriberState _subscriberState;

        public ListService(IListBuilder listBuilder, HttpClient http, IListDayDTO listDayDTO, IListDTO listDTO, IListRepository listRepository, ISubscriberState subscriberState)
        {
            _listBuilder = listBuilder;
            _listDayDTO = listDayDTO;
            _listDTO = listDTO;
            _listRepository = listRepository;
            _http = http;
            _subscriberState = subscriberState;
        }

        public async Task<IListDayDTO> GenerateDayOfRecipes()
        {
            try
            {
                var userRecipes = await _subscriberState.GetRecipeSubscriptions(123);//Change this once auth is implemented
                return _listBuilder.Build(userRecipes);
            }
            catch (Exception)
            {
                return _listDayDTO;
            }
        }

        public async Task<IRecipeDTO> GenerateRandomRecipeByType(string recipeType)
        {
            IRecipeDTO recipe;
            try
            {
                var userRecipes = await _subscriberState.GetRecipeSubscriptions(123);//Change this once auth is implemented
                recipe = _listBuilder.GenerateRandomRecipeByType(userRecipes, recipeType);
            }
            catch (Exception)
            {
                var userRecipes = new List<RecipeDTO>();
                recipe = _listBuilder.GenerateRandomRecipeByType(userRecipes, recipeType);
            }
            return recipe;
        }
        
        public IListDTO CreateList(IListGeneratorDTO listGeneratorDTO)
        {
            if (ListIsValid(listGeneratorDTO))
            {
                var list = _listBuilder.Build(listGeneratorDTO);
                SaveList(list);
                return list;
            }
            return _listDTO;
        }

        private bool ListIsValid(IListGeneratorDTO listGeneratorDTO)
        {
            bool isValid;
            try
            {
                var validationContext = new ValidationContext(listGeneratorDTO);

                isValid = Validator.TryValidateObject(listGeneratorDTO, validationContext, new List<ValidationResult>(), true);
            }
            catch (Exception)
            {
                throw;
            }
            return isValid;
        }

        private IListDTO SaveList(IListDTO listDTO)
        {
            try
            {
                _listRepository.InsertOne(listDTO);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to Add List to Database.", e);
            }
            return listDTO;
        }

    }
}
