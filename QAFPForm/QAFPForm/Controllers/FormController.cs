using Microsoft.AspNetCore.Mvc;
using QAFPForm.Models;
using QAFPForm.Services;
using System.Linq;

namespace QAFPForm.Controllers
{
    public class FormController : Controller
    {
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
            // ---- Walidacja PESEL/REGON ----
            if (string.IsNullOrWhiteSpace(model.Pesel) && string.IsNullOrWhiteSpace(model.Regon))
            {
                ModelState.AddModelError("Pesel", "Wymagane jest podanie PESEL lub REGON.");
                ModelState.AddModelError("Regon", "Wymagane jest podanie REGON lub PESEL.");
            }

            // ---- Walidacja telefon/e-mail ----
            if (string.IsNullOrWhiteSpace(model.Telefon) && string.IsNullOrWhiteSpace(model.Email))
            {
                ModelState.AddModelError("Telefon", "Numer telefonu jest wymagany.");
                ModelState.AddModelError("Email", "Adres e-mail jest wymagany.");
            }

            // ---- Walidacja adresów ----
            if (string.IsNullOrWhiteSpace(model.AdresSiedziba?.Ulica))
                ModelState.AddModelError("AdresSiedziba.Ulica", "Ulica (Siedziba) jest wymagana.");

            if (string.IsNullOrWhiteSpace(model.AdresZaklad?.Ulica))
                ModelState.AddModelError("AdresZaklad.Ulica", "Ulica (Zakład) jest wymagana.");

            if (string.IsNullOrWhiteSpace(model.AdresKorespondencja?.Ulica))
                ModelState.AddModelError("AdresKorespondencja.Ulica", "Ulica (Korespondencja) jest wymagana.");

            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            model.Id = _store.Count + 1;
            _store.Add(model);

            XmlService.Save(model);

            return RedirectToAction("Index", new { id = model.Id });
        }

        public IActionResult DownloadXml(int id)
        {
            var model = _store.FirstOrDefault(x => x.Id == id);
            if (model == null) return NotFound();

            byte[] xmlBytes = XmlService.ExportSingle(model);
            return File(xmlBytes, "application/xml", $"submission_{id}.xml");
        }

        [HttpGet]
        public IActionResult List()
        {
            return View(_store);
        }
    }
}
