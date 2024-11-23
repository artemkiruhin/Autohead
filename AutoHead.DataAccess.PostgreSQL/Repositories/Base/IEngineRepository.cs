using AutoHead.Core.Entities;
using AutoHead.Core.Stores;

namespace AutoHead.DataAccess.PostgreSQL.Repositories.Base;

public interface IEngineRepository : IStore<EngineEntity>
{
    Task<EngineEntity?> GetByName(string name);
    Task<IEnumerable<EngineEntity>> GetByHorsePower(int? from, int? to);
}