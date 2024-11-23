using AutoHead.Core.Entities;
using AutoHead.Core.Stores;

namespace AutoHead.DataAccess.PostgreSQL.Repositories.Base;

public interface ICarTypeRepository : IStore<CarTypeEntity>
{
    Task<CarTypeEntity?> GetByName(string name);
}