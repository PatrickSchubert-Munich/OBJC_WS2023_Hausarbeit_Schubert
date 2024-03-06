using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Werkzeugverleih
{
    public class Tool
    {
        public string Hersteller { get; set; }
        public string Bezeichnung { get; }
        public string Stromversorgung { get; }

        public Tool(string hersteller, string bezeichnung, string stromversorgung)
        {
            this.Hersteller = hersteller;
            this.Bezeichnung = bezeichnung;
            this.Stromversorgung = stromversorgung;
        }
        
    }
}
