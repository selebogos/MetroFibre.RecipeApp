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
        private readonly FoodCreatorService _foodCreatorService;

        public FoodCreatorServiceTests()
        {
            _mockRecipeService = new Mock<IRecipeService>();
            _foodCreatorService = new FoodCreatorService(_mockRecipeService.Object);
        }

        [Fact]
        public async Task PrepareFoodAsync_ShouldReturnExpectedResults_WhenIngredientsMatchRecipes()
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
            Assert.Single(result);
            Assert.Equal("Burger", result[0].Recipe);
            Assert.Equal(3, result[0].Qty); // 3 burgers can be made based on the ingredients provided
        }

        [Fact]
        public async Task PrepareFoodAsync_ShouldReturnEmptyResults_WhenNoRecipesMatch()
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
            Assert.Empty(result); // No matching recipes, so the result should be empty
        }

        [Fact]
        public async Task PrepareFoodAsync_ShouldHandlePartialMatches_Correctly()
        {
            // Arrange
            var ingredients = new List<IngredientDto>
            {
                new IngredientDto { Name = "meat", Quantity = 1 },
                new IngredientDto { Name = "lettuce", Quantity = 1 },
                new IngredientDto { Name = "tomato", Quantity = 1 },
                new IngredientDto { Name = "cheese", Quantity = 1 },
                new IngredientDto { Name = "dough", Quantity = 1 }
            };

            List<RecipeDto> recipes = new List<RecipeDto>()
            {
                new RecipeDto { Name = "Burger", Servings = 1, Ingredients = new List<IngredientDto>() { new IngredientDto{ Name = "meat", Quantity = 1 }, new IngredientDto { Name = "lettuce", Quantity = 1 }, new IngredientDto { Name = "tomato", Quantity = 1 }, new IngredientDto { Name = "cheese", Quantity = 1 }, new IngredientDto { Name = "dough", Quantity = 1 } } },
                new RecipeDto { Name = "Pie", Servings = 1, Ingredients= new List<IngredientDto>() { new IngredientDto { Name = "dough", Quantity = 2 }, new IngredientDto { Name = "meat", Quantity = 2 } } },
                new RecipeDto { Name = "Sandwich", Servings = 1, Ingredients = new List<IngredientDto>() { new IngredientDto { Name = "dough", Quantity = 1 }, new IngredientDto { Name = "cucumber", Quantity = 1 } } },
                new RecipeDto { Name = "Pasta", Servings = 2, Ingredients= new List<IngredientDto>() { new IngredientDto { Name = "dough", Quantity = 2 }, new IngredientDto { Name = "tomato", Quantity = 1 }, new IngredientDto { Name = "cheese", Quantity = 2 }, new IngredientDto { Name = "meat", Quantity = 1 } } },
                new RecipeDto { Name = "Salad", Servings = 3, Ingredients = new List<IngredientDto>() { new IngredientDto { Name = "lettuce", Quantity = 2 }, new IngredientDto { Name = "tomato", Quantity = 2 }, new IngredientDto { Name = "cucumber", Quantity = 1 }, new IngredientDto { Name = "cheese", Quantity = 2 }, new IngredientDto { Name = "olives", Quantity = 1 } } },
                new RecipeDto { Name = "Pizza", Servings = 4, Ingredients= new List<IngredientDto>() { new IngredientDto { Name = "dough", Quantity = 3 }, new IngredientDto { Name = "tomato", Quantity = 2 }, new IngredientDto { Name = "cheese", Quantity = 3 }, new IngredientDto { Name = "olives", Quantity = 1 } } }
            };

            _mockRecipeService.Setup(x => x.GetAllAsync()).ReturnsAsync(recipes);

            // Act
            var result = await _foodCreatorService.PrepareFoodAsync(ingredients);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Burger", result[0].Recipe);
            Assert.Equal(1, result[0].Qty); // Only one burger can be made with the given ingredients
        }
    }
}
