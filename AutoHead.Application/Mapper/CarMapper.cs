using AutoHead.Application.Dtos;
using AutoHead.Application.Mapper.Base;
using AutoHead.Core.Entities;
using AutoHead.DataAccess.PostgreSQL.Repositories.Base;

public class CarMapper : IMapper<CarEntity, CarDto>
{
    private readonly IManufacturerRepository _manufacturerRepository;
    private readonly IEngineRepository _engineRepository;
    private readonly IDriveRepository _driveRepository;
    private readonly IColorRepository _colorRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly ICarTypeRepository _carTypeRepository;
    private readonly IMapper<OrderEntity, OrderDto> _orderMapper;

    public CarMapper(
        IManufacturerRepository manufacturerRepository,
        IEngineRepository engineRepository,
        IDriveRepository driveRepository,
        IColorRepository colorRepository,
        IOrderRepository orderRepository,
        ICarTypeRepository carTypeRepository,
        IMapper<OrderEntity, OrderDto> orderMapper)
    {
        _manufacturerRepository = manufacturerRepository;
        _engineRepository = engineRepository;
        _driveRepository = driveRepository;
        _colorRepository = colorRepository;
        _orderRepository = orderRepository;
        _carTypeRepository = carTypeRepository;
        _orderMapper = orderMapper;
    }

    public async Task<CarEntity> ToDomain(CarDto dto)
    {
        var carType = await _carTypeRepository.GetById(dto.TypeId) 
                      ?? throw new KeyNotFoundException($"Car type with ID {dto.TypeId} not found.");
        var manufacturer = await _manufacturerRepository.GetById(dto.ManufacturerId) 
                           ?? throw new KeyNotFoundException($"Manufacturer with ID {dto.ManufacturerId} not found.");
        var engine = await _engineRepository.GetById(dto.EngineId) 
                     ?? throw new KeyNotFoundException($"Engine with ID {dto.EngineId} not found.");
        var color = await _colorRepository.GetById(dto.ColorId) 
                    ?? throw new KeyNotFoundException($"Color with ID {dto.ColorId} not found.");
        var orders = await Task.WhenAll(dto.Orders.Select(x => _orderMapper.ToDomain(x)));

        return new CarEntity()
        {
            Id = dto.Id,
            Name = dto.Name,
            Released = dto.Released,
            TimeTo100 = dto.TimeTo100,
            TimeTo200 = dto.TimeTo200,
            Price = dto.Price,
            TypeId = dto.TypeId,
            CarType = carType,
            ManufacturerId = dto.ManufacturerId,
            Manufacturer = manufacturer,
            EngineId = dto.EngineId,
            Engine = engine,
            ColorId = dto.ColorId,
            Color = color,
            Orders = orders.ToList(),
            DriveId = dto.DriveId,
            Drive = await _driveRepository.GetById(dto.DriveId) 
                    ?? throw new KeyNotFoundException($"Drive with ID {dto.DriveId} not found.")
        };
    }

    public async Task<CarDto> ToDto(CarEntity domain)
    {
        var orders = await Task.WhenAll(domain.Orders.Select(x => _orderMapper.ToDto(x)));

        return new CarDto()
        {
            Id = domain.Id,
            Name = domain.Name,
            Released = domain.Released,
            TimeTo100 = domain.TimeTo100,
            TimeTo200 = domain.TimeTo200,
            Price = domain.Price,
            TypeId = domain.TypeId,
            CarTypeName = domain.CarType.Name,
            ManufacturerId = domain.ManufacturerId,
            ManufacturerName = domain.Manufacturer.Name,
            EngineId = domain.EngineId,
            EngineName = domain.Engine.Name,
            HorsePower = domain.Engine.HorsePower,
            DriveId = domain.DriveId,
            DriveName = domain.Drive.Name,
            ColorId = domain.ColorId,
            ColorName = domain.Color.Name,
            Orders = orders.ToList()
        };
    }
}