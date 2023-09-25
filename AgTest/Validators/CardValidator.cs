using Ag.BLL.Interfaces;
using Ag.BLL.Models;
using Ag.BLL.Services;

namespace Ag.Web.Validators
{
    public class CardValidator : IValidator<Card>
    {
        private readonly ICardService _cardService;

        public CardValidator(ICardService cardService)
        {
            _cardService = cardService;
        }

        public (bool IsValid, string ErrorMessage) Validate(Card value)
        {
            var exists = _cardService.Exists(value.CardId, value.ClientId, value.CardNumber);
            var errorMsg = exists ? "Card number must be unique" : string.Empty;
            return (!exists, errorMsg);
        }
    }
}
