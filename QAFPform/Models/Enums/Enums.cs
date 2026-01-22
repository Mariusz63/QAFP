using System.ComponentModel.DataAnnotations;

namespace QAFPform.Models.Enums
{
   public enum Wojewodztwa
   {
       Dolnoslaskie,
       KujawskoPomorskie,
       Lubelskie,
       Lubuskie,
       Lodzkie,
       Malopolskie,
       Mazowieckie,
       Opolskie,
       Podkarpackie,
       Podlaskie,
       Pomorskie,
       Slaskie,
       Swietokrzyskie,
       WarminskoMazurskie,
       Wielkopolskie,
       Zachodniopomorskie
    }

    public enum TransportTyp
    {
        [Display(Name = "Własny")]
        Wlasny,

        [Display(Name = "Zewnętrzny")]
        Zewnetrzny,

        [Display(Name = "Mieszany")]
        Mieszany
    }


}
