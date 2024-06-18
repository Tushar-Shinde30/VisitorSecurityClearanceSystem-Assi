using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using WebApplication1.CosmosDB;
using WebApplication1.DTO;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VisitorController : Controller
    {
        private readonly ICosmosDbService _cosmoDBService;
        public readonly IVisitorService _visitorService;
        public IMapper _mapper;

        public VisitorController (IVisitorService visitorService)
        {
            _visitorService = visitorService;
        }

        [HttpPost]
        public async Task<VisitorDTO> AddVisitor(VisitorDTO visitoModel)
        {
            return await _visitorService.AddVisitor(visitoModel);
        }

        [HttpGet("{id}")]
        public async Task<VisitorDTO> GetVisitorById(string id)
        {
            return await _visitorService.GetVisitorById(id);
        }

        [HttpGet]
        public async Task<IEnumerable<VisitorDTO>> GetAllVisitors()
        {
            return await _visitorService.GetAllVisitors();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVisitor(string id, VisitorDTO visitorModel)
        {
            try
            {
                var updatedVisitor = await _visitorService.UpdateVisitor(id, visitorModel);
                return Ok(updatedVisitor);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateVisitor (Controller): {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteVisitor(string id)
        {
            await _visitorService.DeleteVisitor(id);
            return NoContent();
        }


    }


}
