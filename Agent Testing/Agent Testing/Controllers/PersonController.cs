using Agent_Testing.Services.Interface;
using IBM.Data.Db2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Agent_Testing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        public readonly IPersonDetails _personDetails;
        public PersonController(IPersonDetails personDetails)
        {
            _personDetails = personDetails;
        }

        //[HttpGet]
        //public  async Task<IActionResult> GetAllPersons()
        //{
        //    try
        //    {
        //        var result =  await _personDetails.GetAllPersons();
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error fetching data: {ex.Message}");
        //    }
        //}

        //[HttpGet("Details")]
        //public async Task<IActionResult> GetPersonDetail(string phno)
        //{
        //    var result = await _personDetails.GetPersonDetails(phno);
        //    if (result != null)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result);

        //}

        [HttpGet]
        public async Task<IActionResult> GetAllPersons()
        {
            try
            {
                var result = await _personDetails.GetAllPersons();
                return Ok(result);
            }
            catch (DB2Exception dbEx)
            {
                return StatusCode(500, new
                {
                    error = "DB2 Error",
                    message = dbEx.Message,
                    sqlState = dbEx.SqlState,
                    errorCode = dbEx.ErrorCode
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "General Error",
                    message = ex.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }

        [HttpGet("test-connection")]
        public IActionResult TestConnection()
        {
            try
            {
                _personDetails.TestConnection();
                return Ok("Connection successful!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Connection failed: {ex.Message}");
            }
        }
    }
}
