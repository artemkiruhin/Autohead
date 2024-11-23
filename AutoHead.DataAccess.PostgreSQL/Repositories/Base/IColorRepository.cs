using AutoHead.Core.Entities;
using AutoHead.Core.Stores;

namespace AutoHead.DataAccess.PostgreSQL.Repositories.Base;

public interface IColorRepository : IStore<ColorEntity>
{
    Task<ColorEntity?> GetByName(string name);
}