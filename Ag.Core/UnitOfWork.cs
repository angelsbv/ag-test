using Ag.BLL.Interfaces;
using Ag.BLL.Models;
using Ag.DAL.Repositories;

namespace Ag.DAL;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        Clients = new ClientRepository(_context);
        Cards = new BaseRepository<Card>(_context);
    }

    public IClientRepository Clients { get; private set; }
    public IRepository<Card> Cards { get; private set; }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

