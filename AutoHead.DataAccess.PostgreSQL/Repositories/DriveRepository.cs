using AutoHead.Core.Entities;
using AutoHead.DataAccess.PostgreSQL.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AutoHead.DataAccess.PostgreSQL.Repositories;

public class DriveRepository : IDriveRepository
{
    private readonly AppDbContext _context;

    public DriveRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Add(DriveEntity model)
    {
        await _context.Drives.AddAsync(model);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var drive = await _context.Drives.FindAsync(id);
        if (drive is null) throw new KeyNotFoundException($"Drive with guid {id} not found.");

        _context.Drives.Remove(drive);
        await _context.SaveChangesAsync();
    }

    public async Task Update(DriveEntity model)
    {
        var drive = await _context.Drives.FindAsync(model.Id);
        if (drive is null) throw new KeyNotFoundException($"Drive with guid {model.Id} not found.");
        
        _context.Entry(drive).CurrentValues.SetValues(model);
        await _context.SaveChangesAsync();
    }

    public async Task<DriveEntity?> GetById(Guid id)
    {
        return await _context.Drives
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<DriveEntity>> GetAll()
    {
        return await _context.Drives
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<DriveEntity?> GetByName(string name)
    {
        return await _context.Drives
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name);
    }
}