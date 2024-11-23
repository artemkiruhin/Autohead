using AutoHead.Core.Entities;
using AutoHead.DataAccess.PostgreSQL.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AutoHead.DataAccess.PostgreSQL.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Add(CustomerEntity model)
    {
        await _context.Customers.AddAsync(model);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer is null) throw new KeyNotFoundException($"Customer with guid {id} not found.");

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }

    public async Task Update(CustomerEntity model)
    {
        var customer = await _context.Customers.FindAsync(model.Id);
        if (customer is null) throw new KeyNotFoundException($"Customer with guid {model.Id} not found.");
        
        _context.Entry(customer).CurrentValues.SetValues(model);
        await _context.SaveChangesAsync();
    }

    public async Task<CustomerEntity?> GetById(Guid id)
    {
        return await _context.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<CustomerEntity>> GetAll()
    {
        return await _context.Customers
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<CustomerEntity>> GetByName(string name)
    {
        return await _context.Customers
            .AsNoTracking()
            .Where(x => x.Name.Contains(name))
            .ToListAsync();
    }

    public async Task<IEnumerable<CustomerEntity>> GetByEmail(string email)
    {
        return await _context.Customers
            .AsNoTracking()
            .Where(x => x.Email.Contains(email))
            .ToListAsync();
    }

    public async Task<IEnumerable<CustomerEntity>> GetByPhone(string phone)
    {
        return await _context.Customers
            .AsNoTracking()
            .Where(x => x.Phone.Contains(phone))
            .ToListAsync();
    }
}