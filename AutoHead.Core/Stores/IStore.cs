namespace AutoHead.Core.Stores;

public interface IStore <TEntity>
{
    Task Add(TEntity model);
    Task Delete(Guid id);
    Task Update(TEntity model);
    Task<TEntity?> GetById(Guid id);
    Task<IEnumerable<TEntity>> GetAll();
}