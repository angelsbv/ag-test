using Ag.BLL.Interfaces;
using Ag.BLL.Models;
using Microsoft.EntityFrameworkCore;

namespace Ag.DAL.Repositories;

public class ClientRepository : BaseRepository<Client>, IClientRepository
{
    public ClientRepository(AppDbContext context) : base(context) { }

    public async Task<Client?> GetByCodeAsync(int code, params ClientSideLoad[] sideLoads)
    {
        var query = Context.Clients.AsNoTracking();

        if (sideLoads.Contains(ClientSideLoad.Card))
        {
            query = query.Include(c => c.Cards);
        }

        return await query.FirstOrDefaultAsync(c => c.Code == code);
    }
}