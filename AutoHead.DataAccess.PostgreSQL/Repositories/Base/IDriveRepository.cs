using AutoHead.Core.Entities;
using AutoHead.Core.Stores;

namespace AutoHead.DataAccess.PostgreSQL.Repositories.Base;

public interface IDriveRepository : IStore<DriveEntity>
{
    Task<DriveEntity?> GetByName(string name);
}