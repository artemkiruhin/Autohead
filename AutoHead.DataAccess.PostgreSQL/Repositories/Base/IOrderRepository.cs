using AutoHead.Core.Entities;
using AutoHead.Core.Stores;

namespace AutoHead.DataAccess.PostgreSQL.Repositories.Base;

public interface IOrderRepository : IStore<OrderEntity>
{
    Task<IEnumerable<OrderEntity>> GetByCreated(DateTime? from, DateTime? to);
    Task<IEnumerable<OrderEntity>> GetByCustomerId(Guid customer);
    Task<IEnumerable<OrderEntity>> GetByCarId(Guid car);
}