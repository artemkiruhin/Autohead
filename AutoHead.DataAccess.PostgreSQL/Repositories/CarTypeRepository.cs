using AutoHead.Core.Entities;
using AutoHead.DataAccess.PostgreSQL.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AutoHead.DataAccess.PostgreSQL.Repositories;

public class CarTypeRepository : ICarTypeRepository
{
    private readonly AppDbContext _context;

    public CarTypeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Add(CarTypeEntity model)
    {
        await _context.CarTypes.AddAsync(model);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var type = await _context.CarTypes.FindAsync(id);
        if (type is null) throw new KeyNotFoundException($"Car type with guid {id} not found.");

        _context.CarTypes.Remove(type);
        await _context.SaveChangesAsync();
    }

    public async Task Update(CarTypeEntity model)
    {
        var type = await _context.CarTypes.FindAsync(model.Id);
        if (type is null) throw new KeyNotFoundException($"Car type with guid {model.Id} not found.");
        
        _context.Entry(type).CurrentValues.SetValues(model);
        await _context.SaveChangesAsync();
    }

    public async Task<CarTypeEntity?> GetById(Guid id)
    {
        return await _context.CarTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<CarTypeEntity>> GetAll()
    {
        return await _context.CarTypes
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<CarTypeEntity?> GetByName(string name)
    {
        return await _context.CarTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name);
    }
}