using WebApplication1.DTO;

namespace WebApplication1.Interface
{
    public interface ISecurityService
    {
        Task<SecurityDTO> AddSecurity(SecurityDTO securityModel);
        Task<SecurityDTO> GetSecurityById(string id);
        Task<SecurityDTO> UpdateSecurity(string id, SecurityDTO securityModel);
        Task DeleteSecurity(string id);

        Task<SecurityDTO> LoginSecurityUser(string email, string password);
    }
}
