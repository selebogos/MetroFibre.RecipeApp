using MetroFibre.RecipeApp.Data.Entities;
using MetroFibre.RecipeApp.Services.Abstractions;
using MetroFibre.RecipeApp.Services.DTOs;
using MetroFibre.RecipeApp.Services.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Collections.Generic;
using static MetroFibre.RecipeApp.Services.Helpers.RecipeEnums;

namespace MetroFibre.RecipeApp.Services
{
    public class RecipeService: IRecipeService
    {
        private readonly IIngredientService _incredientService;
        private readonly IMemoryCache _memoryCache;
        private readonly ConcurrentDictionary<string, bool> _cacheKeys;

        public RecipeService(IIngredientService incredientService, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _incredientService= incredientService;
            _cacheKeys = new ConcurrentDictionary<string, bool>();
        }
        public async Task AddRecipeAsync()
        {
            var incredient = new IngredientDto();
            List<IngredientDto> incredients = new List<IngredientDto>();
            List<RecipeDto> recipes = new List<RecipeDto>();
            foreach (RecipeEnums.Recipe recipe in Enum.GetValues(typeof(RecipeEnums.Recipe)))
            { 
                AddIncredients(recipe, incredients);
    
                var rec = new RecipeDto
                {
                    Ingredients = incredients,
                    Name= RecipeEnums.GetEnumDescription(recipe),
                    Servings= GetRecipeServing(recipe)
                };
                recipes.Add(rec);

                incredients = new List<IngredientDto>();
             
            
            }
            string iii = JsonConvert.SerializeObject(recipes);
            CacheRecipeData(recipes);
        }

        public async Task<List<RecipeDto>> GetAllAsync()
        {
            List<RecipeDto> _recipes = new List<RecipeDto>()
            {
                new RecipeDto { Name = "Burger", Servings = 1, Ingredients = new List<IngredientDto>() { new IngredientDto{ Name = "meat", Quantity = 1 }, new IngredientDto { Name = "lettuce", Quantity = 1 }, new IngredientDto { Name = "tomato", Quantity = 1 }, new IngredientDto { Name = "cheese", Quantity = 1 }, new IngredientDto { Name = "dough", Quantity = 1 } } },
                new RecipeDto { Name = "Pie", Servings = 1, Ingredients= new List<IngredientDto>() { new IngredientDto { Name = "dough", Quantity = 2 }, new IngredientDto { Name = "meat", Quantity = 1 } } },
                new RecipeDto { Name = "Sandwich", Servings = 1, Ingredients = new List<IngredientDto>() { new IngredientDto { Name = "dough", Quantity = 1 }, new IngredientDto { Name = "cucumber", Quantity = 1 } } },
                new RecipeDto { Name = "Pasta", Servings = 2, Ingredients= new List<IngredientDto>() { new IngredientDto { Name = "dough", Quantity = 2 }, new IngredientDto { Name = "tomato", Quantity = 1 }, new IngredientDto { Name = "cheese", Quantity = 2 }, new IngredientDto { Name = "meat", Quantity = 1 } } },
                new RecipeDto { Name = "Salad", Servings = 3, Ingredients = new List<IngredientDto>() { new IngredientDto { Name = "lettuce", Quantity = 2 }, new IngredientDto { Name = "tomato", Quantity = 2 }, new IngredientDto { Name = "cucumber", Quantity = 1 }, new IngredientDto { Name = "cheese", Quantity = 2 }, new IngredientDto { Name = "olives", Quantity = 1 } } },
                new RecipeDto { Name = "Pizza", Servings = 4, Ingredients= new List<IngredientDto>() { new IngredientDto { Name = "dough", Quantity = 3 }, new IngredientDto { Name = "tomato", Quantity = 2 }, new IngredientDto { Name = "cheese", Quantity = 3 }, new IngredientDto { Name = "olives", Quantity = 1 } } }
            };
            return _recipes;
            //if (_memoryCache.TryGetValue("recipes", out List<RecipeDto> cachedData))
            //{
            //    // Data was found in the cache
            //    return cachedData;
            //}
            //else
            //{
            //    await AddRecipeAsync();
            //    return await GetAllAsync();
            //}
        }

        public async Task<RecipeDto> GetRecipeAsync()
        {
            throw new NotImplementedException();
        }

