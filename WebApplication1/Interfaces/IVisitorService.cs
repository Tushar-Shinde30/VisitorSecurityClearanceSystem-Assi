using WebApplication1.DTO;

namespace WebApplication1.Interfaces
{
    public interface IVisitorService
    {
        Task<VisitorDTO> AddVisitor(VisitorDTO visitoModel);
        Task<IEnumerable<VisitorDTO>> GetAllVisitors();
        Task<VisitorDTO> GetVisitorById(string id);

        Task DeleteVisitor(string id);
        Task<VisitorDTO> UpdateVisitor(string id, VisitorDTO visitorModel);
        public Task<VisitorDTO> UpdateVisitorStatus(string visitorId, bool newStatus);
        Task<List<VisitorDTO>> GetVisitorsByStatus(bool status);

    }
}
