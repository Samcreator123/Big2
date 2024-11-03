namespace Big2.Domain.SeedWorks;
public interface IRepository<T> where T : class
{
    Task<T?> FindById(Guid id, CancellationToken cancellationToken = default);

    Task Save(T data, CancellationToken cancellationToken = default);

    Task Delete(T data, CancellationToken cancellationToken = default);
}
