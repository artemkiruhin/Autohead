using AutoHead.Core.Entities;
using AutoHead.DataAccess.PostgreSQL.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AutoHead.DataAccess.PostgreSQL.Repositories;

public class ColorRepository : IColorRepository
{
    private readonly AppDbContext _context;

    public ColorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Add(ColorEntity model)
    {
        await _context.Colors.AddAsync(model);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var color = await _context.Colors.FindAsync(id);
        if (color is null) throw new KeyNotFoundException($"Color with guid {id} not found.");

        _context.Colors.Remove(color);
        await _context.SaveChangesAsync();
    }

    public async Task Update(ColorEntity model)
    {
        var color = await _context.Colors.FindAsync(model.Id);
        if (color is null) throw new KeyNotFoundException($"Color with guid {model.Id} not found.");
        
        _context.Entry(color).CurrentValues.SetValues(model);
        await _context.SaveChangesAsync();
    }

    public async Task<ColorEntity?> GetById(Guid id)
    {
        return await _context.Colors
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<ColorEntity>> GetAll()
    {
        return await _context.Colors
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ColorEntity?> GetByName(string name)
    {
        return await _context.Colors
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name);
    }
}