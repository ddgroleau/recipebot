using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PBC.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PBC.Server.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        [HttpPost("NewRecipe")]
        public IActionResult PostNewRecipe(RecipeDTO recipeDTO)
        {
            Console.WriteLine($"Recipe controller has received a Recipe! Recipe Title is {recipeDTO.Title}");
            return Ok(recipeDTO);
        }
    }
}
