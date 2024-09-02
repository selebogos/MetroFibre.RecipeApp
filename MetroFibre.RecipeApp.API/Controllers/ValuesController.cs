using MapsterMapper;
using MetroFibre.RecipeApp.API.Models;
using MetroFibre.RecipeApp.Services.Abstractions;
using MetroFibre.RecipeApp.Services.DTOs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MetroFibre.RecipeApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController(IFoodCreatorService _foodCreatorService, IMapper _mapper) : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var a = _foodCreatorService;
            return new string[] { "value1", "value2" };
        }
        [HttpPost("make")]
        public async Task<ActionResult> PrepareFood([FromBody] List<PreparationRequest> incredients)
        {
            var items = _mapper.Map<List<IngredientDto>>(incredients);
            var response = await _foodCreatorService.PrepareFoodAsync(items);

            return Ok(new { message = response });
        }
        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
