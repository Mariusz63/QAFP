using QAFPform.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace QAFPform.Models
{
    public class Formularz
    {
        [Key]
        public int Id { get; set; }

        // ==========================================
        // I. DANE PRZEDSIĘBIORSTWA
        // ==========================================
        [Display(Name = "Nazwa")]
        [StringLength(100, ErrorMessage = "Nazwisko może mieć maksymalnie {1} znaków.")]
        public string? Nazwa { get; set; }

        [Display(Name = "Nazwisko")]
        [StringLength(100, ErrorMessage = "Nazwisko może mieć maksymalnie {1} znaków.")]
        public string? Nazwisko { get; set; }

        [Display(Name = "Imię")]
        [StringLength(100, ErrorMessage = "Imię może mieć maksymalnie {1} znaków.")]
        public string? Imie { get; set; }

        [Required(ErrorMessage = "Numer telefonu jest wymagany.")]
        [Phone(ErrorMessage = "Wprowadź prawidłowy numer telefonu.")]
        [Display(Name = "Numer telefonu")]
        public string? Telefon { get; set; }

        [Required(ErrorMessage = "Adres e-mail jest wymagany.")]
        [EmailAddress(ErrorMessage = "Wprowadź prawidłowy adres e-mail.")]
        [Display(Name = "Adres e-mail")]
        public string? Email { get; set; }

        [Display(Name = "PESEL")]
        [StringLength(11)]
        public string? Pesel { get; set; }

        [Display(Name = "REGON")]
        [StringLength(14)]
        public string? Regon { get; set; }

        [Required(ErrorMessage = "Pole NIP jest wymagane.")]
        [Display(Name = "NIP")]
        [StringLength(10)]
        public string? Nip { get; set; }

        // ==========================================
        // II. ADRESY
        // ==========================================
        public Adres? AdresKorespondencyjny { get; set; }
        public Adres? AdresSiedziby { get; set; }
        public Adres? ZakladuLubGospodarstwa { get; set; }

        // ==========================================
        // III.	RODZAJ DZIAŁALNOŚCI
        // ==========================================

        [Display(Name = "Produkcja zwierzęca")]
        public bool ProdukcjaZwierzeca { get; set; }

        [Display(Name = "Ubój")]
        public bool Uboj { get; set; }

        [Display(Name = "Rozbiór")]
        public bool Rozbior { get; set; }

        [Display(Name = "Przetwórstwo")]
        public bool Przetworstwo { get; set; }

        [Display(Name = "Produkcja konserw")]
        public bool ProdukcjaKonserw { get; set; }

        [Display(Name = "Dystrybucja")]
        public bool Dystrybucja { get; set; }

        // ==========================================
        // IV. ZAKRES PRODUKCJI
        // ==========================================

        [Display(Name = "Całość")]
        [Required(ErrorMessage = "Musisz wybrać zakres produkcji (Całość lub Część).")]
        public bool CzyCalosc { get; set; }

        [Display(Name = "Część stanowiąca")]
        [Range(0, 100, ErrorMessage = "Wartość musi mieścić się w zakresie od 0 do 100.")]
        public string? ProcentProdukcji { get; set; }

        // ==========================================
        // V. OBIEKT I DZIAŁKI ROLNE
        // ==========================================

        [Display(Name = "Obiekty")]
        public bool Obiekty { get; set; }

        [Display(Name = "Produkcyjne")]
        public bool Produkcyjne { get; set; }

        [Display(Name = "Magazynowe")]
        public bool Magazynowe { get; set; }

        [Display(Name = "Oczyszczalnie ścieków")]
        public bool Oczyszczalnie { get; set; }

        [Display(Name = "Magazynowanie/ utylizacja odpadów")]
        public bool Odpady { get; set; }

        [Display(Name = "Działki")]
        public bool Dzialki { get; set; }

        [Display(Name = "Całkowita pow.")]
        public double CalkowitaPow { get; set; } = 0;

        [Display(Name = "Powierzchnia UR")]
        public double PowierzchniaUR { get; set; } = 0;

        // ==========================================
        // VI. RODZAJ I WIELKOŚĆ PRODUKCJI
        // ==========================================
        public List<ProdukcjaItem> Produkcje { get; set; } = new List<ProdukcjaItem>();

        // ==========================================
        // VII. PODZLECANIE (Radio Tak/Nie)
        // ==========================================

        [Required(ErrorMessage = "Musisz określić, czy podzlecasz działania.")]
        [Display(Name = "Czy podzlecasz działania?")]
        public bool CzyPodzlecanie { get; set; } // Tak = true, Nie = false

        [Display(Name = "Rodzaj podzlecanych działań")]
        public string? RodzajPodzlecanych { get; set; }


        // ==========================================
        // VIII. TRANSPORT
        // ==========================================

        [Required(ErrorMessage = "Wybierz rodzaj transportu")]
        [Display(Name = "Rodzaj transportu")]
        public TransportTyp? Transport { get; set; } = TransportTyp.Wlasny;

        // ==========================================
        // IX. OPIS PROCESU
        // ==========================================
        [Required(ErrorMessage = "Opis procesu produkcji/gospodarstwa jest wymagany.")]
        [Display(Name = "Opis procesu produkcji / gospodarstwa")]
        public string? OpisProcesu { get; set; }

        // ==========================================
        // X. JEDNOSTKA CERTYFIKUJĄCA
        // ==========================================
        [Required(ErrorMessage = "Musisz wybrać jednostkę certyfikującą.")]
        [Display(Name = "Wybór jednostki certyfikującej")]
        public string? JednostkaCertyfikujaca { get; set; }

        // ==========================================
        // ZAŁĄCZNIKI (Checkbox'y)
        // ==========================================
        [Display(Name = "Dokumenty potwierdzające status")]
        public bool Zal_DokumentyStatus { get; set; }

        [Display(Name = "Plany sytuacyjne")]
        public bool Zal_PlanySytuacyjne { get; set; }

        [Display(Name = "Plany produkcyjne")]
        public bool Zal_PlanyProdukcyjne { get; set; }

        [Display(Name = "Skład produktu / Szacowana wielkość")]
        public bool Zal_SkladProduktu { get; set; }

        // ==========================================
        // DATA I MIEJSCE ZŁOŻENIA FORMULARZA
        // ==========================================
        [Display(Name = "Data złożenia formuarza")]
        public DateTime DataZlozenia { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Musisz podać miejscowość.")]
        [Display(Name = "Miejsce złożenia formularza")]
        public string MiejsceZlozenia { get; set; } = string.Empty;
    }
}
