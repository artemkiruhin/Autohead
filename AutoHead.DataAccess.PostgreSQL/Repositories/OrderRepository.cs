using AutoHead.Core.Entities;
using AutoHead.DataAccess.PostgreSQL.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AutoHead.DataAccess.PostgreSQL.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Add(OrderEntity model)
    {
        await _context.Orders.AddAsync(model);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order is null) throw new KeyNotFoundException($"Order with guid {id} not found.");

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }

    public async Task Update(OrderEntity model)
    {
        var order = await _context.Orders.FindAsync(model.Id);
        if (order is null) throw new KeyNotFoundException($"Order with guid {model.Id} not found.");
        
        _context.Entry(order).CurrentValues.SetValues(model);
        await _context.SaveChangesAsync();
    }

    public async Task<OrderEntity?> GetById(Guid id)
    {
        return await _context.Orders.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<OrderEntity>> GetAll()
    {
        return await _context.Orders.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<OrderEntity>> GetByCreated(DateTime? from, DateTime? to)
    {
        if (from is null && to is null) 
            throw new NullReferenceException("Both parameters are null");

        var fromQuery = from ?? DateTime.MinValue;
        var toQuery = to ?? DateTime.MaxValue;

        return await _context.Orders
            .AsNoTracking()
            .Where(x => x.Created >= fromQuery && x.Created <= toQuery)
            .ToListAsync();
    }

    public async Task<IEnumerable<OrderEntity>> GetByCustomerId(Guid customer)
    {
        return await _context.Orders.AsNoTracking().Where(x => x.CustomerId == customer).ToListAsync();
    }

    public async Task<IEnumerable<OrderEntity>> GetByCarId(Guid car)
    {
        return await _context.Orders.AsNoTracking().Where(x => x.CarId == car).ToListAsync();
    }
}