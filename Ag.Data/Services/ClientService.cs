using Ag.BLL.Interfaces;
using Ag.BLL.Models;

namespace Ag.BLL.Services
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _unitOfWork.Clients.GetAllAsync();
        }

        public async Task<Client?> GetByCodeAsync(int code, params ClientSideLoad[] sideLoads)
        {
            return await _unitOfWork.Clients.GetByCodeAsync(code, sideLoads);
        }

        public async Task<Client> CreateAsync(Client newClient)
        {
            return await _unitOfWork.Clients.AddAsync(newClient);
        }

        public async Task<bool> UpdateAsync(int code, Client updatedClient)
        {
            var existingClient = await _unitOfWork.Clients.GetByCodeAsync(code);
            if (existingClient == null)
                return false;

            existingClient.FirstName = updatedClient.FirstName;
            existingClient.LastName = updatedClient.LastName;
            existingClient.ContactNumber = updatedClient.ContactNumber;
            existingClient.Occupation = updatedClient.Occupation;

            return await _unitOfWork.Clients.UpdateAsync(existingClient);
        }

        public async Task<bool> DeleteAsync(int code)
        {
            var existingClient = await _unitOfWork.Clients.GetByCodeAsync(code);
            if (existingClient == null)
                return false;

            return await _unitOfWork.Clients.DeleteAsync(code);
        }
    }
}
