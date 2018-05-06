using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klinika123
{

    public class PosjetaKlinici
    {
        public Termin TerminP { get; set; }
        public string Dijagnoza { get; set; }
        public string Terapija { get; set; }
        public string RezultatiAnalize { get; set; }
        public string Alergije { get; set; }
        public string Lijekovi { get; set; }

        public void Validate()
        {
            if (String.IsNullOrEmpty(Dijagnoza) || String.IsNullOrEmpty(Terapija) || String.IsNullOrEmpty(Lijekovi))
            {
                throw new Exception("Popunite obavezna polja: Dijagnoza, Terapija, Lijekovi");
            }

        }
    }

    
    public class Termin
    {
        public Ljekar Doktor = new Ljekar();
        public string Termin_vrijeme { get; set; }
        public bool Zauzet { get; set; }
        public string Datum { get; set; }
        public string Doktor1 { get; set; }

        public void Validate()
        {
            if (Convert.ToDateTime(Datum) < DateTime.Now.Date)
                throw new Exception("Neispravan datum.");
        }
        

        public override string ToString()
        {
            
            return string.Format("{0} - {1} ", Datum, Doktor1);
              
        }
    }
}
