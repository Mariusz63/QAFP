using Microsoft.AspNetCore.Mvc;
using QAFPform.Data;
using QAFPform.Models;
using QAFPform.Models.ViewModels;

namespace QAFPform.Controllers
{
    public class FormularzController : Controller
    {
        // ---- DbContext Injection ---- po podłączeniu bazy danych
        private readonly QAFPformDbContext _context;

        // ---- In-Memory Store for Formularz ----
        private static readonly List<Formularz> _store = new();

        // --- Konstruktor z wstrzykiwaniem DbContext ----
        public FormularzController(QAFPformDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new Formularz());
        }

        [HttpPost]
        public IActionResult Index(Formularz model)
        {
            if (ModelState.IsValid)
            {
                // ---- Walidacja PESEL/REGON ----
                if (string.IsNullOrWhiteSpace(model.Pesel) || string.IsNullOrWhiteSpace(model.Regon))
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

                // ---- Walidacja procentu produkcji ----
                if (model.CzyCalosc == false && string.IsNullOrWhiteSpace(model.ProcentProdukcji))
                {
                    ModelState.AddModelError("ProcentProdukcji", "Proszę podać procent produkcji.");
                }

                if(model.CzyCalosc == false && !string.IsNullOrWhiteSpace(model.ProcentProdukcji))
                {
                    if(!int.TryParse(model.ProcentProdukcji, out int procent) || procent < 1 || procent > 100)
                    {
                        ModelState.AddModelError("ProcentProdukcji", "Procent produkcji musi być liczbą od 1 do 100.");
                    }
                }

                // ---- Walidacja podzlecania ----
                if (model.CzyPodzlecanie == true && string.IsNullOrWhiteSpace(model.RodzajPodzlecanych))
                {
                    ModelState.AddModelError("RodzajPodzlecanych", "Proszę podać rodzaj podzlecanych procesów.");
                }

                if(model.CzyPodzlecanie == false && string.IsNullOrWhiteSpace(model.RodzajPodzlecanych))
                {
                    ModelState.AddModelError("RodzajPodzlecanych", "Proszę podać rodzaj podzlecanych działań lub zaznaczyć brak podzlecania.");
                }

                if(model.CzyPodzlecanie == true && !string.IsNullOrWhiteSpace(model.RodzajPodzlecanych))
                {
                        ModelState.AddModelError("RodzajPodzlecanych", "Błąd podczas wypełniania podzleceń.");
                }

                if (model.Id == 0)
                {
                    model.Id = _store.Count + 1;
                }

                // Save the model to the in-memory store
                _store.Add(model);
                return RedirectToAction("Success");
            }
            return View("Index");
        }
    }
}
