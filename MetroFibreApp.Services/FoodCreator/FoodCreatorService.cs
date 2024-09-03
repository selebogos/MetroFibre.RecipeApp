using MetroFibre.RecipeApp.Services.Abstractions;
using MetroFibre.RecipeApp.Services.DTOs;
using MetroFibre.RecipeApp.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroFibre.RecipeApp.Services.FoodCreator
{
    public class FoodCreatorService(IRecipeService _recipeService) : IFoodCreatorService
    {
        private List<RecipeDto> _recipes;
        private List<PreparationResultDto> _results;

        public async Task<List<PreparationResultDto>> PrepareFoodAsync(List<IngredientDto> incredientProvided)
        {
            _recipes = await _recipeService.GetAllAsync();
            int numberOfIngredients = 0;
            var orderedRecipeList = _recipes.OrderByDescending(x => x.Servings).ToList();
            _results= new List<PreparationResultDto>();
            PreparationResultDto result = null;

            //start with recipe that feeds most people
            foreach (var recipe in orderedRecipeList)
            {
                List<IngredientDto> recipeIngredients = new List<IngredientDto>();
                foreach (var incredient in incredientProvided)
                {
                    bool isAvailable = CheckIncredient(recipe, incredient); //check if ingredient is available
                    if (isAvailable)
                    {
                        recipeIngredients.Add(incredient);
                        numberOfIngredients++;
                    }
                }

                int numberOfDishesMade = 0;
                if (numberOfIngredients == recipe.Ingredients.Count) // if the required qty of ingredients is available then make food
                {
                     result =   MakeFood(recipe, incredientProvided, ref numberOfDishesMade);
                  
                    _results.Add(result);
                }
                 numberOfIngredients = 0;
            }

            return _results;
        }

        private bool CheckIncredient(RecipeDto recipe, IngredientDto incredientToCheck)
        {
            bool isAvailable = false;
            foreach (var incredient in recipe.Ingredients)
            {
                if (incredient.Name.ToLower() == incredientToCheck.Name.ToLower() && incredient.Quantity <= incredientToCheck.Quantity)
                {
                    isAvailable = true;
                    break;
                }
            }

            return isAvailable;
        }
        private bool CheckIngredientAvailability(RecipeDto recipe, List<IngredientDto> incredientToCheck)
        {
            bool isAvailable = true;
            foreach (var recipeIngredient in recipe.Ingredients)
            {
                foreach (var incredient in incredientToCheck)
                {
                    if (incredient.Name.ToLower() == recipeIngredient.Name.ToLower() && incredient.Quantity < recipeIngredient.Quantity)
                    {
                        isAvailable = false;
                        break;
                    }
                }
            }
            return isAvailable;
        }
        private bool IsThereAnotherRecipeThatUsesLessOfIt(IngredientDto ingredient, int currentQty, out RecipeDto recipe)
        {
            bool isAvailable = false;
            recipe = null;
            foreach (var item in _recipes)
            {
                if (CheckIncredient(item, ingredient) && item.Ingredients.First(x => x.Name.ToLower() == ingredient.Name.ToLower()).Quantity < currentQty) //current Qty is greater than the required Qty
                {
                    recipe = item;
                    isAvailable = true;
                }
            }

            return isAvailable;
        }
        private PreparationResultDto MakeFood(RecipeDto recipe, List<IngredientDto> incredients, ref int numberOfDishesMade)
        {
            int numberOfIngredientsAvailable = 0;
            bool moreCanBeMade = true;
            foreach (var recipeIngredient in recipe.Ingredients)
            {
                foreach (var ingredient in incredients)
                {
                    bool isAvailable = IsThereAnotherRecipeThatUsesLessOfIt(ingredient, ingredient.Quantity, out RecipeDto alternativeRecipe);

                    if (isAvailable && alternativeRecipe.Servings == recipe.Servings && alternativeRecipe.Name.ToLower() != recipe.Name.ToLower()
                        && CheckIngredientAvailability(alternativeRecipe, incredients))
                    {
                        numberOfIngredientsAvailable = 0;
                        return MakeFood(alternativeRecipe, incredients, ref numberOfIngredientsAvailable);
                    }

                    if (ingredient.Quantity > 0)
                    {
                        if (ingredient.Name.ToLower() == recipeIngredient.Name.ToLower() && ingredient.Quantity >= recipeIngredient.Quantity)
                        {
                            ingredient.Quantity -= recipeIngredient.Quantity; //substract Qty required by recipe
                            numberOfIngredientsAvailable++;
                        }

                        if (ingredient.Name.ToLower() == recipeIngredient.Name.ToLower() && ingredient.Quantity >= recipeIngredient.Quantity)
                        {
                            moreCanBeMade = true;
                        }
                        else if (ingredient.Name.ToLower() == recipeIngredient.Name.ToLower() && ingredient.Quantity < recipeIngredient.Quantity)
                        {
                            moreCanBeMade = false;
                            break;
                        }

                    }
                }

            }

            if (numberOfIngredientsAvailable == recipe.Ingredients.Count)
                numberOfDishesMade++;

            if (moreCanBeMade && numberOfIngredientsAvailable == recipe.Ingredients.Count)
            {
                if (CheckIngredientAvailability(recipe, incredients))
                    return MakeFood(recipe, incredients, ref numberOfDishesMade);
            }

           int dishes= numberOfDishesMade;
            return new PreparationResultDto {

                Servings= recipe.Servings,
                Recipe=recipe.Name,
                Qty= dishes
            };
        }
    }
}
