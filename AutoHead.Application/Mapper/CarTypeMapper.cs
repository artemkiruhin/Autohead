using AutoHead.Application.Dtos;
using AutoHead.Application.Mapper.Base;
using AutoHead.Core.Entities;

public class CarTypeMapper : IMapper<CarTypeEntity, CarTypeDto>
{
    private readonly IMapper<CarEntity, CarDto> _carMapper;

    public CarTypeMapper(IMapper<CarEntity, CarDto> carMapper)
    {
        _carMapper = carMapper;
    }

    public async Task<CarTypeEntity> ToDomain(CarTypeDto dto)
    {
        var carEntities = await Task.WhenAll(dto.Cars.Select(x => _carMapper.ToDomain(x)));
        return new CarTypeEntity()
        {
            Id = dto.Id,
            Name = dto.Name,
            Cars = carEntities.ToList()
        };
    }

    public async Task<CarTypeDto> ToDto(CarTypeEntity domain)
    {
        var carDtos = await Task.WhenAll(domain.Cars.Select(x => _carMapper.ToDto(x)));
        return new CarTypeDto()
        {
            Id = domain.Id,
            Name = domain.Name,
            Cars = carDtos.ToList()
        };
    }
}