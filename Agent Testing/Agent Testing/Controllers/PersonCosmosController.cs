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


        [HttpGet("{PhNo}")]
        public async Task<ActionResult<Person>> GetPersonbyPhoneNumber(string PhNo)
        {
            try
            {
                var person = await _personCosmosService.GetPersonByPhNoAsync(PhNo);

                if (person == null)
                {
                    return NotFound(new { message = $"person with PhoneNumber '{PhNo}' not found" });
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
        public async Task<ActionResult<Person>> CreateProduct([FromBody] Person person)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdProduct = await _personCosmosService.CreatePersonAsync(person);

                return Ok(createdProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("{PhoneNumber}")]
        public async Task<ActionResult<Person>> UpdatePersonDetails(string PhoneNumber, [FromBody] Person person)
        {
            try
            {
                var updatedProduct = await _personCosmosService.UpdatePersonAsync(PhoneNumber, person);
                if (updatedProduct != null)
                    return Ok(updatedProduct);
                else return BadRequest($"Person with Phonenumber {PhoneNumber} was not found ");
            }

            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePerson(string id)
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