        private int GetRecipeServing(RecipeEnums.Recipe recipe)
        {

            switch (recipe) {
                case RecipeEnums.Recipe.Salad:
                    return 3;
                case RecipeEnums.Recipe.Burger:
                    return 1;
                case RecipeEnums.Recipe.Pie:
                    return 1;
                case RecipeEnums.Recipe.Sandwich:
                    return 1;
                case RecipeEnums.Recipe.Pasta:
                    return 2;
                case RecipeEnums.Recipe.Pizza:
                    return 4;
                default: return 0;

            }
        
        }
        private void AddIncredients(RecipeEnums.Recipe recipe, List<IngredientDto> incredient) 
        {

            switch (recipe) {
                case RecipeEnums.Recipe.Burger:
                    AddBurgerIncredient(incredient);
                    break;
                case RecipeEnums.Recipe.Sandwich:
                    AddSandwichIncredient(incredient);
                    break;

                case RecipeEnums.Recipe.Pie:
                    AddPieIncredient(incredient);
                    break;

                case RecipeEnums.Recipe.Pasta:
                    AddPastaIncredient(incredient);
                    break;
                case RecipeEnums.Recipe.Salad:
                    AddSaladIncredient(incredient);
                    break;
                case RecipeEnums.Recipe.Pizza:
                    AddPizzaIncredient(incredient);
                    break;
            }
     
        }
        private void AddPieIncredient(List<IngredientDto> incredients) 
        {
            var incredient = new IngredientDto();
            incredient.Name = RecipeEnums.GetEnumDescription(RecipeEnums.Incredients.Dough);
            incredient.Quantity = 2;
            incredients.Add(incredient);

            incredient = new IngredientDto();
            incredient.Name = RecipeEnums.GetEnumDescription(RecipeEnums.Incredients.Meat);
            incredient.Quantity = 2;
            incredients.Add(incredient);
        }
        private void AddPastaIncredient(List<IngredientDto> incredients) 
        {
            var incredient = new IngredientDto();
            incredient.Name = RecipeEnums.GetEnumDescription(RecipeEnums.Incredients.Dough);
            incredient.Quantity = 2;
            incredients.Add(incredient);

             incredient = new IngredientDto();
            incredient.Name = RecipeEnums.GetEnumDescription(RecipeEnums.Incredients.Cheese);
            incredient.Quantity = 2;
            incredients.Add(incredient);

            incredient = new IngredientDto();
            incredient.Name = RecipeEnums.GetEnumDescription(RecipeEnums.Incredients.Tomato);
            incredient.Quantity = 1;
            incredients.Add(incredient);

             incredient = new IngredientDto();
            incredient.Name = RecipeEnums.GetEnumDescription(RecipeEnums.Incredients.Meat);
            incredient.Quantity = 1;
            incredients.Add(incredient);
        }
        private void AddSandwichIncredient(List<IngredientDto> incredients) 
        {
            var incredient = new IngredientDto();
            incredient.Name = RecipeEnums.GetEnumDescription(RecipeEnums.Incredients.Dough);
            incredient.Quantity = 1;
            incredients.Add(incredient);

             incredient = new IngredientDto();
            incredient.Name = RecipeEnums.GetEnumDescription(RecipeEnums.Incredients.Cucumber);
            incredient.Quantity = 1;
            incredients.Add(incredient);
        }
        private void AddBurgerIncredient(List<IngredientDto> incredients) 
        {
            var incredient=new IngredientDto();
            incredient.Name = RecipeEnums.GetEnumDescription(RecipeEnums.Incredients.Meat);
            incredient.Quantity = 1;
            incredients.Add(incredient);

            incredient = new IngredientDto();
            incredient.Name = RecipeEnums.GetEnumDescription(RecipeEnums.Incredients.Lettuce);
            incredient.Quantity = 1;
            incredients.Add(incredient);

            incredient = new IngredientDto();
            incredient.Name = RecipeEnums.GetEnumDescription(RecipeEnums.Incredients.Tomato);
            incredient.Quantity = 1;
            incredients.Add(incredient);

            incredient = new IngredientDto();
            incredient.Name = RecipeEnums.GetEnumDescription(RecipeEnums.Incredients.Cheese);
            incredient.Quantity = 1;
            incredients.Add(incredient);

            incredient = new IngredientDto();
            incredient.Name = RecipeEnums.GetEnumDescription(RecipeEnums.Incredients.Dough);
            incredient.Quantity = 1;
            incredients.Add(incredient);
        }
        private void AddSaladIncredient(List<IngredientDto> incredients)
        {
            var incredient = new IngredientDto();
            incredient.Name = RecipeEnums.GetEnumDescription(RecipeEnums.Incredients.Cucumber);
            incredient.Quantity = 1;
            incredients.Add(incredient) ;

            incredient = new IngredientDto();
            incredient.Name = RecipeEnums.GetEnumDescription(RecipeEnums.Incredients.Lettuce);
            incredient.Quantity = 2;
            incredients.Add(incredient);

            incredient = new IngredientDto();
            incredient.Name = RecipeEnums.GetEnumDescription(RecipeEnums.Incredients.Tomato);
            incredient.Quantity = 2;
            incredients.Add(incredient);

            incredient = new IngredientDto();
            incredient.Name = RecipeEnums.GetEnumDescription(RecipeEnums.Incredients.Cheese);
            incredient.Quantity = 2;
            incredients.Add(incredient);

            incredient = new IngredientDto();
            incredient.Name = RecipeEnums.GetEnumDescription(RecipeEnums.Incredients.Olives);
            incredient.Quantity = 1;
            incredients.Add(incredient);
        }

        private void AddPizzaIncredient(List<IngredientDto> incredients)
        {
            var incredient = new IngredientDto();
            incredient.Name = RecipeEnums.GetEnumDescription(RecipeEnums.Incredients.Olives);
            incredient.Quantity = 1;
            incredients.Add(incredient);

            incredient = new IngredientDto();
            incredient.Name = RecipeEnums.GetEnumDescription(RecipeEnums.Incredients.Tomato);
            incredient.Quantity = 2;
            incredients.Add(incredient);

            incredient = new IngredientDto();
            incredient.Name = RecipeEnums.GetEnumDescription(RecipeEnums.Incredients.Cheese);
            incredient.Quantity = 3;
            incredients.Add(incredient);

            incredient = new IngredientDto();
            incredient.Name = RecipeEnums.GetEnumDescription(RecipeEnums.Incredients.Dough);
            incredient.Quantity = 3;
            incredients.Add(incredient);
        }

        private void CacheRecipeData(List<RecipeDto> recipes) 
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
        .SetSlidingExpiration(TimeSpan.FromHours(24)) // Expire if not accessed within 24 hr
        .SetAbsoluteExpiration(TimeSpan.FromHours(24)); // Expire after 24 hour, no matter what

            // Add the recipes to the cache with the specified key and options
            _memoryCache.Set("recipes", recipes, cacheEntryOptions);
            // Keep track of the key
           // _cacheKeys.TryAdd(RecipeEnums.GetEnumDescription(recipeEnum), true);
        }

    }
}
