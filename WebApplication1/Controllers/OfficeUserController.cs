using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTO;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OfficeUserController : Controller
    {
        private readonly IOfficeService _officeService;
        private readonly IVisitorService _visitorService;


        public OfficeUserController(IOfficeService officeService)
        {
            _officeService = officeService;
        }


        [HttpPost]
        public async Task<ActionResult<OfficeDTO>> AddOffice([FromBody] OfficeDTO officeModel)
        {
            var result = await _officeService.AddOffice(officeModel);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginModel)
        {
            var officeUser = await _officeService.LoginOfficeUser(loginModel.Email, loginModel.Password);
            if (officeUser == null)
            {
                return Unauthorized("Invalid credentials");
            }

            return Ok(officeUser);
        }

        [HttpGet("{id}")]
        public async Task<OfficeDTO> GetOfficeById(string id)
        {
            return await _officeService.GetOfficeById(id);
        }
       

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOffice(string id, OfficeDTO securityModel)
        {
            try
            {
                var updatedSecurity = await _officeService.UpdateOffice(id, securityModel);
                return Ok(updatedSecurity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateVisitor (Controller): {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{visitorId}")]
        public async Task<IActionResult> UpdateVisitorStatus(string visitorId, bool newStatus)
        {
            if (string.IsNullOrEmpty(visitorId))
            {
                return BadRequest("Visitor ID cannot be null or empty");
            }

            try
            {
                var updatedVisitor = await _visitorService.UpdateVisitorStatus(visitorId, newStatus);
                return Ok(updatedVisitor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{status}")]
        public async Task<IActionResult> GetVisitorsByStatus(bool status)
        {
            var visitors = await _visitorService.GetVisitorsByStatus(status);
            return Ok(visitors);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOffice(string id)
        {
            await _officeService.DeleteOffice(id);
            return NoContent();
        }
    }
}
