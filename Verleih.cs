using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Werkzeugverleih
{
    [Serializable]
    [XmlRoot("Werkzeugverwaltung")]
    public class Verleih
    {

        [XmlElement("Kunde")]
        public List<Kunde> KundenListe { get; set; } = new List<Kunde>();

    }
}
