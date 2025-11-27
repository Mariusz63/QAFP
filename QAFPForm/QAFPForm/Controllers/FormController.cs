using Microsoft.AspNetCore.Mvc;
using QAFPForm.Models;
using System.Xml.Linq; 
using System.IO;

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

            // zapis do XML
            SaveToXml(model);

            return RedirectToAction("Success", new { id = model.Id });
        }

        [HttpGet]
        public IActionResult Success(int id)
        {
            var model = _store.FirstOrDefault(x => x.Id == id);
            if (model == null) return NotFound();
            return View(model);
        }

        private void SaveToXml(FormSubmission model)
        {
            // katalog "Data" w katalogu głównym projektu (bez wwwroot)
            string dataFolder = Path.Combine(Directory.GetCurrentDirectory(), "Data");
            // Tworzymy katalog jeśli nie istnieje
            if (!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
            }
            string filePath = Path.Combine(dataFolder, "submissions.xml");

            XDocument doc;

            if (System.IO.File.Exists(filePath))
            {
                doc = XDocument.Load(filePath);
            }
            else
            {
                doc = new XDocument(new XElement("Submissions"));
            }

            XElement submissionElement = new XElement("Submission",
                new XElement("Id", model.Id),
                new XElement("Nazwisko", model.Nazwisko),
                new XElement("Imie", model.Imie),
                new XElement("Telefon", model.Telefon),
                new XElement("Email", model.Email),
                new XElement("Pesel", model.Pesel),
                new XElement("Regon", model.Regon),
                new XElement("Nip", model.Nip),
                new XElement("Siedziba", model.Siedziba),
                new XElement("Zaklad", model.Zaklad),
                new XElement("Korespondencja", model.Korespondencja),
                new XElement("ProdukcjaZwierzeca", model.ProdukcjaZwierzeca),
                new XElement("Uboj", model.Uboj),
                new XElement("Rozbior", model.Rozbior),
                new XElement("Przetworstwo", model.Przetworstwo),
                new XElement("ProdukcjaKonserw", model.ProdukcjaKonserw),
                new XElement("Dystrybucja", model.Dystrybucja),
                new XElement("ZakresProdukcji", model.ZakresProdukcji),
                new XElement("ProcentProdukcji", model.ProcentProdukcji),
                new XElement("Obiekty", model.Obiekty),
                new XElement("Produkcyjne", model.Produkcyjne),
                new XElement("Magazynowe", model.Magazynowe),
                new XElement("Oczyszczalnie", model.Oczyszczalnie),
                new XElement("Odpady", model.Odpady),
                new XElement("Dzialki", model.Dzialki),
                new XElement("CalkowitaPow", model.CalkowitaPow),
                new XElement("PowierzchniaUR", model.PowierzchniaUR),
                new XElement("Podzlecanie", model.Podzlecanie),
                new XElement("RodzajPodzlecanych", model.RodzajPodzlecanych),
                new XElement("Transport", model.Transport),
                new XElement("OpisProcesu", model.OpisProcesu),
                new XElement("JednostkaCertyfikujaca", model.JednostkaCertyfikujaca),
                new XElement("Zal_DokumentyStatus", model.Zal_DokumentyStatus),
                new XElement("Zal_PlanySytuacyjne", model.Zal_PlanySytuacyjne),
                new XElement("Zal_PlanyProdukcyjne", model.Zal_PlanyProdukcyjne),
                new XElement("Zal_SkladProduktu", model.Zal_SkladProduktu)
            );

            doc.Root!.Add(submissionElement);
            doc.Save(filePath);
        }


        // Lista dla Admina
        [HttpGet]
        public IActionResult List()
        {
            return View(_store);
        }
    }
}
