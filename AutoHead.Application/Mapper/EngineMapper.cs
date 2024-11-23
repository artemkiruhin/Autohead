using AutoHead.Application.Dtos;
using AutoHead.Application.Mapper.Base;
using AutoHead.Core.Entities;

public class EngineMapper : IMapper<EngineEntity, EngineDto>
{
    private readonly IMapper<CarEntity, CarDto> _carMapper;

    public EngineMapper(IMapper<CarEntity, CarDto> carMapper)
    {
        _carMapper = carMapper;
    }

    public async Task<EngineEntity> ToDomain(EngineDto dto)
    {
        var cars = await Task.WhenAll(dto.Cars.Select(x => _carMapper.ToDomain(x)));

        return new EngineEntity()
        {
            Id = dto.Id,
            HorsePower = dto.HorsePower,
            Name = dto.Name,
            Cars = cars.ToList()  // Convert array to List if necessary
        };
    }

    public async Task<EngineDto> ToDto(EngineEntity domain)
    {
        var cars = await Task.WhenAll(domain.Cars.Select(x => _carMapper.ToDto(x)));
        return new EngineDto()
        {
            Id = domain.Id,
            Name = domain.Name,
            HorsePower = domain.HorsePower,
            Cars = cars.ToList()  // Convert array to List if necessary
        };
    }
}