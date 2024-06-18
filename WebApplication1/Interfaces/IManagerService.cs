using WebApplication1.DTO;

namespace WebApplication1.Interfaces
{

    public interface IManagerService
    {
        Task<ManagerDTO> AddManager(ManagerDTO managerModel);
        Task<ManagerDTO> GetManagerById(string id);
        Task<ManagerDTO> UpdateManager(string id, ManagerDTO managerModel);
        Task DeleteManager(string id);



    }
}
