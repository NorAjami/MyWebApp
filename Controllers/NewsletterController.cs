using Microsoft.AspNetCore.Mvc;
using MyWebApp.Models;
using MyWebApp.Services;
using System.Threading.Tasks;
using MyWebApp;

namespace MyWebApp.Controllers
{
    public class NewsletterController : Controller
    {
        // Vi hämtar in vår service via DI
        
         //private readonly IValidator<Subscriber> _validator;
        private readonly INewsletterService _newsletterService;

  /*public NewsletterController(IValidator<Subscriber> validator, INewsletterService newsletterService)
    {
        _validator = validator;
        _newsletterService = newsletterService;
    }*/
        // Konstruktor där vi kopplar DI till vår service
        public NewsletterController(INewsletterService newsletterService)
        {
            _newsletterService = newsletterService;
        }

        // GET: Visa formulär
        [HttpGet]
        public IActionResult Subscribe()
        {
            return View();
        }

        // POST: Skicka formulär
        [HttpPost]
        public async Task<IActionResult> Subscribe(Subscriber subscriber)
        {
            if (!ModelState.IsValid)
            {
                return View(subscriber);
            }

            var result = await _newsletterService.SignUpForNewsletterAsync(subscriber);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("Email", result.Message);
                return View(subscriber);
            }

            TempData["SuccessMessage"] = result.Message;

            return RedirectToAction(nameof(Subscribe));
        }

        // GET: Visa alla prenumeranter
        [HttpGet]
        public async Task<IActionResult> Subscribers()
        {
            var subscribers = await _newsletterService.GetActiveSubscribersAsync();
            return View(subscribers);
        }

        // POST: Avprenumerera
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Unsubscribe(string email)
        {
            var result = await _newsletterService.OptOutFromNewsletterAsync(email);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message;
            }

            return RedirectToAction(nameof(Subscribers));
        }
    }
}
