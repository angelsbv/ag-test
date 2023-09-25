using Ag.BLL.Models;

namespace Ag.DAL.Repositories;

public class CardRepository : BaseRepository<Card>
{
    public CardRepository(AppDbContext context) : base(context)  { }
}