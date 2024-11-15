using Big2.Domain.Entities;
using Big2.Domain.SeedWorks;

namespace Big2.Infrastructure.Repositories;
public class PlayerRepository : IRepository<Player>
{
    public Task Delete(Player data, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Player?> FindById(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task Save(Player data, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
