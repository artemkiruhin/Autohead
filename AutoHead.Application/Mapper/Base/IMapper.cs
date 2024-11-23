namespace AutoHead.Application.Mapper.Base;

public interface IMapper<TDomain, TDto>
{
    Task<TDomain> ToDomain(TDto dto);
    Task<TDto> ToDto(TDomain dto);
}