using AutoHead.Application.Dtos;
using AutoHead.Application.Mapper.Base;
using AutoHead.Core.Entities;
using AutoHead.DataAccess.PostgreSQL.Repositories.Base;

public class OrderMapper : IMapper<OrderEntity, OrderDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICarRepository _carRepository;

    public OrderMapper(ICustomerRepository customerRepository, ICarRepository carRepository)
    {
        _customerRepository = customerRepository;
        _carRepository = carRepository;
    }

    public async Task<OrderEntity> ToDomain(OrderDto dto)
    {
        var customer = await _customerRepository.GetById(dto.CustomerId);
        var car = await _carRepository.GetById(dto.CarId);

        if (customer == null)
            throw new KeyNotFoundException($"Customer with ID {dto.CustomerId} not found.");
        if (car == null)
            throw new KeyNotFoundException($"Car with ID {dto.CarId} not found.");

        return new OrderEntity()
        {
            Id = dto.Id,
            CustomerId = dto.CustomerId,
            Customer = customer,
            CarId = dto.CarId,
            Car = car,
            Created = dto.Created,
            Details = string.IsNullOrEmpty(dto.Details) ? null : dto.Details
        };
    }

    public async Task<OrderDto> ToDto(OrderEntity domain)
    {
        return new OrderDto()
        {
            Id = domain.Id,
            CustomerId = domain.CustomerId,
            CustomerName = domain.Customer.Name,
            CustomerEmail = domain.Customer.Email,
            CustomerPhone = domain.Customer.Phone,
            CarId = domain.CarId,
            CarName = domain.Car.Name,
            ManufacturerName = domain.Car.Manufacturer.Name,
            Price = domain.Car.Price,
            ColorName = domain.Car.Color.Name,
            Created = domain.Created,
            Details = domain.Details ?? string.Empty
        };
    }
}