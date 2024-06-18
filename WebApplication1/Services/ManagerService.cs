using AutoMapper;
using WebApplication1.DTO;
using WebApplication1.Entities;
using WebApplication1.Interfaces;
namespace WebApplication1.Services
{
    public class ManagerService : IManagerService
    {
        private readonly ICosmosDbService _cosmoDBService;
        private readonly IMapper _autoMapper;

        public ManagerService(ICosmosDbService cosmoDBService, IMapper mapper)
        {
            _cosmoDBService = cosmoDBService;
            _autoMapper = mapper;
        }

        public async Task<ManagerDTO> AddManager(ManagerDTO managerModel)
        {

            // Map the DTO to an Entity
            var managerEntity = _autoMapper.Map<ManagerEntity>(managerModel);

            // Initialize the Entity
            managerEntity.Intialize(true, "manager", "Nisarga", "Nisarga");

            // Add the entity to the database
            var response = await _cosmoDBService.Add(managerEntity);

            // Map the response back to a DTO
            return _autoMapper.Map<ManagerDTO>(response);
        }

        public async Task<ManagerDTO> GetManagerById(string id)
        {
            var security = await _cosmoDBService.GetManagerById(id); // Call non-generic method
            return _autoMapper.Map<ManagerDTO>(security);
        }

        public async Task<ManagerDTO> UpdateManager(string id, ManagerDTO managerModel)
        {
            var managerEntity = await _cosmoDBService.GetManagerById(id);
            if (managerEntity == null)
            {
                throw new Exception("Manager not found");
            }
            managerEntity = _autoMapper.Map<ManagerEntity>(managerModel);
            managerEntity.Id = id;
            var response = await _cosmoDBService.Update(managerEntity);
            return _autoMapper.Map<ManagerDTO>(response);
        }

        public async Task DeleteManager(string id)
        {
            await _cosmoDBService.DeleteManager(id);
        }


    }
}