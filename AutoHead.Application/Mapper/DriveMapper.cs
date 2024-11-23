using AutoHead.Application.Dtos;
using AutoHead.Application.Mapper.Base;
using AutoHead.Core.Entities;

public class DriveMapper : IMapper<DriveEntity, DriveDto>
{
    private readonly IMapper<CarEntity, CarDto> _carMapper;

    public DriveMapper(IMapper<CarEntity, CarDto> carMapper)
    {
        _carMapper = carMapper;
    }

    public async Task<DriveEntity> ToDomain(DriveDto dto)
    {
        var cars = await Task.WhenAll(dto.Cars.Select(x => _carMapper.ToDomain(x)));

        return new DriveEntity()
        {
            Id = dto.Id,
            Name = dto.Name,
            Cars = cars.ToList()  // Convert array to List if necessary
        };
    }

    public async Task<DriveDto> ToDto(DriveEntity domain)
    {
        var cars = await Task.WhenAll(domain.Cars.Select(x => _carMapper.ToDto(x)));
        return new DriveDto()
        {
            Id = domain.Id,
            Name = domain.Name,
            Cars = cars.ToList()  // Convert array to List if necessary
        };
    }
}