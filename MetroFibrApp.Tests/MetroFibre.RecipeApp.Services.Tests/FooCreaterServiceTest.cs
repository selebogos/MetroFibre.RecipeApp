using MetroFibre.RecipeApp.Services.Abstractions;
using MetroFibre.RecipeApp.Services.DTOs;
using MetroFibre.RecipeApp.Services.FoodCreator;
using Moq;
using Xunit;

namespace MetroFibre.RecipeApp.Tests.MetroFibre.RecipeApp.Services.Tests
{
    public class FoodCreatorServiceTests
    {
        private readonly Mock<IRecipeService> _mockRecipeService;
        private readonly Mock<IIngredientService> _mockIngredientService;
        private readonly FoodCreatorService _foodCreatorService;

        public FoodCreatorServiceTests()
        {
            _mockRecipeService = new Mock<IRecipeService>();
            _mockIngredientService = new Mock<IIngredientService>();
            _foodCreatorService = new FoodCreatorService(_mockIngredientService.Object, _mockRecipeService.Object);
        }

        [Fact]
        public async Task PrepareFoodAsync_ShouldReturnCorrectResult_WhenIngredientsMatchRecipes()
        {
            // Arrange
            var ingredients = new List<IngredientDto>
            {
                new IngredientDto { Name = "meat", Quantity = 5 },
                new IngredientDto { Name = "lettuce", Quantity = 3 },
                new IngredientDto { Name = "tomato", Quantity = 3 },
                new IngredientDto { Name = "cheese", Quantity = 3 },
                new IngredientDto { Name = "dough", Quantity = 5 }
            };

            var recipes = new List<RecipeDto>
            {
                new RecipeDto
                {
                    Name = "Burger",
                    Servings = 1,
                    Ingredients = new List<IngredientDto>
                    {
                        new IngredientDto { Name = "meat", Quantity = 1 },
                        new IngredientDto { Name = "lettuce", Quantity = 1 },
                        new IngredientDto { Name = "tomato", Quantity = 1 },
                        new IngredientDto { Name = "cheese", Quantity = 1 },
                        new IngredientDto { Name = "dough", Quantity = 1 }
                    }
                }
            };

            _mockRecipeService.Setup(x => x.GetAllAsync()).ReturnsAsync(recipes);

            // Act
            var result = await _foodCreatorService.PrepareFoodAsync(ingredients);

            // Assert
            Assert.NotNull(result);
            Assert.Contains("Burger X 3", result.Results); // Expecting 3 Burgers based on the ingredients provided
        }

        [Fact]
        public async Task PrepareFoodAsync_ShouldReturnEmptyResult_WhenNoRecipesMatch()
        {
            // Arrange
            var ingredients = new List<IngredientDto>
            {
                new IngredientDto { Name = "meat", Quantity = 1 }
            };

            var recipes = new List<RecipeDto>
            {
                new RecipeDto
                {
                    Name = "Pizza",
                    Servings = 4,
                    Ingredients = new List<IngredientDto>
                    {
                        new IngredientDto { Name = "dough", Quantity = 3 },
                        new IngredientDto { Name = "tomato", Quantity = 2 },
                        new IngredientDto { Name = "cheese", Quantity = 3 },
                        new IngredientDto { Name = "olives", Quantity = 1 }
                    }
                }
            };

            _mockRecipeService.Setup(x => x.GetAllAsync()).ReturnsAsync(recipes);

            // Act
            var result = await _foodCreatorService.PrepareFoodAsync(ingredients);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(string.Empty, result.Results);
        }

        // You can add more tests to cover other scenarios and edge cases
    }

}
