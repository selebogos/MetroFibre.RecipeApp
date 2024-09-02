using MetroFibre.RecipeApp.Data.Entities;
using MetroFibre.RecipeApp.Services.Abstractions;
using MetroFibre.RecipeApp.Services.DTOs;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;
using static MetroFibre.RecipeApp.Services.Helpers.RecipeEnums;

namespace MetroFibre.RecipeApp.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ConcurrentDictionary<string, bool> _cacheKeys;
        public IngredientService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _cacheKeys = new ConcurrentDictionary<string, bool>();
        }

        public async Task<IngredientDto> GetByNameAsync(string name)
        {
            if (_memoryCache.TryGetValue(name, out IngredientDto Ingredient))
            {
                // Ingredient was found in cache
                return Ingredient;
            }

            // Ingredient not found in cache
            return new();
        }

        public async Task AddAsync(IngredientDto Ingredient)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
           .SetSlidingExpiration(TimeSpan.FromMinutes(2)) // Expire if not accessed within 10 minutes
           .SetAbsoluteExpiration(TimeSpan.FromHours(1)); // Expire after 1 hour, no matter what

            // Add the Ingredient to the cache with the specified key and options
            _memoryCache.Set(Ingredient.Name, Ingredient, cacheEntryOptions);
            // Keep track of the key
            _cacheKeys.TryAdd(Ingredient.Name, true);
        }

        public async Task<List<IngredientDto>> GetAllAsync()
        {
            List<IngredientDto> Ingredients = new List<IngredientDto>();

            foreach (var key in _cacheKeys.Keys)
            {
                if (_memoryCache.TryGetValue(key, out IngredientDto Ingredient))
                {
                    Ingredients.Add(Ingredient);
                }
            }

            return Ingredients;
        }
    }
}
