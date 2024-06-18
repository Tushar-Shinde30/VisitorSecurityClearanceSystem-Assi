using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System.Threading.Tasks;
using WebApplication1.Common;
using WebApplication1.Entities;
using WebApplication1.Interfaces;

namespace WebApplication1.CosmosDB
{
    public class CosmosDbService : ICosmosDbService
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public CosmosDbService()
        {
            _cosmosClient = new CosmosClient(Credentials.CosmosEndpoint, Credentials.PrimaryKey);
            _container = _cosmosClient.GetContainer(Credentials.DatabaseName, Credentials.ContainerName);
        }

        public async Task<Visitor> AddVisitor(Visitor visitor)
        {
            var response = await _container.CreateItemAsync(visitor);
            return response.Resource;
        }
        public async Task<Visitor> GetVisitorById(string id)
        {
            try
            {
                var query = _container.GetItemLinqQueryable<Visitor>(true)
                                      .Where(q => q.Id == id && q.Active && !q.Archived)
                                      .FirstOrDefault();

                return query;
            }
            catch (CosmosException ex)
            {
                Console.WriteLine($"Cosmos DB Exception: {ex.Message}");
                throw; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetVisitorById: {ex.Message}");
                throw; 
            }
        }


        public async Task<IEnumerable<T>> GetAll<T>()
        {
            var query = _container.GetItemQueryIterator<T>();
            var results = new List<T>();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response);
            }

            return results;
        }
        public async Task<Visitor>DeleteVisitor(string id)
        {
            var visitor = await GetVisitorById(id);
            if (visitor != null)
            {
                visitor.Active = false;
                visitor.Archived = true;
                await Update(visitor);
            }
            return visitor;
        }

        public async Task<OfficeEntity> GetOfficeById(string id)
        {
            try
            {
                var query = _container.GetItemLinqQueryable<OfficeEntity>(true)
                                      .Where(q => q.Id == id && q.Active && !q.Archived)
                                      .FirstOrDefault();



                return query;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<T> Update<T>(T data)
        {
            var response = await _container.UpsertItemAsync(data);
            return response.Resource;
        }
        public async Task<T> Add<T>(T data)
        {
            var response = await _container.CreateItemAsync(data);
            return response.Resource;
        }

        public async Task DeleteOffice(string id)
        {
            var office = await GetOfficeById(id);
            if (office != null)
            {
                office.Active = false;
                office.Archived = true;
                await Update(office);
            }
        }

        public async Task<OfficeEntity> GetOfficeUserByEmail(string email)
        {
            var query = _container.GetItemLinqQueryable<OfficeEntity>(true)
                                  .Where(q => q.Email == email && q.Active && !q.Archived)
                                  .ToFeedIterator();

            while (query.HasMoreResults)
            {
                var resultSet = await query.ReadNextAsync();
                var officeUser = resultSet.FirstOrDefault();
                if (officeUser != null)
                {
                    return officeUser;
                }
            }

            return null;
        }

        public async Task DeleteSecurity(string id)
        {
            var security = await GetSecurityById(id);
            if (security != null)
            {
                security.Active = false;
                security.Archived = true;
                await Update(security);
            }
        }
   
        public async Task<SecurityEntity> GetSecurityUserByEmail(string email)
        {
            var query = _container.GetItemLinqQueryable<SecurityEntity>(true)
                                  .Where(q => q.Email == email && q.Active && !q.Archived)
                                  .ToFeedIterator();

            while (query.HasMoreResults)
            {
                var resultSet = await query.ReadNextAsync();
                var security = resultSet.FirstOrDefault();
                if (security != null)
                {
                    return security;
                }
            }

            return null;
        }
       
        public async Task<SecurityEntity> GetSecurityById(string id)
        {
            try
            {
                var query = _container.GetItemLinqQueryable<SecurityEntity>(true)
                                      .Where(q => q.Id == id && q.Active && !q.Archived)
                                      .FirstOrDefault();



                return query;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<ManagerEntity> GetManagerById(string id)
        {
            try
            {
                var query = _container.GetItemLinqQueryable<ManagerEntity>(true)
                                      .Where(q => q.Id == id && q.Active && !q.Archived)
                                      .FirstOrDefault();



                return query;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }
        public async Task DeleteManager(string id)
        {
            var manager = await GetManagerById(id);
            if (manager != null)
            {

                manager.Active = false;
                manager.Archived = true;
                await Update(manager);
            }
        }

    }
}