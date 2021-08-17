﻿using PBC.Shared.RecipeComponent;
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

        public ListService(IListBuilder listBuilder, HttpClient http, IListDayDTO listDayDTO, IListDTO listDTO, IListRepository listRepository)
        {
            _listBuilder = listBuilder;
            _listDayDTO = listDayDTO;
            _listDTO = listDTO;
            _listRepository = listRepository;
            _http = http;
        }


        public async Task<IListDayDTO> GenerateDayOfRecipes()
        {
            string userName = "Test"; //Remove this once auth is implemented
            try
            {
                    var userRecipes = await _http.GetFromJsonAsync<List<RecipeDTO>>($"https://localhost:4001/api/Recipe/UserRecipes/{userName}");
                    return _listBuilder.Build(userRecipes);
            }
            catch (Exception)
            {
                return _listDayDTO;
            }
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
