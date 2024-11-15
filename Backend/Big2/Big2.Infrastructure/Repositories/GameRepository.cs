using Big2.Domain.Entities;
using Big2.Domain.SeedWorks;

namespace Big2.Infrastructure.Repositories;
public class GameRepository : IRepository<Game>
{
    public Task Delete(Game data, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Game?> FindById(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task Save(Game data, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
