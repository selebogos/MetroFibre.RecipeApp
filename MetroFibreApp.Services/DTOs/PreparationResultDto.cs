using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroFibre.RecipeApp.Services.DTOs
{
    public class PreparationResultDto
    {
        public string Recipe { get; set; }
        public int Qty { get; set; }
        public int Servings { get; set; }
    }
}
