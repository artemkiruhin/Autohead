namespace AutoHead.Application.EntityServices.Base;

public interface ICrudAnonService<TEntity>
{
    Task Add(TEntity model);
    Task Delete(Guid id);
    Task Update(TEntity model);
    Task<TEntity?> GetById(Guid id);
    Task<IEnumerable<TEntity>> GetAll();
}