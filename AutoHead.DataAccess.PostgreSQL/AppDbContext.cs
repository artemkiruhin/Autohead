using AutoHead.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoHead.DataAccess.PostgreSQL;

public class AppDbContext : DbContext
{
    public DbSet<CarEntity> Cars { get; set; } = null!;
    public DbSet<CarTypeEntity> CarTypes { get; set; } = null!;
    public DbSet<ColorEntity> Colors { get; set; } = null!;
    public DbSet<CustomerEntity> Customers { get; set; } = null!;
    public DbSet<DriveEntity> Drives { get; set; } = null!;
    public DbSet<EmployeeEntity> Employees { get; set; } = null!;
    public DbSet<EngineEntity> Engines { get; set; } = null!;
    public DbSet<ManufacturerEntity> Manufacturers { get; set; } = null!;
    public DbSet<OrderEntity> Orders { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=localhost;Database=autohead;Username=postgres;Password=root");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CarTypeEntity>(options =>
        {
            options.HasKey(x => x.Id);
            options.Property(x => x.Name).IsRequired();
            options.HasIndex(x => x.Name).IsUnique();
            options
                .HasMany(x => x.Cars)
                .WithOne(x => x.CarType)
                .HasForeignKey(x => x.TypeId);
        });
        modelBuilder.Entity<ColorEntity>(options =>
        {
            options.HasKey(x => x.Id);
            options.Property(x => x.Name).IsRequired();
            options.HasIndex(x => x.Name).IsUnique();
            options
                .HasMany(x => x.Cars)
                .WithOne(x => x.Color)
                .HasForeignKey(x => x.ColorId);
        });
        modelBuilder.Entity<ManufacturerEntity>(options =>
        {
            options.HasKey(x => x.Id);
            options.Property(x => x.Name).IsRequired();
            options.HasIndex(x => x.Name).IsUnique();
            options
                .HasMany(x => x.Cars)
                .WithOne(x => x.Manufacturer)
                .HasForeignKey(x => x.ManufacturerId);
        });
        modelBuilder.Entity<DriveEntity>(options =>
        {
            options.HasKey(x => x.Id);
            options.Property(x => x.Name).IsRequired();
            options.HasIndex(x => x.Name).IsUnique();
            options
                .HasMany(x => x.Cars)
                .WithOne(x => x.Drive)
                .HasForeignKey(x => x.DriveId);
        });
        modelBuilder.Entity<EngineEntity>(options =>
        {
            options.HasKey(x => x.Id);
            options.Property(x => x.Name).IsRequired();
            options
                .HasMany(x => x.Cars)
                .WithOne(x => x.Engine)
                .HasForeignKey(x => x.EngineId);
        });
        modelBuilder.Entity<CustomerEntity>(options =>
        {
            options.HasKey(x => x.Id);
            options.Property(x => x.Name).IsRequired();
            options.Property(x => x.Email).IsRequired();
            options.Property(x => x.Phone).IsRequired();

            options.HasIndex(x => x.Email).IsUnique();
            options.HasIndex(x => x.Phone).IsUnique();

            options
                .HasMany(x => x.Orders)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId);
        });
        modelBuilder.Entity<OrderEntity>(options =>
        {
            options.HasKey(x => x.Id);

            options
                .HasOne(x => x.Customer)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CustomerId);
            
            
            options
                .HasOne(x => x.Car)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CarId);
        });
        
        
        modelBuilder.Entity<CarEntity>(options =>
        {
            options.HasKey(x => x.Id);
            options.Property(x => x.Name).IsRequired();
            options.HasIndex(x => x.Name).IsUnique();

            options
                .HasOne(x => x.CarType)
                .WithMany(x => x.Cars)
                .HasForeignKey(x => x.TypeId);
            options
                .HasOne(x => x.Color)
                .WithMany(x => x.Cars)
                .HasForeignKey(x => x.ColorId);
            options
                .HasOne(x => x.Engine)
                .WithMany(x => x.Cars)
                .HasForeignKey(x => x.EngineId);
            options
                .HasOne(x => x.Drive)
                .WithMany(x => x.Cars)
                .HasForeignKey(x => x.DriveId);
            options
                .HasOne(x => x.Manufacturer)
                .WithMany(x => x.Cars)
                .HasForeignKey(x => x.ManufacturerId);
            options
                .HasMany(x => x.Orders)
                .WithOne(x => x.Car)
                .HasForeignKey(x => x.CarId);
        });
        
    }
}