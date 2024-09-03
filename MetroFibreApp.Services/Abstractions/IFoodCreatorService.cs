using MetroFibre.RecipeApp.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroFibre.RecipeApp.Services.Abstractions
{
    public interface IFoodCreatorService
    {
        Task<List<PreparationResultDto>> PrepareFoodAsync(List<IngredientDto> incredients);
    }
}
