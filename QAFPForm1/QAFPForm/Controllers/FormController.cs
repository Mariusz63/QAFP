using Microsoft.AspNetCore.Mvc;
using QAFPForm.Models;
using QAFPForm.Services;
using System.Linq;
using System.Xml.Serialization;

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
            ViewBag.XmlSaved = true; // komunikat

            return RedirectToAction("Index", new { id = model.Id });
        }


        [HttpPost]
        public IActionResult DownloadXml(FormSubmission model)
        {
            // Serializacja modelu do XML
            var serializer = new XmlSerializer(typeof(FormSubmission));

            using var stringWriter = new StringWriter();
            serializer.Serialize(stringWriter, model);
            var xmlContent = stringWriter.ToString();
            var xmlBytes = System.Text.Encoding.UTF8.GetBytes(xmlContent);

            return File(xmlBytes, "application/xml", "zgloszenie_qafp.xml");
        }


        public IActionResult Weryfikacja()
        {
            // Zakładam że zapisujesz dane w XML
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "submission.xml");

            if (!System.IO.File.Exists(path))
                return NotFound("Brak danych do weryfikacji.");

            var xml = System.IO.File.ReadAllText(path);

            // Deserialize XML -> model
            XmlSerializer serializer = new XmlSerializer(typeof(FormSubmission));
            using var reader = new StringReader(xml);
            var model = (FormSubmission)serializer.Deserialize(reader);

            return View(model);
        }


        [HttpGet]
        public IActionResult List()
        {
            return View(_store);
        }
    }
}
