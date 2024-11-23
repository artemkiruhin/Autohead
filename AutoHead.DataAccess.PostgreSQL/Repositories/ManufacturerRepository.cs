using AutoHead.Core.Entities;
using AutoHead.DataAccess.PostgreSQL.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AutoHead.DataAccess.PostgreSQL.Repositories;

public class ManufacturerRepository : IManufacturerRepository
{
    private readonly AppDbContext _context;

    public ManufacturerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Add(ManufacturerEntity model)
    {
        await _context.Manufacturers.AddAsync(model);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var manufacturer = await _context.Manufacturers.FindAsync(id);
        if (manufacturer is null) throw new KeyNotFoundException($"Manufacturer with guid {id} not found.");

        _context.Manufacturers.Remove(manufacturer);
        await _context.SaveChangesAsync();
    }

    public async Task Update(ManufacturerEntity model)
    {
        var manufacturer = await _context.Manufacturers.FindAsync(model.Id);
        if (manufacturer is null) throw new KeyNotFoundException($"Manufacturer with guid {model.Id} not found.");
        
        _context.Entry(manufacturer).CurrentValues.SetValues(model);
        await _context.SaveChangesAsync();
    }

    public async Task<ManufacturerEntity?> GetById(Guid id)
    {
        return await _context.Manufacturers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<ManufacturerEntity>> GetAll()
    {
        return await _context.Manufacturers.AsNoTracking().ToListAsync();
    }

    public async Task<ManufacturerEntity?> GetByName(string name)
    {
        return await _context.Manufacturers.AsNoTracking().FirstOrDefaultAsync(x => x.Name.Contains(name));
    }
}