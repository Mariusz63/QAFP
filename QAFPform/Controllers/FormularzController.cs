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

        // ---- In-Memory Store for FormularzViewModel ----
        private static readonly List<FormularzViewModel> _store = new();

        // --- Konstruktor z wstrzykiwaniem DbContext ----
        public FormularzController(QAFPformDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new FormularzViewModel());
        }

        [HttpPost]
        public IActionResult Index(FormularzViewModel model)
        {
            if (ModelState.IsValid)
            {
                // ---- Walidacja PESEL/REGON ----
                if (string.IsNullOrWhiteSpace(model.Formularz.Pesel) || string.IsNullOrWhiteSpace(model.Formularz.Regon))
                {
                    ModelState.AddModelError("Pesel", "Wymagane jest podanie PESEL lub REGON.");
                    ModelState.AddModelError("Regon", "Wymagane jest podanie REGON lub PESEL.");
                }

                // ---- Walidacja telefon/e-mail ----
                if (string.IsNullOrWhiteSpace(model.Formularz.Telefon) && string.IsNullOrWhiteSpace(model.Formularz.Email))
                {
                    ModelState.AddModelError("Telefon", "Numer telefonu jest wymagany.");
                    ModelState.AddModelError("Email", "Adres e-mail jest wymagany.");
                }

                // ---- Walidacja procentu produkcji ----
                if (model.Formularz.CzyCalosc == false && string.IsNullOrWhiteSpace(model.Formularz.ProcentProdukcji))
                {
                    ModelState.AddModelError("ProcentProdukcji", "Proszę podać procent produkcji.");
                }

                if(model.Formularz.CzyCalosc == false && !string.IsNullOrWhiteSpace(model.Formularz.ProcentProdukcji))
                {
                    if(!int.TryParse(model.Formularz.ProcentProdukcji, out int procent) || procent < 1 || procent > 100)
                    {
                        ModelState.AddModelError("ProcentProdukcji", "Procent produkcji musi być liczbą od 1 do 100.");
                    }
                }

                // ---- Walidacja podzlecania ----
                if (model.Formularz.CzyPodzlecanie == true && string.IsNullOrWhiteSpace(model.Formularz.RodzajPodzlecanych))
                {
                    ModelState.AddModelError("RodzajPodzlecanych", "Proszę podać rodzaj podzlecanych procesów.");
                }

                if(model.Formularz.CzyPodzlecanie == false && string.IsNullOrWhiteSpace(model.Formularz.RodzajPodzlecanych))
                {
                    ModelState.AddModelError("RodzajPodzlecanych", "Proszę podać rodzaj podzlecanych działań lub zaznaczyć brak podzlecania.");
                }

                if(model.Formularz.CzyPodzlecanie == true && !string.IsNullOrWhiteSpace(model.Formularz.RodzajPodzlecanych))
                {
                        ModelState.AddModelError("RodzajPodzlecanych", "Błąd podczas wypełniania podzleceń.");
                }

                if (model.Formularz.Id == 0)
                {
                    model.Formularz.Id = _store.Count + 1;
                }

                // Save the model to the in-memory store
                _store.Add(model);
                return RedirectToAction("Success");
            }
            return View("Index");
        }
    }
}
