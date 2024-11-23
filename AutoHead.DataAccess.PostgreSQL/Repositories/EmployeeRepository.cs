using AutoHead.Core.Entities;
using AutoHead.DataAccess.PostgreSQL.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AutoHead.DataAccess.PostgreSQL.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Add(EmployeeEntity model)
    {
        await _context.Employees.AddAsync(model);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee is null) throw new KeyNotFoundException($"Employee with guid {id} not found.");

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
    }

    public async Task Update(EmployeeEntity model)
    {
        var employee = await _context.Employees.FindAsync(model.Id);
        if (employee is null) throw new KeyNotFoundException($"Employee with guid {model.Id} not found.");
        
        _context.Entry(employee).CurrentValues.SetValues(model);
        await _context.SaveChangesAsync();
    }

    public async Task<EmployeeEntity?> GetById(Guid id)
    {
        return await _context.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<EmployeeEntity>> GetAll()
    {
        return await _context.Employees
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<EmployeeEntity?> GetByUsername(string username)
    {
        return await _context.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Username == username);
    }
}