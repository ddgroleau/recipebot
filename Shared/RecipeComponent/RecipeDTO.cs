﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBC.Shared.RecipeComponent;

namespace PBC.Shared
{
    public class RecipeDTO : IRecipeDTO
    {
        public int Id { get; set; }
        [Required, Url]
        public string URL { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Ingredients { get; set; } = new List<string>();
        public List<string> Instructions { get; set; } = new List<string>();
        public string NewIngredient { get; set; }
        public string NewInstruction { get; set; }
        public void AddIngredient()
        {
            Ingredients.Add(NewIngredient);
        }
        public void AddInstruction()
        {
            Instructions.Add(NewInstruction);
        }
        public void ResetRecipe()
        {
            URL = "";
            Title = "";
            Description = "";
            Ingredients = new List<string>();
            Instructions = new List<string>();
            NewIngredient = "";
            NewInstruction = "";
        }
    }
}