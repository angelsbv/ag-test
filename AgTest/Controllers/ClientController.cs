using Ag.BLL.Interfaces;
using Ag.BLL.Models;
using Ag.Web.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Ag.Web.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IUnitOfWork _unitOfWork;

        public ClientController(IUnitOfWork unitOfWork, IClientService clientService)
        {
            _unitOfWork = unitOfWork;
            _clientService = clientService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _clientService.GetAllAsync());
        }

        public async Task<IActionResult> Details(int? code)
        {
            if (code == null)
            {
                return NotFound();
            }

            var client = await _clientService.GetByCodeAsync(code.Value);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Client client)
        {
            if (ModelState.IsValid)
            {
                await _clientService.CreateAsync(client);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        public async Task<IActionResult> Edit(int? code)
        {
            if (code == null)
            {
                return NotFound();
            }

            var client = await _clientService.GetByCodeAsync(code.Value, ClientSideLoad.Card);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int code, Client client)
        {
            if (code != client.Code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _clientService.UpdateAsync(code, client);
                await _unitOfWork.SaveAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        public async Task<IActionResult> Delete(int? code)
        {
            if (code == null)
            {
                return NotFound();
            }

            var client = await _clientService.GetByCodeAsync(code.Value);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int code)
        {
            var client = await _clientService.GetByCodeAsync(code);
            if (client != null)
            {
                await _clientService.DeleteAsync(client.Code);
                await _unitOfWork.SaveAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
