using AutoHead.Core.Entities;
using AutoHead.DataAccess.PostgreSQL.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AutoHead.DataAccess.PostgreSQL.Repositories;

public class EngineRepository : IEngineRepository
{
    private readonly AppDbContext _context;

    public EngineRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Add(EngineEntity model)
    {
        await _context.Engines.AddAsync(model);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var engine = await _context.Engines.FindAsync(id);
        if (engine is null) throw new KeyNotFoundException($"Engine with guid {id} not found.");

        _context.Engines.Remove(engine);
        await _context.SaveChangesAsync();
    }

    public async Task Update(EngineEntity model)
    {
        var engine = await _context.Engines.FindAsync(model.Id);
        if (engine is null) throw new KeyNotFoundException($"Engine with guid {model.Id} not found.");
        
        _context.Entry(engine).CurrentValues.SetValues(model);
        await _context.SaveChangesAsync();
    }

    public async Task<EngineEntity?> GetById(Guid id)
    {
        return await _context.Engines
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<EngineEntity>> GetAll()
    {
        return await _context.Engines
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<EngineEntity?> GetByName(string name)
    {
        return await _context.Engines
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<IEnumerable<EngineEntity>> GetByHorsePower(int? from, int? to)
    {
        if (from is null && to is null) 
            throw new NullReferenceException("Both parameters are null");

        var fromQuery = from ?? 0;
        var toQuery = to ?? int.MaxValue;

        return await _context.Engines
            .AsNoTracking()
            .Where(x => x.HorsePower >= fromQuery && x.HorsePower <= toQuery)
            .ToListAsync();
    }
}