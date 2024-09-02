using MetroFibre.RecipeApp.Services.Abstractions;
using MetroFibre.RecipeApp.Services.FoodCreator;
using Microsoft.Extensions.DependencyInjection;

namespace MetroFibre.RecipeApp.Services.ServiceCollectionExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMetrofibeAppServices(this IServiceCollection services)
        {
            services.AddScoped<IIngredientService, IngredientService>();
            services.AddScoped<IRecipeService, RecipeService>(); 
            services.AddScoped<IFoodCreatorService, FoodCreatorService>();

            services.AddMemoryCache();
        }
    }
}
