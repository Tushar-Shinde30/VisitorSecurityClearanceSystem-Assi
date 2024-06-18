using AutoMapper;
using WebApplication1.DTO;
using WebApplication1.Entities;
using WebApplication1.Interface;
using WebApplication1.Interfaces;

namespace WebApplication1.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly ICosmosDbService _cosmoDBService;
        private readonly IMapper _autoMapper;
        public SecurityService(ICosmosDbService cosmoDBService, IMapper mapper)
        {
            _cosmoDBService = cosmoDBService;
            _autoMapper = mapper;
        }

        public async Task<SecurityDTO> AddSecurity(SecurityDTO securityModel)
        {

            // Map the DTO to an Entity
            var securityEntity = _autoMapper.Map<SecurityEntity>(securityModel);

            // Initialize the Entity
            securityEntity.Intialize(true, "security", "nisarga", "nisarga");

            // Add the entity to the database
            var response = await _cosmoDBService.Add(securityEntity);

            // Map the response back to a DTO
            return _autoMapper.Map<SecurityDTO>(response);
        }

        public async Task<SecurityDTO> GetSecurityById(string id)
        {
            var security = await _cosmoDBService.GetSecurityById(id); 
            return _autoMapper.Map<SecurityDTO>(security);
        }

        public async Task<SecurityDTO> UpdateSecurity(string id, SecurityDTO securityModel)
        {
            var securityEntity = await _cosmoDBService.GetSecurityById(id);
            if (securityEntity == null)
            {
                throw new Exception("Security not found");
            }
            securityEntity = _autoMapper.Map<SecurityEntity>(securityModel);
            securityEntity.Id = id;
            var response = await _cosmoDBService.Update(securityEntity);
            return _autoMapper.Map<SecurityDTO>(response);
        }

        public async Task DeleteSecurity(string id)
        {
            await _cosmoDBService.DeleteSecurity(id);
        }

        public async Task<SecurityDTO> LoginSecurityUser(string email, string password)
        {
            // Fetch the manager entity by email
            var securityUser = await _cosmoDBService.GetSecurityUserByEmail(email);

            if (securityUser == null || securityUser.Password != password)
            {
                return null; // Credentials are invalid
            }

            // Map ManagerEntity to ManagerDTO
            var securityDto = new SecurityDTO
            {
                Id = securityUser.Id,
                Name = securityUser.Name,
                Email = securityUser.Email,
                // Add other properties here
            };

            return securityDto;
        }
    }
}
