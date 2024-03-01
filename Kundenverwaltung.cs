using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Werkzeugverleih
{
    public class Kundenverwaltung
    {
        // Filepath and Filename to save Xml-File
        private string FilePath = @"C:\Users\Patri\source\repos\OBJC_WS2023_Hausarbeit_Schubert";
        private string FileName = @"Kundendatei.xml";

        // List of Objects Kunde
        private List<Kunde> Kunden { get; set; }

        // Boolean Flag, checks amount of Customers (see Fcn below) 
        private bool Flag { get; set; }

        // Constructor
        public Kundenverwaltung(List<Kunde> kunden)
        {
            this.Kunden = kunden;
            this.Flag = false;
        }

        /// <summary>
        /// Serialization of Customers to XML-File.
        /// </summary>
        /// <param name="Kunden">List of Objects Kunde</param>
        public void KundeAnlegen(List<Kunde> Kunden)
        {
            // Daten serialisieren
            if (AmountOfCustomers())
            {   // Serialisieren aller Kunden durch Übergabe einer Liste
                XmlHandler.SerializeXmlListOfObjects(CreateCompleteFilePath(FilePath, FileName), this.Kunden);
            }
            else
            {   // Serialisieren eines Kunden durch Übergabe eines einzelnen Objekts
                XmlHandler.SerializeXmlObject(CreateCompleteFilePath(FilePath, FileName), this.Kunden[0]);
            }
        }

        void KundeBearbeiten(int selection)
        {
            // Get Content (objects) from XML
            XDocument kundenXdoc = new XDocument();
            kundenXdoc = XDocument.Parse(CreateCompleteFilePath(FilePath, FileName));

            var kunden = from kunde in kundenXdoc.Descendants("Kunde")
                    select new { kundenXdoc.Elements() };
        }

        public void KundeLoeschen(int selection)
        {
            // Get Content (objects) from XML
            var kundenObjects = XmlHandler.DeserializeXmlObjects<List<Kunde>>(CreateCompleteFilePath(FilePath, FileName));

            try
            {
                if (kundenObjects != null)
                {
                    foreach (var k in kundenObjects)
                    {
                        if (k.KundenID == selection)
                        {
                            Console.WriteLine("Kunde kann bearbeitet werden.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Error: The deserialization result is null.");
                }

                // Write back Content (objects) to XML
                KundeAnlegen(Kunden);

            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"Error: {ex.Message}. NullReferenceException during deserialization. Object: {ex.Source}");
                throw;
            }
        }


        public void KundeDatenAnzeigen()
        {
            var kundenObjects = XmlHandler.DeserializeXmlObjects<List<Kunde>>(CreateCompleteFilePath(FilePath, FileName));

            try
            {
                if (kundenObjects != null)
                {
                    foreach (var k in kundenObjects)
                    {
                        Console.WriteLine("**************** KUNDENDATEN **************");
                        Console.WriteLine($"Vorname   : {k.Vorname},\n" +
                                          $"Nachname  : {k.Nachname},\n" +
                                          $"Geschlecht: {k.Geschlecht},\n" +
                                          $"Geb.-Datum: {k.Geburtsdatum},\n" +
                                          $"Adresse   : {k.Adresse},\n" +
                                          $"Kundentyp : {k.Kundentyp},\n" +
                                          $"Tel.-Nr.  : {k.Telefonnummer},\n" +
                                          $"Bankverb. : {k.Bankverbindung},\n" +
                                          "*******************************************\n");
                    }
                }
                else
                {
                    Console.WriteLine("Error: The deserialization result is null.");
                }
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"Error: {ex.Message}. NullReferenceException during deserialization. Object: {ex.Source}");
                throw;
            }

        }

        /// <summary>
        /// For completion of Filepath and Filename 
        /// </summary>
        /// <param name="FilePath">Filepath where file is located</param>
        /// <param name="FileName">Filename is the name of the Xml-File</param>
        /// <returns></returns>
        private string CreateCompleteFilePath(string FilePath, string FileName)
        {
            string FileToSave = Path.Combine(FilePath, FileName);
            return FileToSave;
        }

        /// <summary>
        /// Helper Function for checking amount of Customers.
        /// If more than one Customer, Flag is set to true. 
        /// </summary>
        /// <returns>Boolean Flag</returns>
        private bool AmountOfCustomers()
        {
            if (this.Kunden.Count > 1)
            {
                this.Flag = true;
            }

            return this.Flag;
        }

    }

}
