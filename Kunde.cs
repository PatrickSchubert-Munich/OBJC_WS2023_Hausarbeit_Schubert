using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Werkzeugverleih
{
    public class Kunde
    {

        // Properties
        [XmlElement("Id")]
        public int KundenID { get; set; }

        public string Vorname { get; set; }

        public string Nachname { get; set; }

        public string Geburtsdatum { get; set; }
        
        [XmlElement("Alter")]
        public int Alter
        {
            get  
            {
                // Speichere das heutige Datum.
                var today = DateTime.Today;
                var birthday = DateTime.Parse(Geburtsdatum);
                // Berechne das Alter und gib es zurück.
                int age = today.Year - birthday.Year;
                return age;
            }
            set { }

        }

        public string Geschlecht { get; set; }

        public string Adresse { get; set; }

        public string Bankverbindung { get; set; }

        [XmlAttribute("typ")]
        public string Kundentyp { get; set; }

        public string Telefonnummer { get; set; }

        [XmlAttribute("rating")]
        public int Bewertung { get; set; }


        // parameterless constructor
        public Kunde() { }

        // constructor with parameters from customer
        public Kunde(int kundenId, string vorname, string nachname,
                     string geschlecht, string geburtsdatum, string adresse, string bankverbindung,
                     string kundentyp, string telefonnummer, int bewertung)
        {
            this.KundenID = kundenId;
            this.Vorname = vorname;
            this.Nachname = nachname;
            this.Geschlecht = geschlecht;
            this.Geburtsdatum = ConvertBirthday(geburtsdatum);
            this.Adresse = adresse;
            this.Bankverbindung = bankverbindung;
            this.Kundentyp = kundentyp;
            this.Telefonnummer = telefonnummer;
            this.Bewertung = bewertung;
        }


        /// <summary>
        /// Check format of customers birthday
        /// </summary>
        /// <param name="geburtsdatum">birthday of customer</param>
        /// <returns></returns>
        private string ConvertBirthday(string geburtsdatum)
        {
            var defaultBirthday = DateTime.Now.Date;
            
            try
            {
                
                if (DateTime.TryParse(geburtsdatum, out defaultBirthday))
                {
                    var formatBirthday = Convert.ToDateTime(geburtsdatum);
                    return $"{formatBirthday.Day}.{formatBirthday.Month}.{formatBirthday.Year}";
                };
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Birthday not in correct Format: DD.MM.YYYY");
                throw;
            }

            return $"{defaultBirthday.Day}.{defaultBirthday.Month}.{defaultBirthday.Year}";
        }

    }
}
