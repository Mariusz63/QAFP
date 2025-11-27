using System.ComponentModel.DataAnnotations;

namespace QAFPForm.Models
{
    public class FormSubmission
    {
        public int Id { get; set; }

        // ==========================================
        // I. DANE PRZEDSIĘBIORSTWA
        // ==========================================
        [Required(ErrorMessage = "Pole Nazwisko / Nazwa jest wymagane.")]
        [Display(Name = "Nazwisko / Nazwa")]
        [StringLength(100, ErrorMessage = "Nazwisko/Nazwa może mieć maksymalnie {1} znaków.")]
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
        [StringLength(14)]
        public string? Pesel { get; set; }

        [Display(Name = "REGON")]
        [StringLength(14)]
        public string? Regon { get; set; }


        [Required(ErrorMessage = "Pole NIP jest wymagane.")]
        [Display(Name = "NIP")]
        [StringLength(10)]
        public string? Nip { get; set; }

        // ==========================================
        // II. ADRES
        // ==========================================
        [Required(ErrorMessage = "Pole Adres Siedziby jest wymagane.")]
        [Display(Name = "Siedziby")]
        public string? Siedziba { get; set; }

        [Required(ErrorMessage = "Pole Adres Zakładu jest wymagane.")]
        [Display(Name = "Zakładu lub gospodarstwa")]
        public string? Zaklad { get; set; }

        [Required(ErrorMessage = "Pole Adres do korespondencji jest wymagane.")]
        [Display(Name = "Adres do korespondencji")]
        public string? Korespondencja { get; set; }

        // ==========================================
        // III. RODZAJ DZIAŁALNOŚCI (Checkbox'y)
        // Walidacja checkboxów jest bardziej złożona, pomijamy Required, zakładając, że wystarczy 0
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
        // Zapewniamy, że wybrano opcję "Całość" lub "Część"
        [Required(ErrorMessage = "Musisz wybrać zakres produkcji (Całość lub Część).")]
        public string? ZakresProdukcji { get; set; }

        [Display(Name = "Część stanowiąca")]
        [Range(0, 100, ErrorMessage = "Wartość musi mieścić się w zakresie od 0 do 100.")]
        [RegularExpression(@"^\d{1,3}$", ErrorMessage = "Procent musi być liczbą całkowitą.")]
        public int? ProcentProdukcji { get; set; }

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

        [Display(Name = "Magazynowanie/utylizacja odpadów")]
        public bool Odpady { get; set; }

        [Display(Name = "Działki")]
        public bool Dzialki { get; set; }

        [Display(Name = "Całkowita pow.")]
        public string? CalkowitaPow { get; set; }

        [Display(Name = "Powierzchnia UR")]
        public string? PowierzchniaUR { get; set; }


        // ==========================================
        // VI. RODZAJ I WIELKOŚĆ PRODUKCJI
        // ==========================================
        [Required(ErrorMessage = "Opis rodzaju produkcji jest wymagany.")]
        [Display(Name = "Rodzaj produkcji")]
        public string? RodzajProdukcjiOpis { get; set; }

        [Required(ErrorMessage = "Określenie wielkości produkcji jest wymagane.")]
        [Display(Name = "Wielkość (rocznie)")]
        public string? WielkoscProdukcji { get; set; }

        // ==========================================
        // VIII. PODZLECANIE (Radio Tak/Nie)
        // ==========================================
        [Required(ErrorMessage = "Musisz określić, czy podzlecasz działania.")]
        public bool? Podzlecanie { get; set; }

        [Display(Name = "Rodzaj podzlecanych działań")]
        public string? RodzajPodzlecanych { get; set; }

        // ==========================================
        // IX. TRANSPORT
        // ==========================================
        [Required(ErrorMessage = "Musisz określić rodzaj transportu.")]
        public string? Transport { get; set; }

        // ==========================================
        // X. OPIS PROCESU
        // ==========================================
        [Required(ErrorMessage = "Opis procesu produkcji/gospodarstwa jest wymagany.")]
        [Display(Name = "Opis procesu produkcji / gospodarstwa")]
        public string? OpisProcesu { get; set; }

        // ==========================================
        // XI. JEDNOSTKA CERTYFIKUJĄCA
        // ==========================================
        [Required(ErrorMessage = "Musisz wybrać jednostkę certyfikującą.")]
        [Display(Name = "Wybór jednostki certyfikującej")]
        public string? JednostkaCertyfikujaca { get; set; }

        // ==========================================
        // ZAŁĄCZNIKI (Checkbox'y)
        // ==========================================
        // ... (brak dodatkowej walidacji boolowskiej) ...
        [Display(Name = "Dokumenty potwierdzające status")]
        public bool Zal_DokumentyStatus { get; set; }

        [Display(Name = "Plany sytuacyjne")]
        public bool Zal_PlanySytuacyjne { get; set; }

        [Display(Name = "Plany produkcyjne")]
        public bool Zal_PlanyProdukcyjne { get; set; }

        [Display(Name = "Skład produktu / Szacowana wielkość")]
        public bool Zal_SkladProduktu { get; set; }
    }
}