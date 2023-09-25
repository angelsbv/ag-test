using Ag.BLL.Models;

namespace Ag.BLL.Interfaces;

public interface IClientService
{
    Task<IEnumerable<Client>> GetAllAsync();
    Task<Client?> GetByCodeAsync(int code, params ClientSideLoad[] sideLoads);
    Task<Client> CreateAsync(Client newClient);
    Task<bool> UpdateAsync(int code, Client updatedClient);
    Task<bool> DeleteAsync(int code);
}
