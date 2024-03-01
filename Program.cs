using System.Xml.Serialization;
using Werkzeugverleih;
using static System.Collections.Specialized.BitVector32;

namespace OBJC_WS2023_Hausarbeit_Schubert
{
    public class Program
    {
        public static List<Kunde> KundenListe = new List<Kunde>();

        public static void Main(string[] args)
        {
            // Erstelle einen Kunden
            var kunde1 = new Kunde(
                kundenId: 1,
                vorname: "Thomas",
                nachname: "Mustermann",
                geschlecht: "maennlich",
                geburtsdatum: "10.08.2003",
                adresse: "80678 München",
                bankverbindung: "DE12 3456 1000 0025 2937 90",
                kundentyp: "privat",
                telefonnummer: "0177689654",
                bewertung: 2);

            // Erstelle einen weiteren Kunden
            Kunde kunde2 = new Kunde(
                kundenId: 2,
                vorname: "Anna",
                nachname: "Musterfrau",
                geschlecht: "weiblich",
                geburtsdatum: "20.03.1984",
                adresse: "12345 Berlin",
                bankverbindung: "DE34 5678 1000 0025 9876 54",
                kundentyp: "geschaeftlich",
                telefonnummer: "01551234567",
                bewertung: 3
            );

            // Füge den Kunden zur Kundenliste hinzu
            KundenListe.Add(kunde1);
            KundenListe.Add(kunde2);

            // Erzeuge eine Instanz der Kundenverwaltung
            Kundenverwaltung kundenverwaltung = new Kundenverwaltung(KundenListe);
            kundenverwaltung.KundeAnlegen(KundenListe);

            // Zeige Kundendaten an
            kundenverwaltung.KundeDatenAnzeigen();

            // Loesche einen Kunden
            int selection = 1;
            kundenverwaltung.KundeLoeschen(selection);

            // Zeige Kundendaten an
            kundenverwaltung.KundeDatenAnzeigen();


        }

    }
}