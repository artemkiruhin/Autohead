using AutoHead.Application.Dtos;
using AutoHead.Application.Mapper.Base;
using AutoHead.Core.Entities;

public class CustomerMapper : IMapper<CustomerEntity, CustomerDto>
{
    private readonly IMapper<OrderEntity, OrderDto> _orderMapper;

    public CustomerMapper(IMapper<OrderEntity, OrderDto> orderMapper)
    {
        _orderMapper = orderMapper;
    }

    public async Task<CustomerEntity> ToDomain(CustomerDto dto)
    {
        var orders = await Task.WhenAll(dto.Orders.Select(x => _orderMapper.ToDomain(x)));

        return new CustomerEntity()
        {
            Id = dto.Id,
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone,
            Orders = orders.ToList()
        };
    }

    public async Task<CustomerDto> ToDto(CustomerEntity dto)
    {
        var orders = await Task.WhenAll(dto.Orders.Select(x => _orderMapper.ToDto(x)));

        return new CustomerDto()
        {
            Id = dto.Id,
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone,
            Orders = orders.ToList()
        };
    }
}