namespace AutoHead.Application.EntityServices.Base;

public interface ICrudService<TEntity>
{
    Task Add(TEntity model, Guid senderId);
    Task Delete(Guid id, Guid senderId);
    Task Update(TEntity model, Guid senderId);
    Task<TEntity?> GetById(Guid id, Guid senderId);
    Task<IEnumerable<TEntity>> GetAll();
}