using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroFibre.RecipeApp.Services.DTOs
{
    public class RecipeDto
    {
        public string Name { get; set; }
        public List<IngredientDto> Ingredients { get; set; }
        public int Servings { get; set; }
    }
}
