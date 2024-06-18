using WebApplication1.DTO;

namespace WebApplication1.Interfaces
{

    public interface IOfficeService
    {
        Task<OfficeDTO> AddOffice(OfficeDTO officeModel);
        Task<OfficeDTO> GetOfficeById(string id);
        Task<OfficeDTO> UpdateOffice(string id, OfficeDTO officeModel);
        Task DeleteOffice(string id);

        Task<OfficeDTO> LoginOfficeUser(string email, string password);
    }
}
