using System.IO;
using System.Xml.Linq;
using QAFPForm.Models;

namespace QAFPForm.Services
{
    public static class XmlService
    {
        private static string DataFolder => Path.Combine(Directory.GetCurrentDirectory(), "Data");
        private static string FilePath => Path.Combine(DataFolder, "submissions.xml");

        public static XElement BuildSubmission(FormSubmission model)
        {
            XElement BuildAddress(Address addr)
            {
                return new XElement("Adres",
                    new XElement("Ulica", addr.Ulica),
                    new XElement("NrDomu", addr.NrDomu),
                    new XElement("NrLokalu", addr.NrLokalu),
                    new XElement("KodPocztowy", addr.KodPocztowy),
                    new XElement("Miejscowosc", addr.Miejscowosc),
                    new XElement("Gmina", addr.Gmina),
                    new XElement("Powiat", addr.Powiat),
                    new XElement("Wojewodztwo", addr.Wojewodztwo),
                    new XElement("Kraj", addr.Kraj)
                );
            }

            XElement BuildProdukcje(IEnumerable<ProdukcjaItem> produkcje)
            {
                return new XElement("RodzajeProdukcji",
                    produkcje.Select(p =>
                        new XElement("Produkcja",
                            new XElement("Rodzaj", p.RodzajProdukcjiOpis),
                            new XElement("Wielkosc", p.WielkoscProdukcji)
                        )
                    )
                );
            }


            return new XElement("Submission",
                new XElement("Id", model.Id),
                new XElement("Nazwisko", model.Nazwisko),
                new XElement("Imie", model.Imie),
                new XElement("Telefon", model.Telefon),
                new XElement("Email", model.Email),
                new XElement("Pesel", model.Pesel),
                new XElement("Regon", model.Regon),
                new XElement("Nip", model.Nip),

                new XElement("AdresSiedziba", BuildAddress(model.AdresSiedziba)),
                new XElement("AdresZaklad", BuildAddress(model.AdresZaklad)),
                new XElement("AdresKorespondencja", BuildAddress(model.AdresKorespondencja)),

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

                BuildProdukcje(model.Produkcje),

                new XElement("Podzlecanie", model.Podzlecanie),
                new XElement("RodzajPodzlecanych", model.RodzajPodzlecanych),
                new XElement("Transport", model.Transport),

                new XElement("OpisProcesu", model.OpisProcesu),
                new XElement("JednostkaCertyfikujaca", model.JednostkaCertyfikujaca),

                new XElement("Zal_DokumentyStatus", model.Zal_DokumentyStatus),
                new XElement("Zal_PlanySytuacyjne", model.Zal_PlanySytuacyjne),
                new XElement("Zal_PlanyProdukcyjne", model.Zal_PlanyProdukcyjne),
                new XElement("Zal_SkladProduktu", model.Zal_SkladProduktu),
                
                new XElement("DataZlozenia", model.DataZlozenia),
                new XElement("MiejsceZlozenia", model.MiejsceZlozenia)
            );
        }

        public static void Save(FormSubmission model)
        {
            if (!Directory.Exists(DataFolder))
                Directory.CreateDirectory(DataFolder);

            XDocument doc;

            if (File.Exists(FilePath))
                doc = XDocument.Load(FilePath);
            else
                doc = new XDocument(new XElement("Submissions"));

            doc.Root!.Add(BuildSubmission(model));
            doc.Save(FilePath);
        }

        public static byte[] ExportSingle(FormSubmission model)
        {
            XDocument doc = new XDocument(new XElement("Submissions", BuildSubmission(model)));
            return System.Text.Encoding.UTF8.GetBytes(doc.ToString());
        }
    }
}
