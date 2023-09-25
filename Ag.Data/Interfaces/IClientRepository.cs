using Ag.BLL.Models;

namespace Ag.BLL.Interfaces;

public interface IClientRepository : IRepository<Client>
{
    Task<Client?> GetByCodeAsync(int code, params ClientSideLoad[] sideLoads);
}