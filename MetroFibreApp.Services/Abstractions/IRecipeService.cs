using MetroFibre.RecipeApp.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MetroFibre.RecipeApp.Services.Helpers.RecipeEnums;

namespace MetroFibre.RecipeApp.Services.Abstractions
{
    public interface IRecipeService
    {
        Task AddRecipeAsync();
        Task<RecipeDto> GetRecipeAsync();
        Task<List<RecipeDto>> GetAllAsync();
    }
}
