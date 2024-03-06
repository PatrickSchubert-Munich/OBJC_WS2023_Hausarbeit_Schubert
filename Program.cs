using System.Xml.Serialization;
using Werkzeugverleih;
using static System.Collections.Specialized.BitVector32;

namespace OBJC_WS2023_Hausarbeit_Schubert
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // main menue: Werkzeuge verwalten, Kunden verwalten, Werkzeuge verleihen
            
            // sub menue Kunden verwalten: Kunde anlegen, Kunde bearbeiten, Kunde löschen

            // sub menue Werkzeuge verwalten: Werkzeug anlegen, Werkzeug bearbeiten, Werkzeug löschen

            // sub menue Werkzeuge verleihen: Ausleihe anlegen, Ausleihe bearbeiten, Ausleihe löschen



            // Screen 1: Einen neuen Customer Anlegen
            char backTick = 'b'; // b: back to main menue
            bool backToMainMenue = false;

            //while (!backToMainMenue)
            //{
            //    Console.WriteLine("Bitte geben Sie einen Vornamen ein: ");
            //    var surname = Convert.ToString(Console.ReadLine());

            //    Console.WriteLine("Bitte geben Sie einen Nachnamen ein: ");
            //    var name = Convert.ToString(Console.ReadLine());

            //    Console.WriteLine("Bitte geben Sie das Gender ein: ");
            //    var gender = Convert.ToString(Console.ReadLine());

            //    Console.WriteLine("Bitte geben Sie ein Birthday ein: ");
            //    var birthday = Convert.ToString(Console.ReadLine());

            //    Console.WriteLine("Bitte geben Sie eine Address ein: ");
            //    var address = Convert.ToString(Console.ReadLine());

            //    Console.WriteLine("Bitte geben Sie eine gültige BankAccount (IBAN) ein: ");
            //    var bankAccount = Convert.ToString(Console.ReadLine());

            //    Console.WriteLine("Bitte geben Sie den CustomerType an (privat/geschaeftlich): ");
            //    var customerType = Convert.ToString(Console.ReadLine());

            //    Console.WriteLine("Bitte geben Sie eine gültige TelephoneNumber ein: ");
            //    var telephoneNumber = Convert.ToString(Console.ReadLine());

            //    Console.WriteLine("Bitte geben Sie eine aktuelle Rating des Kunden an (1 - 5): ");
            //    var rating = Convert.ToInt16(Console.ReadLine());

            //    // Customer anlegen
            //    var kunde = new Customer(
            //        customerId: 1,
            //        surname: surname,
            //        name: name,
            //        gender: gender,
            //        birthday: birthday,
            //        address: address,
            //        bankAccount: bankAccount,
            //        customerType: customerType,
            //        telephoneNumber: telephoneNumber,
            //        rating: rating);

            //    // CustomerManagement aufrufen
            //    var kundenverwaltung = new CustomerManagement<Customer>(kunde);
            //    kundenverwaltung.CreateCustomers();

            //}

            //// Erstelle einen Kunden
            //var kunde1 = new Customer(
            //    surname: "Torsten",
            //    name: "Knallermann",
            //    gender: "maennlich",
            //    birthday: "10.08.2003",
            //    address: "80678 München",
            //    bankAccount: "DE12 3456 1000 0025 2937 90",
            //    customerType: "privat",
            //    telephoneNumber: "0177689654",
            //    rating: 2);


            // CustomerManagement aufrufen
            var customerManagement = new CustomerManagement<Customer>();
            // kundenverwaltung.CreateCustomers();
            // customerManagement.DeleteCustomers(customerIdToRemove: 1);
            customerManagement.EditCustomers(6, "Surname", "Sara");


            //// Erstelle einen Kunden
            //var kunde1 = new Customer(
            //    surname: "Thomas",
            //    name: "Mustermann",
            //    gender: "maennlich",
            //    birthday: "10.08.2003",
            //    address: "80678 München",
            //    bankAccount: "DE12 3456 1000 0025 2937 90",
            //    customerType: "privat",
            //    telephoneNumber: "0177689654",
            //    rating: 2);

            //// Erstelle einen weiteren Kunden
            //Customer kunde2 = new Customer(
            //    surname: "Anna",
            //    name: "Musterfrau",
            //    gender: "weiblich",
            //    birthday: "20.03.1984",
            //    address: "12345 Berlin",
            //    bankAccount: "DE34 5678 1000 0025 9876 54",
            //    customerType: "geschaeftlich",
            //    telephoneNumber: "01551234567",
            //    rating: 3
            //);

            // Füge den Kunden zur Kundenliste hinzu
            //KundenListe.Add(kunde1);
            //KundenListe.Add(kunde2);

            // Erzeuge eine Instanz der CustomerManagement
            //CustomerManagement kundenverwaltung = new CustomerManagement(KundenListe);
            //kundenverwaltung.CreateCustomers(KundenListe);

            // Zeige Kundendaten an
            //kundenverwaltung.ShowCustomerData();

            // Loesche einen Kunden
            //int selection = 1;
            //kundenverwaltung.DeleteCustomers(selection);

            // Zeige Kundendaten an
            //kundenverwaltung.ShowCustomerData();

            // Customer bearbeiten
            //kundenverwaltung.EditCustomers(selectedId: 1, element: "Surname" , newContent: "Max");


        }

    }


}