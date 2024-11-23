using AutoHead.Core.Entities;
using AutoHead.Core.Stores;

namespace AutoHead.DataAccess.PostgreSQL.Repositories.Base;

public interface ICarRepository : IStore<CarEntity>
{
    Task<IEnumerable<CarEntity>> GetByName(string name);
    Task<IEnumerable<CarEntity>> GetByReleased(DateTime? from, DateTime? to);
    Task<IEnumerable<CarEntity>> GetByPrice(decimal? from, decimal? to);
    Task<IEnumerable<CarEntity>> GetByManufacturerId(Guid manufacturer);
    Task<IEnumerable<CarEntity>> GetByColorId(Guid color);
    Task<IEnumerable<CarEntity>> GetByEngineId(Guid engine);
    Task<IEnumerable<CarEntity>> GetByDriveId(Guid drive);
    Task<IEnumerable<CarEntity>> GetByTypeId(Guid type);
}