using MapsterMapper;
using MetroFibre.RecipeApp.API.Models;
using MetroFibre.RecipeApp.Services.Abstractions;
using MetroFibre.RecipeApp.Services.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetroFibre.RecipeApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodCreatorController(IFoodCreatorService _foodCreatorService,IMapper _mapper) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> PrepareFood([FromBody] List<PreparationRequest> incredients)
        {
            var items=_mapper.Map<List<IngredientDto>>(incredients);
           var response=await _foodCreatorService.PrepareFoodAsync(items);

            return Ok(new{message= response});
        }
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //    int a = 0;
        //}
    }

    
}
