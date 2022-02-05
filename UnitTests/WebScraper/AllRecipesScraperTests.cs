using Microsoft.Extensions.Logging;
using Recipebot.Shared;
using Recipebot.Shared.RecipeComponent;
using Recipebot.Shared.WebScraper;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTests.WebScraper
{
    public class AllRecipesScraperTests
    {
        [Fact]
        public void ScrapeRecipe_WithValidDinnerRecipe_ShouldReturnRecipeDTO()
        {
            string validRecipeUrl = "https://www.allrecipes.com/recipe/255220/pineapple-sticky-ribs/";
            IRecipeDTO RecipeDTOExpected = new RecipeDTO();

            RecipeDTOExpected.URL = validRecipeUrl;
            RecipeDTOExpected.Title = "Pineapple Sticky Ribs";
            RecipeDTOExpected.Description = "Sticky ribs come out tender and delicious with no pre-boiling " +
                "involved. Just mix up the sauce and bake. Everyone loves them! You can bake right away" +
                " or prepare ahead and let them marinate in this sauce for a while first. Prep Time: " +
                "15 mins. Cook Time: 1 hr 30 mins. Total Time: 1 hr 45 mins. Servings: 6. Yield: 6 servings.";

            RecipeDTOExpected.Ingredients.Add("3 pounds pork back ribs, separated and fat trimmed");
            RecipeDTOExpected.Ingredients.Add("1 (12.5 ounce) can pineapple tidbits, drained");
            RecipeDTOExpected.Ingredients.Add("1 cup apricot jam");
            RecipeDTOExpected.Ingredients.Add("¼ cup soy sauce");
            RecipeDTOExpected.Ingredients.Add("2 tablespoons white wine vinegar");
            RecipeDTOExpected.Ingredients.Add("2 tablespoons brown sugar");
            RecipeDTOExpected.Ingredients.Add("2 cloves garlic, minced");
            RecipeDTOExpected.Ingredients.Add("1 teaspoon ground ginger");
            RecipeDTOExpected.Ingredients.Add("½ teaspoon salt");
            RecipeDTOExpected.Ingredients.Add("½ teaspoon ground black pepper");
            RecipeDTOExpected.Ingredients.Add("¼ teaspoon cayenne pepper");

            RecipeDTOExpected.Instructions.Add("Preheat oven to 350 degrees F (175 degrees C). Arrange ribs in a single layer in a roasting pan.");
            RecipeDTOExpected.Instructions.Add("Stir pineapple, jam, soy sauce, vinegar, brown sugar, garlic, ginger, salt, black pepper, and cayenne pepper together in a bowl; pour over ribs.");
            RecipeDTOExpected.Instructions.Add("Bake in the preheated oven, basting every 15 minutes, until ribs are glazed and pull away easily from the bone, about 1 1/2 hours. An instant-read thermometer inserted into the center should read 145 degrees F (63 degrees C).");

            var allRecipeScraper = new AllRecipesScraper();
            var RecipeDTOActual = allRecipeScraper.ScrapeRecipe(validRecipeUrl, new RecipeDTO());

            Assert.Equal(RecipeDTOExpected.Title, RecipeDTOActual.Title);
            Assert.Equal(RecipeDTOExpected.Description, RecipeDTOActual.Description);
            Assert.Equal(RecipeDTOExpected.Ingredients, RecipeDTOActual.Ingredients);
            Assert.Equal(RecipeDTOExpected.Instructions, RecipeDTOActual.Instructions);
        }

        [Fact]
        public void ScrapeRecipe_WithValidBreakfastRecipe_ShouldReturnRecipeDTO()
        {
            string validRecipeUrl = "https://www.allrecipes.com/recipe/274974/lemon-ricotta-cornmeal-waffles/";
            IRecipeDTO RecipeDTOExpected = new RecipeDTO();

            RecipeDTOExpected.URL = validRecipeUrl;
            RecipeDTOExpected.Title = "Lemon-Ricotta Cornmeal Waffles";
            RecipeDTOExpected.Description = "Slightly dense waffles with a hint of lemon flavor and ricotta cheese for richness. If you like corn muffins," +
                " I think you will like these. Pairs well with fresh berries. Prep Time: " +
                "10 mins. Cook Time: 20 mins. Total Time: 30 mins. Servings: 4. Yield: 4 servings.";

            RecipeDTOExpected.Ingredients.Add("1 cup all-purpose flour");
            RecipeDTOExpected.Ingredients.Add("½ cup cornmeal");
            RecipeDTOExpected.Ingredients.Add("¼ cup white sugar");
            RecipeDTOExpected.Ingredients.Add("1 ½ teaspoons baking powder");
            RecipeDTOExpected.Ingredients.Add("½ teaspoon baking soda");
            RecipeDTOExpected.Ingredients.Add("¼ teaspoon salt");
            RecipeDTOExpected.Ingredients.Add("¾ cup half-and-half");
            RecipeDTOExpected.Ingredients.Add("½ cup ricotta cheese");
            RecipeDTOExpected.Ingredients.Add("2 large eggs");
            RecipeDTOExpected.Ingredients.Add("2 tablespoons melted butter");
            RecipeDTOExpected.Ingredients.Add("1 teaspoon lemon extract");
            RecipeDTOExpected.Ingredients.Add("cooking spray");

            RecipeDTOExpected.Instructions.Add("Preheat a waffle iron according to manufacturer's instructions.");
            RecipeDTOExpected.Instructions.Add("Whisk flour, cornmeal, sugar, baking powder, baking soda, and salt together in a large mixing bowl.");
            RecipeDTOExpected.Instructions.Add("Whisk half-and-half, ricotta cheese, eggs, melted butter, and lemon extract together in a separate bowl until smooth. Pour into the flour mixture and mix until thoroughly combined.");
            RecipeDTOExpected.Instructions.Add("Coat the preheated waffle iron with cooking spray. Pour batter into waffle iron in batches and cook until crisp and golden brown and the iron stops steaming, about 5 minutes.");

            var allRecipeScraper = new AllRecipesScraper();
            var RecipeDTOActual = allRecipeScraper.ScrapeRecipe(validRecipeUrl, new RecipeDTO());

            Assert.Equal(RecipeDTOExpected.Title, RecipeDTOActual.Title);
            Assert.Equal(RecipeDTOExpected.Description, RecipeDTOActual.Description);
            Assert.Equal(RecipeDTOExpected.Ingredients, RecipeDTOActual.Ingredients);
            Assert.Equal(RecipeDTOExpected.Instructions, RecipeDTOActual.Instructions);
        }

        [Fact]
        public void ScrapeRecipe_WithValidAirFryerRecipe_ShouldReturnRecipeDTO()
        {
            string validRecipeUrl = "https://www.allrecipes.com/recipe/260624/air-fried-crumbed-fish/";
            IRecipeDTO RecipeDTOExpected = new RecipeDTO();

            RecipeDTOExpected.URL = validRecipeUrl;
            RecipeDTOExpected.Title = "Air-Fried Crumbed Fish";
            RecipeDTOExpected.Description = "Crumbed fish is one of my favorite fried items, and this air-fried version of the recipe gives me great flavor without the fat. " +
                "Prep Time: 10 mins. Cook Time: 12 mins. Total Time: 22 mins. Servings: 4. Yield: 4 fillets.";

            RecipeDTOExpected.Ingredients.Add("1 cup dry bread crumbs");
            RecipeDTOExpected.Ingredients.Add("¼ cup vegetable oil");
            RecipeDTOExpected.Ingredients.Add("4 flounder fillets");
            RecipeDTOExpected.Ingredients.Add("1 egg, beaten");
            RecipeDTOExpected.Ingredients.Add("1 lemon, sliced");

            RecipeDTOExpected.Instructions.Add("Preheat an air fryer to 350 degrees F (180 degrees C).");
            RecipeDTOExpected.Instructions.Add("Mix bread crumbs and oil together in a bowl. Stir until mixture becomes loose and crumbly.");
            RecipeDTOExpected.Instructions.Add("Dip fish fillets into the egg; shake off any excess. Dip fillets into the bread crumb mixture; coat evenly and fully.");
            RecipeDTOExpected.Instructions.Add("Lay coated fillets gently in the preheated air fryer. Cook until fish flakes easily with a fork, about 12 minutes. Garnish with lemon slices.");

            var allRecipeScraper = new AllRecipesScraper();
            var RecipeDTOActual = allRecipeScraper.ScrapeRecipe(validRecipeUrl, new RecipeDTO());

            Assert.Equal(RecipeDTOExpected.Title, RecipeDTOActual.Title);
            Assert.Equal(RecipeDTOExpected.Description, RecipeDTOActual.Description);
            Assert.Equal(RecipeDTOExpected.Ingredients, RecipeDTOActual.Ingredients);
            Assert.Equal(RecipeDTOExpected.Instructions, RecipeDTOActual.Instructions);
        }

        [Fact]
        public void ScrapeRecipe_WithValidDessertRecipe_ShouldReturnRecipeDTO()
        {
            string validRecipeUrl = "https://www.allrecipes.com/recipe/234410/no-bake-strawberry-cheesecake/";
            IRecipeDTO RecipeDTOExpected = new RecipeDTO();

            RecipeDTOExpected.URL = validRecipeUrl;
            RecipeDTOExpected.Title = "No Bake Strawberry Cheesecake";
            RecipeDTOExpected.Description = "Light and fluffy no bake cheesecake with a strawberry surprise taste. " +
                "Prep Time: 30 mins. Cook Time: 3 hrs 30 mins. Total Time: 4 hrs. Servings: 10. Yield: 1 cheesecake.";

            RecipeDTOExpected.Ingredients.Add("1 (3 ounce) package strawberry-flavored gelatin (such as Jell-O®)");
            RecipeDTOExpected.Ingredients.Add("1 cup boiling water");
            RecipeDTOExpected.Ingredients.Add("1 (8 ounce) package cream cheese, softened");
            RecipeDTOExpected.Ingredients.Add("1 cup white sugar");
            RecipeDTOExpected.Ingredients.Add("1 teaspoon vanilla extract");
            RecipeDTOExpected.Ingredients.Add("1 (5 ounce) can cold evaporated milk");
            RecipeDTOExpected.Ingredients.Add("1 (9 inch) prepared graham cracker crust");

            RecipeDTOExpected.Instructions.Add("Dissolve strawberry gelatin in boiling water in a bowl; cool in refrigerator until thick, but not set, about 20 minutes.");
            RecipeDTOExpected.Instructions.Add("Beat cream cheese, sugar, and vanilla extract together in a bowl until smooth.");
            RecipeDTOExpected.Instructions.Add("Beat evaporated milk in a separate bowl with an electric mixer until whipped and thick. Gradually pour strawberry gelatin mixture into evaporated milk, beating constantly. Fold cream cheese mixture into gelatin-milk mixture to form cheesecake filling.");
            RecipeDTOExpected.Instructions.Add("Set graham cracker crust on a baking sheet or plate to maintain stability. Pour cheesecake filling into crust. Refrigerate until cake is set, at least 3 1/2 hours.");

            var allRecipeScraper = new AllRecipesScraper();
            var RecipeDTOActual = allRecipeScraper.ScrapeRecipe(validRecipeUrl, new RecipeDTO());

            Assert.Equal(RecipeDTOExpected.Title, RecipeDTOActual.Title);
            Assert.Equal(RecipeDTOExpected.Description, RecipeDTOActual.Description);
            Assert.Equal(RecipeDTOExpected.Ingredients, RecipeDTOActual.Ingredients);
            Assert.Equal(RecipeDTOExpected.Instructions, RecipeDTOActual.Instructions);
        }

        [Fact]
        public void ScrapeRecipe_WithPartialButValidURL_ShouldReturnRecipeDTO()
        {
            string validRecipeUrl = "https://www.allrecipes.com/recipe/234410/";
            IRecipeDTO RecipeDTOExpected = new RecipeDTO();

            RecipeDTOExpected.URL = validRecipeUrl;
            RecipeDTOExpected.Title = "No Bake Strawberry Cheesecake";
            RecipeDTOExpected.Description = "Light and fluffy no bake cheesecake with a strawberry surprise taste. " +
                "Prep Time: 30 mins. Cook Time: 3 hrs 30 mins. Total Time: 4 hrs. Servings: 10. Yield: 1 cheesecake.";

            RecipeDTOExpected.Ingredients.Add("1 (3 ounce) package strawberry-flavored gelatin (such as Jell-O®)");
            RecipeDTOExpected.Ingredients.Add("1 cup boiling water");
            RecipeDTOExpected.Ingredients.Add("1 (8 ounce) package cream cheese, softened");
            RecipeDTOExpected.Ingredients.Add("1 cup white sugar");
            RecipeDTOExpected.Ingredients.Add("1 teaspoon vanilla extract");
            RecipeDTOExpected.Ingredients.Add("1 (5 ounce) can cold evaporated milk");
            RecipeDTOExpected.Ingredients.Add("1 (9 inch) prepared graham cracker crust");

            RecipeDTOExpected.Instructions.Add("Dissolve strawberry gelatin in boiling water in a bowl; cool in refrigerator until thick, but not set, about 20 minutes.");
            RecipeDTOExpected.Instructions.Add("Beat cream cheese, sugar, and vanilla extract together in a bowl until smooth.");
            RecipeDTOExpected.Instructions.Add("Beat evaporated milk in a separate bowl with an electric mixer until whipped and thick. Gradually pour strawberry gelatin mixture into evaporated milk, beating constantly. Fold cream cheese mixture into gelatin-milk mixture to form cheesecake filling.");
            RecipeDTOExpected.Instructions.Add("Set graham cracker crust on a baking sheet or plate to maintain stability. Pour cheesecake filling into crust. Refrigerate until cake is set, at least 3 1/2 hours.");

            var allRecipeScraper = new AllRecipesScraper();
            var RecipeDTOActual = allRecipeScraper.ScrapeRecipe(validRecipeUrl, new RecipeDTO());

            Assert.Equal(RecipeDTOExpected.Title, RecipeDTOActual.Title);
            Assert.Equal(RecipeDTOExpected.Description, RecipeDTOActual.Description);
            Assert.Equal(RecipeDTOExpected.Ingredients, RecipeDTOActual.Ingredients);
            Assert.Equal(RecipeDTOExpected.Instructions, RecipeDTOActual.Instructions);
        }

        [Fact]
        public void ScrapeRecipe_WithValidURLAndNoRecipe_ShouldReturnEmptyRecipeDTO()
        {
            string validRecipeUrl = "https://www.allrecipes.com/recipe/";
            var allRecipeScraper = new AllRecipesScraper();

            var RecipeDTOActual = allRecipeScraper.ScrapeRecipe(validRecipeUrl, new RecipeDTO());

            Assert.True(String.IsNullOrEmpty(RecipeDTOActual.Title));
            Assert.True(String.IsNullOrEmpty(RecipeDTOActual.Description));
            Assert.Equal(RecipeDTOActual.Ingredients, new List<string>());
            Assert.Equal(RecipeDTOActual.Instructions, new List<string>());
        }

        [Fact]
        public void ScrapeRecipe_WithInvalidUrl_ShouldReturnEmptyRecipeDTO()
        {
            string validRecipeUrl = "https://www.google.com";
            var allRecipeScraper = new AllRecipesScraper();

            var RecipeDTOActual = allRecipeScraper.ScrapeRecipe(validRecipeUrl, new RecipeDTO());

            Assert.True(String.IsNullOrEmpty(RecipeDTOActual.Title));
            Assert.True(String.IsNullOrEmpty(RecipeDTOActual.Description));
            Assert.Equal(RecipeDTOActual.Ingredients, new List<string>());
            Assert.Equal(RecipeDTOActual.Instructions, new List<string>());
        }

        [Fact]
        public void ScrapeRecipe_WithAlternateUI_ShouldReturnRecipeDTO()
        {
            string validRecipeUrl = "https://www.allrecipes.com/recipe/212400/ginger-ale/";
            IRecipeDTO RecipeDTOExpected = new RecipeDTO();

            RecipeDTOExpected.URL = validRecipeUrl;
            RecipeDTOExpected.Title = "Ginger Ale";
            RecipeDTOExpected.Description = "Homemade soda is a treat that you can feel good about. To make this thirst-quenching low calorie Ginger Ale, " +
                "all it takes is five ingredients and five minutes. And, it only has 4 calories per serving.";

            RecipeDTOExpected.Ingredients.Add("2 liters plain seltzer water");
            RecipeDTOExpected.Ingredients.Add("1 tablespoon pressed fresh ginger juice");
            RecipeDTOExpected.Ingredients.Add("1 fresh squeezed lemon (medium-sized)");
            RecipeDTOExpected.Ingredients.Add("1 teaspoon pure vanilla extract");
            RecipeDTOExpected.Ingredients.Add("5 packets Stevia Extract In The Raw®");

            RecipeDTOExpected.Instructions.Add("Cut small pieces of peeled ginger root and place in a press (like the kind used for garlic). Press ginger root to produce 1 tablespoon of juice and place in small bowl. " +
                "Add strained lemon juice, vanilla extract and Stevia Extract In The Raw. Stir to combine.");
            RecipeDTOExpected.Instructions.Add("Open 2-liter bottle of seltzer and pour out 1/2 cup to make room for soda flavor mixture. Using a funnel, pour mixture into soda bottle and quickly screw on cap.");
            RecipeDTOExpected.Instructions.Add("Careful! The carbonated water will react explosively with the natural ingredients in flavor mixture.");
            RecipeDTOExpected.Instructions.Add("Allow for ingredients to mix with water and for some gas to be released from bottle prior to uncapping - by unscrewing cap just a small amount to hear hissssss. Pour over ice and enjoy!");

            var allRecipeScraper = new AllRecipesScraper();
            var RecipeDTOActual = allRecipeScraper.ScrapeRecipe(validRecipeUrl, new RecipeDTO());

            Assert.Equal(RecipeDTOExpected.Title, RecipeDTOActual.Title);
            Assert.Equal(RecipeDTOExpected.Description, RecipeDTOActual.Description);
            Assert.Equal(RecipeDTOExpected.Ingredients, RecipeDTOActual.Ingredients);
            Assert.Equal(RecipeDTOExpected.Instructions, RecipeDTOActual.Instructions);
        }

        [Fact]
        public void ScrapeRecipe_WithValidUrlAndInvalidRecipe_ShouldReturnEmptyRecipeDTO()
        {
            string InvalidRecipeUrl = "https://www.allrecipes.com/recipe/1100/";
            var allRecipeScraper = new AllRecipesScraper();

            var RecipeDTOActual = allRecipeScraper.ScrapeRecipe(InvalidRecipeUrl, new RecipeDTO());

            Assert.True(String.IsNullOrEmpty(RecipeDTOActual.Title));
            Assert.True(String.IsNullOrEmpty(RecipeDTOActual.Description));
            Assert.Equal(RecipeDTOActual.Ingredients, new List<string>());
            Assert.Equal(RecipeDTOActual.Instructions, new List<string>());
        }

        [Fact]
        public void ScrapeRecipe_WithValidRecipeAndBadSummary_ShouldReturnRecipeDTO()
        {
            string validRecipeUrl = "https://www.allrecipes.com/recipe/12340/watermelon-pie/";
            IRecipeDTO RecipeDTOExpected = new RecipeDTO();

            RecipeDTOExpected.URL = validRecipeUrl;
            RecipeDTOExpected.Title = "Watermelon Pie";
            RecipeDTOExpected.Description = "Just right for the summertime.";

            RecipeDTOExpected.Ingredients.Add("1 (3 ounce) package watermelon flavored Jell-O®");
            RecipeDTOExpected.Ingredients.Add("¼ cup water");
            RecipeDTOExpected.Ingredients.Add("1 (12 ounce) container frozen whipped topping, thawed");
            RecipeDTOExpected.Ingredients.Add("2 cups watermelon");
            RecipeDTOExpected.Ingredients.Add("1 (9 inch) prepared graham cracker crust");

            RecipeDTOExpected.Instructions.Add("Mix together the watermelon gelatin and water. Fold gelatin mixture into the dessert topping. Add cut watermelon.");
            RecipeDTOExpected.Instructions.Add("Pour mixture into graham cracker crust. Cool in refrigerator for about 3 hours.");

            var allRecipeScraper = new AllRecipesScraper();
            var RecipeDTOActual = allRecipeScraper.ScrapeRecipe(validRecipeUrl, new RecipeDTO());

            Assert.Equal(RecipeDTOExpected.Title, RecipeDTOActual.Title);
            Assert.Equal(RecipeDTOExpected.Description, RecipeDTOActual.Description);
            Assert.Equal(RecipeDTOExpected.Ingredients, RecipeDTOActual.Ingredients);
            Assert.Equal(RecipeDTOExpected.Instructions, RecipeDTOActual.Instructions);
        }
    }
}
