using System.Threading.Tasks;
using WebApplication1.Entities;

namespace WebApplication1.Interfaces
{
    public interface ICosmosDbService
    {
        Task<Visitor> AddVisitor(Visitor visitor);
        Task<Visitor> GetVisitorById(string id);



        // -------Office Users -------- 
        Task<IEnumerable<T>> GetAll<T>();
        Task<Visitor> DeleteVisitor(string id);
        public Task<T> Update<T>(T entity);
       

        Task<OfficeEntity> GetOfficeById(string id);
        Task<OfficeEntity> GetOfficeUserByEmail(string email);
        Task DeleteOffice(string id);
        Task<T> Add<T>(T entity);


        Task<SecurityEntity> GetSecurityById(string id);
        Task<ManagerEntity> GetManagerById(string id);
        Task<SecurityEntity> GetSecurityUserByEmail(string email);
        Task DeleteManager(string id);
        Task DeleteSecurity(string id);
    }
}