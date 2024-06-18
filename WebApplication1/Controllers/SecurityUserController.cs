using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTO;
using WebApplication1.Interface;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SecurityUserController : Controller
    {
        private readonly ISecurityService _securityService;
        private readonly IVisitorService _visitorService;
        public SecurityUserController(ISecurityService securityService, IVisitorService visitorService)
        {
            _securityService = securityService;
            _visitorService = visitorService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginModel)
        {
            var securityUser = await _securityService.LoginSecurityUser(loginModel.Email, loginModel.Password);
            if (securityUser == null)
            {
                return Unauthorized("Invalid credentials");
            }

            return Ok(securityUser);
        }

        [HttpGet("{status}")]
        public async Task<IActionResult> GetVisitorsByStatus(bool status)
        {
            var visitors = await _visitorService.GetVisitorsByStatus(status);
            return Ok(visitors);
        }

        [HttpGet("{id}")]
        public async Task<VisitorDTO> GetVisitorById(string id)
        {
            return await _visitorService.GetVisitorById(id);
        }



        [HttpGet("{id}")]
        public async Task<SecurityDTO> GetSecurityById(string id)
        {
            return await _securityService.GetSecurityById(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSecurity(string id, SecurityDTO securityModel)
        {
            try
            {
                var updatedSecurity = await _securityService.UpdateSecurity(id, securityModel);
                return Ok(updatedSecurity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateVisitor (Controller): {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSecurity(string id)
        {
            await _securityService.DeleteSecurity(id);
            return NoContent();
        }
    }
}
