using Agent_Testing.Models;
using Agent_Testing.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Agent_Testing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonCosmosController : ControllerBase
    {
        private readonly IPersonCosmosService _personCosmosService;

        public PersonCosmosController(IPersonCosmosService personCosmosService, ILogger<PersonCosmosController> logger)
        {
            _personCosmosService = personCosmosService;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Person>>> GetAllPersons()
        {
            try
            {
                var person = await _personCosmosService.GetAllPersonsAsync();
                return Ok(person);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred while retrieving Person Details" });
            }
        }

   
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Person>> GetProductById(int id)
        {
            try
            {
                var person = await _personCosmosService.GetPersonByIdAsync(id);

                if (person == null)
                {
                    return NotFound(new { message = $"person with id '{id}' not found" });
                }

                return Ok(person);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred while retrieving the person detail" });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Person>> CreateProduct([FromBody] Person person)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdProduct = await _personCosmosService.CreatePersonAsync(person);

                return CreatedAtAction(
                    nameof(GetProductById),
                    new { id = createdProduct.Id },
                    createdProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }            
        }

      
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Person>> UpdatePersonDetails(int id, [FromBody] Person person)
        {
            try
            {                
                var updatedProduct = await _personCosmosService.UpdatePersonAsync(id, person);

                if (updatedProduct == null)
                {
                    return NotFound(new { message = $"Product with id '{id}' not found" });
                }
                return Ok(updatedProduct);
            }
           
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeletePerson(int id)
        {
            try
            {
                var result = await _personCosmosService.DeletePersonAsync(id);

                if (!result)
                {
                    return NotFound(new { message = $"Person with id '{id}' not found" });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred while deleting the person" });
            }
        }
    }
}
