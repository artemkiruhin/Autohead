using AutoHead.Application.Dtos;
using AutoHead.Application.Mapper.Base;
using AutoHead.Core.Entities;
using AutoHead.DataAccess.PostgreSQL.Repositories.Base;

public class EmployeeMapper : IMapper<EmployeeEntity, EmployeeDto>
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeMapper(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<EmployeeEntity> ToDomain(EmployeeDto dto)
    {
        var empl = await _employeeRepository.GetById(dto.Id);
        if (empl is null) throw new KeyNotFoundException($"Employee with guid {dto.Id} not found");
        var password = empl.PasswordHash;

        return new EmployeeEntity()
        {
            Id = dto.Id,
            Username = dto.Username,
            PasswordHash = password,
            Hired = dto.Hired
        };
    }

    public async Task<EmployeeDto> ToDto(EmployeeEntity domain)
    {
        return new EmployeeDto()
        {
            Id = domain.Id,
            Username = domain.Username,
            Hired = domain.Hired
        };
    }
}