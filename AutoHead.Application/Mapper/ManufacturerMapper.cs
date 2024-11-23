using AutoHead.Application.Dtos;
using AutoHead.Application.Mapper.Base;
using AutoHead.Core.Entities;

public class ManufacturerMapper : IMapper<ManufacturerEntity, ManufacturerDto>
{
    private readonly IMapper<CarEntity, CarDto> _carMapper;

    public ManufacturerMapper(IMapper<CarEntity, CarDto> carMapper)
    {
        _carMapper = carMapper;
    }

    public async Task<ManufacturerEntity> ToDomain(ManufacturerDto dto)
    {
        var cars = await Task.WhenAll(dto.Cars.Select(x => _carMapper.ToDomain(x)));

        return new ManufacturerEntity()
        {
            Id = dto.Id,
            Name = dto.Name,
            Cars = cars.ToList()  // Convert array to List if necessary
        };
    }

    public async Task<ManufacturerDto> ToDto(ManufacturerEntity domain)
    {
        var cars = await Task.WhenAll(domain.Cars.Select(x => _carMapper.ToDto(x)));
        return new ManufacturerDto()
        {
            Id = domain.Id,
            Name = domain.Name,
            Cars = cars.ToList()  // Convert array to List if necessary
        };
    }
}