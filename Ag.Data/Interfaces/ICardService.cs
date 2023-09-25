using Ag.BLL.Models;
using System.Linq.Expressions;

namespace Ag.BLL.Interfaces;

public interface ICardService
{
    bool Exists(int cardId, int clientId, string cardNumber);
    Task<IEnumerable<Card>> GetAllAsync();
    Task<Card?> GetByCodeAsync(int code);
    Task<Card> CreateAsync(Card newCard);
    Task<bool> UpdateAsync(int code, Card updatedCard);
    Task<bool> DeleteAsync(int code);
}
