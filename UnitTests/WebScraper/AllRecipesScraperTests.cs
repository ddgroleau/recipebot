using PBC.Shared;
using PBC.Shared.RecipeComponent;
using PBC.Shared.WebScraper;
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
            IRecipeDTO recipeDTOExpected = new RecipeDTO();

            recipeDTOExpected.URL = validRecipeUrl;
            recipeDTOExpected.Title = "Pineapple Sticky Ribs";
            recipeDTOExpected.Description = "Sticky ribs come out tender and delicious with no pre-boiling " +
                "involved. Just mix up the sauce and bake. Everyone loves them! You can bake right away" +
                " or prepare ahead and let them marinate in this sauce for a while first. Prep Time: " +
                "15 mins. Cook Time: 1 hr 30 mins. Total Time: 1 hr 45 mins. Servings: 6. Yield: 6 servings.";

            recipeDTOExpected.Ingredients.Add("3 pounds pork back ribs, separated and fat trimmed");
            recipeDTOExpected.Ingredients.Add("1 (12.5 ounce) can pineapple tidbits, drained");
            recipeDTOExpected.Ingredients.Add("1 cup apricot jam");
            recipeDTOExpected.Ingredients.Add("¼ cup soy sauce");
            recipeDTOExpected.Ingredients.Add("2 tablespoons white wine vinegar");
            recipeDTOExpected.Ingredients.Add("2 tablespoons brown sugar");
            recipeDTOExpected.Ingredients.Add("2 cloves garlic, minced");
            recipeDTOExpected.Ingredients.Add("1 teaspoon ground ginger");
            recipeDTOExpected.Ingredients.Add("½ teaspoon salt");
            recipeDTOExpected.Ingredients.Add("½ teaspoon ground black pepper");
            recipeDTOExpected.Ingredients.Add("¼ teaspoon cayenne pepper");

            recipeDTOExpected.Instructions.Add("Preheat oven to 350 degrees F (175 degrees C). Arrange ribs in a single layer in a roasting pan.");
            recipeDTOExpected.Instructions.Add("Stir pineapple, jam, soy sauce, vinegar, brown sugar, garlic, ginger, salt, black pepper, and cayenne pepper together in a bowl; pour over ribs.");
            recipeDTOExpected.Instructions.Add("Bake in the preheated oven, basting every 15 minutes, until ribs are glazed and pull away easily from the bone, about 1 1/2 hours. An instant-read thermometer inserted into the center should read 145 degrees F (63 degrees C).");

            var allRecipeScraper = new AllRecipesScraper();
            var recipeDTOActual = allRecipeScraper.ScrapeRecipe(validRecipeUrl, new RecipeDTO());

            Assert.Equal(recipeDTOExpected.Title, recipeDTOActual.Title);
            Assert.Equal(recipeDTOExpected.Description, recipeDTOActual.Description);
            Assert.Equal(recipeDTOExpected.Ingredients, recipeDTOActual.Ingredients);
            Assert.Equal(recipeDTOExpected.Instructions, recipeDTOActual.Instructions);
        } 
        
        [Fact]
        public void ScrapeRecipe_WithValidBreakfastRecipe_ShouldReturnRecipeDTO()
        {
            string validRecipeUrl = "https://www.allrecipes.com/recipe/274974/lemon-ricotta-cornmeal-waffles/";
            IRecipeDTO recipeDTOExpected = new RecipeDTO();

            recipeDTOExpected.URL = validRecipeUrl;
            recipeDTOExpected.Title = "Lemon-Ricotta Cornmeal Waffles";
            recipeDTOExpected.Description = "Slightly dense waffles with a hint of lemon flavor and ricotta cheese for richness. If you like corn muffins," +
                " I think you will like these. Pairs well with fresh berries. Prep Time: " +
                "10 mins. Cook Time: 20 mins. Total Time: 30 mins. Servings: 4. Yield: 4 servings.";

            recipeDTOExpected.Ingredients.Add("1 cup all-purpose flour");
            recipeDTOExpected.Ingredients.Add("½ cup cornmeal");
            recipeDTOExpected.Ingredients.Add("¼ cup white sugar");
            recipeDTOExpected.Ingredients.Add("1 ½ teaspoons baking powder");
            recipeDTOExpected.Ingredients.Add("½ teaspoon baking soda");
            recipeDTOExpected.Ingredients.Add("¼ teaspoon salt");
            recipeDTOExpected.Ingredients.Add("¾ cup half-and-half");
            recipeDTOExpected.Ingredients.Add("½ cup ricotta cheese");
            recipeDTOExpected.Ingredients.Add("2 large eggs");
            recipeDTOExpected.Ingredients.Add("2 tablespoons melted butter");
            recipeDTOExpected.Ingredients.Add("1 teaspoon lemon extract");
            recipeDTOExpected.Ingredients.Add("cooking spray");

            recipeDTOExpected.Instructions.Add("Preheat a waffle iron according to manufacturer's instructions.");
            recipeDTOExpected.Instructions.Add("Whisk flour, cornmeal, sugar, baking powder, baking soda, and salt together in a large mixing bowl.");
            recipeDTOExpected.Instructions.Add("Whisk half-and-half, ricotta cheese, eggs, melted butter, and lemon extract together in a separate bowl until smooth. Pour into the flour mixture and mix until thoroughly combined.");
            recipeDTOExpected.Instructions.Add("Coat the preheated waffle iron with cooking spray. Pour batter into waffle iron in batches and cook until crisp and golden brown and the iron stops steaming, about 5 minutes.");

            var allRecipeScraper = new AllRecipesScraper();
            var recipeDTOActual = allRecipeScraper.ScrapeRecipe(validRecipeUrl, new RecipeDTO());

            Assert.Equal(recipeDTOExpected.Title, recipeDTOActual.Title);
            Assert.Equal(recipeDTOExpected.Description, recipeDTOActual.Description);
            Assert.Equal(recipeDTOExpected.Ingredients, recipeDTOActual.Ingredients);
            Assert.Equal(recipeDTOExpected.Instructions, recipeDTOActual.Instructions);
        }
        [Fact]
        public void ScrapeRecipe_WithValidAirFryerRecipe_ShouldReturnRecipeDTO()
        {
            string validRecipeUrl = "https://www.allrecipes.com/recipe/260624/air-fried-crumbed-fish/";
            IRecipeDTO recipeDTOExpected = new RecipeDTO();

            recipeDTOExpected.URL = validRecipeUrl;
            recipeDTOExpected.Title = "Air-Fried Crumbed Fish";
            recipeDTOExpected.Description = "Crumbed fish is one of my favorite fried items, and this air-fried version of the recipe gives me great flavor without the fat. " +
                "Prep Time: 10 mins. Cook Time: 12 mins. Total Time: 22 mins. Servings: 4. Yield: 4 fillets.";

            recipeDTOExpected.Ingredients.Add("1 cup dry bread crumbs");
            recipeDTOExpected.Ingredients.Add("¼ cup vegetable oil");
            recipeDTOExpected.Ingredients.Add("4 flounder fillets");
            recipeDTOExpected.Ingredients.Add("1 egg, beaten");
            recipeDTOExpected.Ingredients.Add("1 lemon, sliced");

            recipeDTOExpected.Instructions.Add("Preheat an air fryer to 350 degrees F (180 degrees C).");
            recipeDTOExpected.Instructions.Add("Mix bread crumbs and oil together in a bowl. Stir until mixture becomes loose and crumbly.");
            recipeDTOExpected.Instructions.Add("Dip fish fillets into the egg; shake off any excess. Dip fillets into the bread crumb mixture; coat evenly and fully.");
            recipeDTOExpected.Instructions.Add("Lay coated fillets gently in the preheated air fryer. Cook until fish flakes easily with a fork, about 12 minutes. Garnish with lemon slices.");

            var allRecipeScraper = new AllRecipesScraper();
            var recipeDTOActual = allRecipeScraper.ScrapeRecipe(validRecipeUrl, new RecipeDTO());

            Assert.Equal(recipeDTOExpected.Title, recipeDTOActual.Title);
            Assert.Equal(recipeDTOExpected.Description, recipeDTOActual.Description);
            Assert.Equal(recipeDTOExpected.Ingredients, recipeDTOActual.Ingredients);
            Assert.Equal(recipeDTOExpected.Instructions, recipeDTOActual.Instructions);
        }        
        
        [Fact]
        public void ScrapeRecipe_WithValidDessertRecipe_ShouldReturnRecipeDTO()
        {
            string validRecipeUrl = "https://www.allrecipes.com/recipe/234410/no-bake-strawberry-cheesecake/";
            IRecipeDTO recipeDTOExpected = new RecipeDTO();

            recipeDTOExpected.URL = validRecipeUrl;
            recipeDTOExpected.Title = "No Bake Strawberry Cheesecake";
            recipeDTOExpected.Description = "Light and fluffy no bake cheesecake with a strawberry surprise taste. " +
                "Prep Time: 30 mins. Cook Time: 3 hrs 30 mins. Total Time: 4 hrs. Servings: 10. Yield: 1 cheesecake.";

            recipeDTOExpected.Ingredients.Add("1 (3 ounce) package strawberry-flavored gelatin (such as Jell-O®)");
            recipeDTOExpected.Ingredients.Add("1 cup boiling water");
            recipeDTOExpected.Ingredients.Add("1 (8 ounce) package cream cheese, softened");
            recipeDTOExpected.Ingredients.Add("1 cup white sugar");
            recipeDTOExpected.Ingredients.Add("1 teaspoon vanilla extract");
            recipeDTOExpected.Ingredients.Add("1 (5 ounce) can cold evaporated milk");
            recipeDTOExpected.Ingredients.Add("1 (9 inch) prepared graham cracker crust");

            recipeDTOExpected.Instructions.Add("Dissolve strawberry gelatin in boiling water in a bowl; cool in refrigerator until thick, but not set, about 20 minutes.");
            recipeDTOExpected.Instructions.Add("Beat cream cheese, sugar, and vanilla extract together in a bowl until smooth.");
            recipeDTOExpected.Instructions.Add("Beat evaporated milk in a separate bowl with an electric mixer until whipped and thick. Gradually pour strawberry gelatin mixture into evaporated milk, beating constantly. Fold cream cheese mixture into gelatin-milk mixture to form cheesecake filling.");
            recipeDTOExpected.Instructions.Add("Set graham cracker crust on a baking sheet or plate to maintain stability. Pour cheesecake filling into crust. Refrigerate until cake is set, at least 3 1/2 hours.");

            var allRecipeScraper = new AllRecipesScraper();
            var recipeDTOActual = allRecipeScraper.ScrapeRecipe(validRecipeUrl, new RecipeDTO());

            Assert.Equal(recipeDTOExpected.Title, recipeDTOActual.Title);
            Assert.Equal(recipeDTOExpected.Description, recipeDTOActual.Description);
            Assert.Equal(recipeDTOExpected.Ingredients, recipeDTOActual.Ingredients);
            Assert.Equal(recipeDTOExpected.Instructions, recipeDTOActual.Instructions);
        }   
        
        [Fact]
        public void ScrapeRecipe_WithPartialButValidURL_ShouldReturnRecipeDTO()
        {
            string validRecipeUrl = "https://www.allrecipes.com/recipe/234410/";
            IRecipeDTO recipeDTOExpected = new RecipeDTO();

            recipeDTOExpected.URL = validRecipeUrl;
            recipeDTOExpected.Title = "No Bake Strawberry Cheesecake";
            recipeDTOExpected.Description = "Light and fluffy no bake cheesecake with a strawberry surprise taste. " +
                "Prep Time: 30 mins. Cook Time: 3 hrs 30 mins. Total Time: 4 hrs. Servings: 10. Yield: 1 cheesecake.";

            recipeDTOExpected.Ingredients.Add("1 (3 ounce) package strawberry-flavored gelatin (such as Jell-O®)");
            recipeDTOExpected.Ingredients.Add("1 cup boiling water");
            recipeDTOExpected.Ingredients.Add("1 (8 ounce) package cream cheese, softened");
            recipeDTOExpected.Ingredients.Add("1 cup white sugar");
            recipeDTOExpected.Ingredients.Add("1 teaspoon vanilla extract");
            recipeDTOExpected.Ingredients.Add("1 (5 ounce) can cold evaporated milk");
            recipeDTOExpected.Ingredients.Add("1 (9 inch) prepared graham cracker crust");

            recipeDTOExpected.Instructions.Add("Dissolve strawberry gelatin in boiling water in a bowl; cool in refrigerator until thick, but not set, about 20 minutes.");
            recipeDTOExpected.Instructions.Add("Beat cream cheese, sugar, and vanilla extract together in a bowl until smooth.");
            recipeDTOExpected.Instructions.Add("Beat evaporated milk in a separate bowl with an electric mixer until whipped and thick. Gradually pour strawberry gelatin mixture into evaporated milk, beating constantly. Fold cream cheese mixture into gelatin-milk mixture to form cheesecake filling.");
            recipeDTOExpected.Instructions.Add("Set graham cracker crust on a baking sheet or plate to maintain stability. Pour cheesecake filling into crust. Refrigerate until cake is set, at least 3 1/2 hours.");

            var allRecipeScraper = new AllRecipesScraper();
            var recipeDTOActual = allRecipeScraper.ScrapeRecipe(validRecipeUrl, new RecipeDTO());

            Assert.Equal(recipeDTOExpected.Title, recipeDTOActual.Title);
            Assert.Equal(recipeDTOExpected.Description, recipeDTOActual.Description);
            Assert.Equal(recipeDTOExpected.Ingredients, recipeDTOActual.Ingredients);
            Assert.Equal(recipeDTOExpected.Instructions, recipeDTOActual.Instructions);
        }    
        
        [Fact]
        public void ScrapeRecipe_WithValidURLAndNoRecipe_ShouldReturnEmptyRecipeDTO()
        {
            string validRecipeUrl = "https://www.allrecipes.com/recipe/";
            var allRecipeScraper = new AllRecipesScraper();

            var recipeDTOActual = allRecipeScraper.ScrapeRecipe(validRecipeUrl, new RecipeDTO());

            Assert.True(String.IsNullOrEmpty(recipeDTOActual.Title));
            Assert.True(String.IsNullOrEmpty(recipeDTOActual.Description));
            Assert.Equal(recipeDTOActual.Ingredients, new List<string>());
            Assert.Equal(recipeDTOActual.Instructions, new List<string>());
        }     
        
        [Fact]
        public void ScrapeRecipe_WithInvalidUrl_ShouldReturnEmptyRecipeDTO()
        {
            string validRecipeUrl = "https://www.google.com";
            var allRecipeScraper = new AllRecipesScraper();

            var recipeDTOActual = allRecipeScraper.ScrapeRecipe(validRecipeUrl, new RecipeDTO());

            Assert.True(String.IsNullOrEmpty(recipeDTOActual.Title));
            Assert.True(String.IsNullOrEmpty(recipeDTOActual.Description));
            Assert.Equal(recipeDTOActual.Ingredients, new List<string>());
            Assert.Equal(recipeDTOActual.Instructions, new List<string>());
        }

        [Fact]
        public void ScrapeRecipe_WithAlternateUI_ShouldReturnRecipeDTO()
        {
            string validRecipeUrl = "https://www.allrecipes.com/recipe/212400/ginger-ale/";
            IRecipeDTO recipeDTOExpected = new RecipeDTO();

            recipeDTOExpected.URL = validRecipeUrl;
            recipeDTOExpected.Title = "Ginger Ale";
            recipeDTOExpected.Description = "Homemade soda is a treat that you can feel good about. To make this thirst-quenching low calorie Ginger Ale, " +
                "all it takes is five ingredients and five minutes. And, it only has 4 calories per serving.";

            recipeDTOExpected.Ingredients.Add("2 liters plain seltzer water");
            recipeDTOExpected.Ingredients.Add("1 tablespoon pressed fresh ginger juice");
            recipeDTOExpected.Ingredients.Add("1 fresh squeezed lemon (medium-sized)");
            recipeDTOExpected.Ingredients.Add("1 teaspoon pure vanilla extract");
            recipeDTOExpected.Ingredients.Add("5 packets Stevia Extract In The Raw");

            recipeDTOExpected.Instructions.Add("Cut small pieces of peeled ginger root and place in a press (like the kind used for garlic). Press ginger root to produce 1 tablespoon of juice and place in small bowl. " +
                "Add strained lemon juice, vanilla extract and Stevia Extract In The Raw. Stir to combine.");
            recipeDTOExpected.Instructions.Add("Open 2-liter bottle of seltzer and pour out 1/2 cup to make room for soda flavor mixture. Using a funnel, pour mixture into soda bottle and quickly screw on cap.");
            recipeDTOExpected.Instructions.Add("Careful! The carbonated water will react explosively with the natural ingredients in flavor mixture.");
            recipeDTOExpected.Instructions.Add("Allow for ingredients to mix with water and for some gas to be released from bottle prior to uncapping - by unscrewing cap just a small amount to hear hissssss. Pour over ice and enjoy!");

            var allRecipeScraper = new AllRecipesScraper();
            var recipeDTOActual = allRecipeScraper.ScrapeRecipe(validRecipeUrl, new RecipeDTO());

            Assert.Equal(recipeDTOExpected.Title, recipeDTOActual.Title);
            Assert.Equal(recipeDTOExpected.Description, recipeDTOActual.Description);
            Assert.Equal(recipeDTOExpected.Ingredients, recipeDTOActual.Ingredients);
            Assert.Equal(recipeDTOExpected.Instructions, recipeDTOActual.Instructions);
        }

        [Fact]
        public void ScrapeRecipe_WithValidUrlAndInvalidRecipe_ShouldReturnEmptyRecipeDTO()
        {
            string InvalidRecipeUrl = "https://www.allrecipes.com/recipe/1100/";
            var allRecipeScraper = new AllRecipesScraper();

            var recipeDTOActual = allRecipeScraper.ScrapeRecipe(InvalidRecipeUrl, new RecipeDTO());

            Assert.True(String.IsNullOrEmpty(recipeDTOActual.Title));
            Assert.True(String.IsNullOrEmpty(recipeDTOActual.Description));
            Assert.Equal(recipeDTOActual.Ingredients, new List<string>());
            Assert.Equal(recipeDTOActual.Instructions, new List<string>());
        }

        [Fact]
        public void ScrapeRecipe_WithValidRecipeAndBadSummary_ShouldReturnRecipeDTO()
        {
            string validRecipeUrl = "https://www.allrecipes.com/recipe/12340/watermelon-pie/";
            IRecipeDTO recipeDTOExpected = new RecipeDTO();

            recipeDTOExpected.URL = validRecipeUrl;
            recipeDTOExpected.Title = "Watermelon Pie";
            recipeDTOExpected.Description = "Just right for the summertime.";

            recipeDTOExpected.Ingredients.Add("1 (3 ounce) package watermelon flavored Jell-O®");
            recipeDTOExpected.Ingredients.Add("¼ cup water");
            recipeDTOExpected.Ingredients.Add("1 (12 ounce) container frozen whipped topping, thawed");
            recipeDTOExpected.Ingredients.Add("2 cups watermelon");
            recipeDTOExpected.Ingredients.Add("1 (9 inch) prepared graham cracker crust");
          
            recipeDTOExpected.Instructions.Add("Mix together the watermelon gelatin and water. Fold gelatin mixture into the dessert topping. Add cut watermelon.");
            recipeDTOExpected.Instructions.Add("Pour mixture into graham cracker crust. Cool in refrigerator for about 3 hours.");

            var allRecipeScraper = new AllRecipesScraper();
            var recipeDTOActual = allRecipeScraper.ScrapeRecipe(validRecipeUrl, new RecipeDTO());

            Assert.Equal(recipeDTOExpected.Title, recipeDTOActual.Title);
            Assert.Equal(recipeDTOExpected.Description, recipeDTOActual.Description);
            Assert.Equal(recipeDTOExpected.Ingredients, recipeDTOActual.Ingredients);
            Assert.Equal(recipeDTOExpected.Instructions, recipeDTOActual.Instructions);
        }
    }
}
