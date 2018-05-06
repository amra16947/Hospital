using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klinika123
{
   public class Izuzetak: Exception
    {
       public Izuzetak() { }

       public Izuzetak(int s, string t, DateTime d) 
       {
           Sifra = s;
           Text = t;
           Datum = d;
       }

       int sifra;
       public int Sifra
       {
           get { return sifra; }
           set { sifra = value; }
       }
       string text;
       public string Text
       {
           get { return text; }
           set { text = value; }

       }
       DateTime datum;
       public DateTime Datum
       {
           get { return datum; }
           set { datum = value; }
       }

       public override string ToString()
       {
           return "IZUZETAK: " + Text + "      DATUM: " + Datum.ToString(); ;
       }

    }
}
