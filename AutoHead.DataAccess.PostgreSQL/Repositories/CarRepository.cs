using AutoHead.Core.Entities;
using AutoHead.DataAccess.PostgreSQL.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AutoHead.DataAccess.PostgreSQL.Repositories;

public class CarRepository : ICarRepository
{
    private readonly AppDbContext _context;

    public CarRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Add(CarEntity model)
    {
        await _context.Cars.AddAsync(model);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car is null) throw new KeyNotFoundException($"Car with guid {id} not found.");

        _context.Cars.Remove(car);
        await _context.SaveChangesAsync();
    }

    public async Task Update(CarEntity model)
    {
        var car = await _context.Cars.FindAsync(model.Id);
        if (car is null) throw new KeyNotFoundException($"Car with guid {model.Id} not found.");
        
        _context.Entry(car).CurrentValues.SetValues(model);
        await _context.SaveChangesAsync();
    }

    public async Task<CarEntity?> GetById(Guid id)
    {
        return await _context.Cars
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<CarEntity>> GetAll()
    {
        return await _context.Cars
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<CarEntity>> GetByName(string name)
    {
        return await _context.Cars
            .AsNoTracking()
            .Where(x => x.Name.Contains(name))
            .ToListAsync();
    }

    public async Task<IEnumerable<CarEntity>> GetByReleased(DateTime? from, DateTime? to)
    {
        if (from is null && to is null) 
            throw new NullReferenceException("Both parameters are null");

        var fromQuery = from ?? DateTime.MinValue;
        var toQuery = to ?? DateTime.MaxValue;

        return await _context.Cars
            .AsNoTracking()
            .Where(x => x.Released >= fromQuery && x.Released <= toQuery)
            .ToListAsync();
    }

    public async Task<IEnumerable<CarEntity>> GetByPrice(decimal? from, decimal? to)
    {
        if (from is null && to is null) 
            throw new NullReferenceException("Both parameters are null");

        var fromQuery = from ?? 0;
        var toQuery = to ?? decimal.MaxValue;

        return await _context.Cars
            .AsNoTracking()
            .Where(x => x.Price >= fromQuery && x.Price <= toQuery)
            .ToListAsync();
    }

    public async Task<IEnumerable<CarEntity>> GetByManufacturerId(Guid manufacturer)
    {
        return await _context.Cars
            .AsNoTracking()
            .Where(x => x.ManufacturerId == manufacturer)
            .ToListAsync();
    }

    public async Task<IEnumerable<CarEntity>> GetByColorId(Guid color)
    {
        return await _context.Cars
            .AsNoTracking()
            .Where(x => x.ColorId == color)
            .ToListAsync();
    }

    public async Task<IEnumerable<CarEntity>> GetByEngineId(Guid engine)
    {
        return await _context.Cars
            .AsNoTracking()
            .Where(x => x.EngineId == engine)
            .ToListAsync();
    }

    public async Task<IEnumerable<CarEntity>> GetByDriveId(Guid drive)
    {
        return await _context.Cars
            .AsNoTracking()
            .Where(x => x.DriveId == drive)
            .ToListAsync();
    }

    public async Task<IEnumerable<CarEntity>> GetByTypeId(Guid type)
    {
        return await _context.Cars
            .AsNoTracking()
            .Where(x => x.TypeId == type)
            .ToListAsync();
    }
}