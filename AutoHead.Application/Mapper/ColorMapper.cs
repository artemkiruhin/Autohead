using AutoHead.Application.Dtos;
using AutoHead.Application.Mapper.Base;
using AutoHead.Core.Entities;

public class ColorMapper : IMapper<ColorEntity, ColorDto>
{
    private readonly IMapper<CarEntity, CarDto> _carMapper;

    public ColorMapper(IMapper<CarEntity, CarDto> carMapper)
    {
        _carMapper = carMapper;
    }

    public async Task<ColorEntity> ToDomain(ColorDto dto)
    {
        var cars = await Task.WhenAll(dto.Cars.Select(x => _carMapper.ToDomain(x)));

        return new ColorEntity()
        {
            Id = dto.Id,
            Name = dto.Name,
            Cars = cars.ToList()  // Convert array to List if necessary
        };
    }

    public async Task<ColorDto> ToDto(ColorEntity domain)
    {
        var cars = await Task.WhenAll(domain.Cars.Select(x => _carMapper.ToDto(x)));
        return new ColorDto()
        {
            Id = domain.Id,
            Name = domain.Name,
            Cars = cars.ToList()  // Convert array to List if necessary
        };
    }
}