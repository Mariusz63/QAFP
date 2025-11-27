using Microsoft.AspNetCore.Mvc;
using QAFPForm.Models;
using System.Diagnostics;

namespace QAFPForm.Controllers
{
    public class FormController: Controller
    {
        // Zapis w pamięci (pozniej zamienic na dostęp do bazy danych)
        private static readonly List<FormSubmission> _store = new();

        [HttpGet]
        public IActionResult Index()
        {
            return View(new FormSubmission());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(FormSubmission model)
        {
            // ---- Walidacja: PESEL lub REGON ----
            if (string.IsNullOrWhiteSpace(model.Pesel) && string.IsNullOrWhiteSpace(model.Regon))
            {
                ModelState.AddModelError("Pesel", "Wymagane jest podanie PESEL lub REGON.");
                ModelState.AddModelError("Regon", "Wymagane jest podanie REGON lub PESEL.");
            }

            // ---- Walidacja: num. kontatowy lub e-mail ----
            if (string.IsNullOrWhiteSpace(model.Telefon) && string.IsNullOrWhiteSpace(model.Email))
            {
                ModelState.AddModelError("Telefon", "Numer telefonu jest wymagany.");
                ModelState.AddModelError("Email", "Adres e-mail jest wymagany.");
            }

            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            // zapis
            model.Id = _store.Count + 1;
            _store.Add(model);

            return RedirectToAction("Success", new { id = model.Id });
        }

        [HttpGet]
        public IActionResult Success(int id)
        {
            var model = _store.FirstOrDefault(x => x.Id == id);
            if (model == null) return NotFound();
            return View(model);
        }

        // Lista dla Admina
        [HttpGet]
        public IActionResult List()
        {
            return View(_store);
        }

        
    }
}
