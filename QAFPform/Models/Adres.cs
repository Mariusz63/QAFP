using QAFPform.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAFPform.Models
{
    public class Adres
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Musisz podać nazwę ulicy.")]
        [Display(Name = "Ulica")]
        public string? Ulica { get; set; }

        [Display(Name = "Nr ulicy")]
        public string? NrUlicy{ get; set; } = string.Empty;

        [Display(Name = "Nr domu")]
        public string? NrDomu { get; set; } = string.Empty;

        [Display(Name = "Nr lokalu")]
        public string? NrLokalu { get; set; } = string.Empty;

        [Display(Name = "Kod pocztowy")]
        [RegularExpression(@"\d{2}-\d{3}", ErrorMessage = "Kod pocztowy musi mieć format 00-000")]
        public string? KodPocztowy { get; set; }

        [Required(ErrorMessage = "Musisz podać nazwę miejscowości.")]
        [Display(Name = "Miejscowość")]
        public string? Miejscowosc { get; set; }

        [Display(Name = "Województwo")]
        public Wojewodztwa Wojewodztwo { get; set; }

        // Klucz obcy do formularza
        public int? FormularzId { get; set; }

        [ForeignKey("FormularzId")]
        public Formularz? Formularz { get; set; }
    }
}
