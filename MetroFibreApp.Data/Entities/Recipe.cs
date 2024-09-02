using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroFibre.RecipeApp.Data.Entities
{
    public class Recipe
    {
        public Recipe()
        {
            Ingredients = new List<Ingredient>();
        }
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public int Servings { get; set; }
    }
}
