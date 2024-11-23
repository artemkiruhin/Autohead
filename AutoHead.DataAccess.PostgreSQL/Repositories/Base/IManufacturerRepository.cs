using AutoHead.Core.Entities;
using AutoHead.Core.Stores;

namespace AutoHead.DataAccess.PostgreSQL.Repositories.Base;

public interface IManufacturerRepository : IStore<ManufacturerEntity>
{
    Task<ManufacturerEntity?> GetByName(string name);
}