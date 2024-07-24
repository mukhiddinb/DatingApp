namespace API.Interfaces;

public interface IBaseRepository<TEntity>
{
    Task<bool> SaveAllAsync();

    Task<IEnumerable<TEntity>> GetAllAsync();

    void Update(TEntity entity);
}
