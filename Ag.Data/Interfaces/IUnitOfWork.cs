using Ag.BLL.Models;

namespace Ag.BLL.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IClientRepository Clients { get; }
    IRepository<Card> Cards { get; }

    Task<int> SaveAsync();
}
