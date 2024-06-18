

    using AutoMapper;
    using WebApplication1.DTO;
    using WebApplication1.Entities;
    using WebApplication1.Interfaces;

    namespace WebApplication1.Services
    {
        public class OfficeService : IOfficeService
        {
            private readonly ICosmosDbService _cosmoDBService;
            private readonly IMapper _autoMapper;
            public OfficeService(ICosmosDbService cosmoDBService, IMapper mapper)
            {
                _cosmoDBService = cosmoDBService;
                _autoMapper = mapper;
            }

          
            public async Task<OfficeDTO> AddOffice(OfficeDTO officeModel)
            {
                // Map the DTO to an Entity
                var officeEntity = _autoMapper.Map<OfficeEntity>(officeModel);

                // Initialize the Entity
                officeEntity.Intialize(true, "security", "Nisarga", "Nisarga");

                // Add the entity to the database
                var response = await _cosmoDBService.Add(officeEntity);

                // Map the response back to a DTO
                return _autoMapper.Map<OfficeDTO>(response);
            }
            public async Task<OfficeDTO> GetOfficeById(string id)
            {
                var office = await _cosmoDBService.GetOfficeById(id); // Call non-generic method
                return _autoMapper.Map<OfficeDTO>(office);
            }

            public async Task<OfficeDTO> UpdateOffice(string id, OfficeDTO officeModel)
            {
                var officeEntity = await _cosmoDBService.GetOfficeById(id);
                if (officeEntity == null)
                {
                    throw new Exception("Office not found");
                }
                officeEntity = _autoMapper.Map<OfficeEntity>(officeModel);
                officeEntity.Id = id;
                var response = await _cosmoDBService.Update(officeEntity);
                return _autoMapper.Map<OfficeDTO>(response);
            }

            public async Task DeleteOffice(string id)
            {
                await _cosmoDBService.DeleteVisitor(id);
            }


            public async Task<OfficeDTO> LoginOfficeUser(string email, string password)
            {
                // Fetch the manager entity by email
                var officeUser = await _cosmoDBService.GetOfficeUserByEmail(email);

                if (officeUser == null || officeUser.Password != password)
                {
                    return null; // Credentials are invalid
                }

                // Map ManagerEntity to ManagerDTO
                var officeDto = new OfficeDTO
                {
                    Id = officeUser.Id,
                    Name = officeUser.Name,
                    Email = officeUser.Email,
                    // Add other properties here
                };

                return officeDto;
            }
        }
    }


