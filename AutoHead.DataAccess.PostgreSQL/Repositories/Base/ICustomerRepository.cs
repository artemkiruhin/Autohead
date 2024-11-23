using AutoHead.Core.Entities;
using AutoHead.Core.Stores;

namespace AutoHead.DataAccess.PostgreSQL.Repositories.Base;

public interface ICustomerRepository : IStore<CustomerEntity>
{
    Task<IEnumerable<CustomerEntity>> GetByName(string name);
    Task<IEnumerable<CustomerEntity>> GetByEmail(string email);
    Task<IEnumerable<CustomerEntity>> GetByPhone(string phone);
}