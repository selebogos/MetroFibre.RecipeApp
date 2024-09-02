using MetroFibre.RecipeApp.Data.Entities;
using MetroFibre.RecipeApp.Services.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MetroFibre.RecipeApp.Services.Helpers.RecipeEnums;

namespace MetroFibre.RecipeApp.Services.Abstractions
{
    public interface IIngredientService
    {
        Task AddAsync(IngredientDto incredient);
       Task<IngredientDto> GetByNameAsync(string name);
        Task<List<IngredientDto>> GetAllAsync();
    }
}
