using Ag.BLL.Interfaces;
using Ag.BLL.Models;
using Ag.Web.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Ag.Web.Controllers
{
    public class CardController : Controller
    {
        private readonly ICardService _cardService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Card> _validator;

        public CardController(ICardService cardService, IUnitOfWork unitOfWork, IValidator<Card> validator)
        {
            _cardService = cardService;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        [Route("[controller]/[action]/{clientId}")]
        public IActionResult Create([FromRoute] int clientId)
        {
            return View(new Card { ClientId = clientId });
        }

        [HttpPost("[controller]/[action]/{clientId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CardId,ClientId,CardType,Bank,CardNumber,ExpiryMonth,ExpiryYear")] Card card)
        {
            var (isValid, errorMessage) = _validator.Validate(card);
            if (!isValid)
            {
                ModelState.TryAddModelError("CardNumber", errorMessage);
                return View(card);
            }
            if (ModelState.IsValid)
            {
                await _cardService.CreateAsync(card);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(
                    nameof(ClientController.Edit),
                    nameof(ClientController).Replace("Controller", ""),
                    new { code = card.ClientId }
                );
            }
            return View(card);
        }

        public async Task<IActionResult> Edit(int? code)
        {
            if (code == null)
            {
                return NotFound();
            }

            var card = await _cardService.GetByCodeAsync(code.Value);
            if (card == null)
            {
                return NotFound();
            }
            return View(card);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int code, [Bind("CardId,ClientId,CardType,Bank,CardNumber,ExpiryMonth,ExpiryYear")] Card card)
        {
            if (code != card.CardId)
            {
                return NotFound();
            }

            var (isValid, errorMessage) = _validator.Validate(card);
            if (!isValid)
            {
                ModelState.TryAddModelError("CardNumber", errorMessage);
                return View(card);
            }

            if (ModelState.IsValid)
            {
                await _cardService.UpdateAsync(code, card);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(
                    nameof(ClientController.Edit),
                    nameof(ClientController).Replace("Controller", ""),
                    new { code = card.ClientId }
                );
            }
            return View(card);
        }

        public async Task<IActionResult> Delete(int? code)
        {
            if (code == null)
            {
                return NotFound();
            }

            var card = await _cardService.GetByCodeAsync(code.Value);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int code)
        {
            var card = await _cardService.GetByCodeAsync(code);
            if (card == null)
            {
                return NotFound();
            }

            await _cardService.DeleteAsync(code);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(
                nameof(ClientController.Edit),
                nameof(ClientController).Replace("Controller", ""),
                new { code = card.ClientId }
            );
        }
    }
}
