using System.ComponentModel.DataAnnotations;

namespace MetroFibre.RecipeApp.API.Models
{
    public class PreparationRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
