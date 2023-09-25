using Ag.BLL.Interfaces;
using Ag.BLL.Models;

namespace Ag.BLL.Services
{
    public class CardService : ICardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public bool Exists(int cardId, int clientId, string cardNumber)
        {
            return _unitOfWork.Cards.Any(c => c.CardId != cardId
                && c.ClientId == clientId && c.CardNumber == cardNumber);
        }

        public async Task<IEnumerable<Card>> GetAllAsync()
        {
            return await _unitOfWork.Cards.GetAllAsync();
        }

        public async Task<Card?> GetByCodeAsync(int code)
        {
            return await _unitOfWork.Cards.GetByCodeAsync(code);
        }

        public async Task<Card> CreateAsync(Card newCard)
        {
            return await _unitOfWork.Cards.AddAsync(newCard);
        }

        public async Task<bool> UpdateAsync(int code, Card updatedCard)
        {
            var existingCard = await _unitOfWork.Cards.GetByCodeAsync(code);
            if (existingCard == null)
                return false;

            existingCard.CardType = updatedCard.CardType;
            existingCard.Bank = updatedCard.Bank;
            existingCard.CardNumber = updatedCard.CardNumber;
            existingCard.ExpiryMonth = updatedCard.ExpiryMonth;
            existingCard.ExpiryYear = updatedCard.ExpiryYear;

            return await _unitOfWork.Cards.UpdateAsync(existingCard);
        }

        public async Task<bool> DeleteAsync(int code)
        {
            var existingCard = await _unitOfWork.Cards.GetByCodeAsync(code);
            if (existingCard == null)
                return false;

            return await _unitOfWork.Cards.DeleteAsync(code);
        }
    }
}
