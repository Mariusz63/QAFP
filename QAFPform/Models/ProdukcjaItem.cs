using System.ComponentModel.DataAnnotations;

namespace QAFPform.Models
{
    public class ProdukcjaItem
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Opis rodzaju produkcji jest wymagany.")]
        [Display(Name = "Rodzaj produkcji")]
        public string? RodzajProdukcjiOpis { get; set; }

        [Required(ErrorMessage = "Określenie wielkości produkcji jest wymagane.")]
        [Display(Name = "Wielkość (rocznie)")]
        public string WielkoscProdukcji { get; set; } = string.Empty;

        // Klucz obcy do Formularza
        public int FormularzId { get; set; }
        public Formularz? Formularz { get; set; }
    }
}
