using AutoHead.Core.Entities;
using AutoHead.Core.Stores;

namespace AutoHead.DataAccess.PostgreSQL.Repositories.Base;

public interface IEmployeeRepository : IStore<EmployeeEntity>
{
    Task<EmployeeEntity?> GetByUsername(string username);
}