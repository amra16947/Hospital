using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klinika123
{
    [Serializable]
    public abstract class Osoba: INotifyPropertyChanged
    {
        public Osoba() { }

        public Osoba(string Id, string Ime, string Prezime, string tel)
        {
            ID = Id;
            Ime = ime;
            this.Prezime = Prezime;
            Telefon = tel;

        }
        public string ime;
        public string prezime;
        public string telefon;
        public virtual string ID { get; set; }

        public virtual string Ime
        {
            get { return ime; }
            set { ime = value; }
        }

        public virtual string Prezime
        {
            get { return prezime; }
            set { prezime = value; }
        }

        public virtual string Telefon
        {
            get { return telefon; }
            set 
            { 
                telefon = value;
            NotifyPropertyChanged("Telefon");
            }
        }

        public virtual void Validate()
        {
            if (String.IsNullOrEmpty(Ime) || String.IsNullOrEmpty(Prezime) || String.IsNullOrEmpty(ID))
                throw new OsobaException("Prvo popunite obavezna polja!");
            else
            {
                
                foreach (char c in ID)
                    if (c < '0' || c > '9')
                        throw new OsobaException("ID mora biti broj!");
            }

            if(!String.IsNullOrEmpty(Telefon))
            {
                if (Telefon.Count() != 8)
                    throw new OsobaException("Neispravan unos broja telefona.");

                else
                {
                    foreach(char c in Telefon)
                        if(c<'0' || c>'9')
                            throw new OsobaException("Uneseni broj telefona je neispravan!");

                }
            }
        }

        public virtual event PropertyChangedEventHandler PropertyChanged;
        public virtual void NotifyPropertyChanged(string naziv)
        {
            if (PropertyChanged != null)
            {
                
                PropertyChanged(this, new PropertyChangedEventArgs(naziv));
            }

        }

    } //kraj klase Osoba

    [Serializable]
    public class Ljekar : Osoba, INotifyPropertyChanged
    {
        public Ljekar() : base() { }
        public Ljekar(string Id, string Ime, string Prezime, string tel, string s, string t, DateTime dat, string spol)
            : base(Id, Ime, Prezime, tel)
        {
            Specijalizacija = s;
            Titula = t;
            DatumZaposlenja = dat;
            Spol = spol;
        }
        public override string ID { get; set; }
        public override string Ime
        {
            get;
            set;
        }
        public override string Prezime
        {
            get;
            set;
        }
        public override string Telefon
        {
            get;

            set;
        }


        string specijalizacija;
        public string Specijalizacija
        {
            get
            {
                return specijalizacija;
            }
            set
            {
                specijalizacija  = value;
                NotifyPropertyChanged("Specijalizacija");

            }
        }
        string titula;
        public string Titula
        {
            get
            {
                return titula;
            }
            set
            {
                titula = value;
                NotifyPropertyChanged("Titula");

            }
        }
        DateTime datZaposljavanja;
        public DateTime DatumZaposlenja
        {
            get
            {
                return datZaposljavanja;
            }
            set
            {
                datZaposljavanja = value;
                NotifyPropertyChanged("DatumZaposlenja");

            }
        }
        string spol;
        public string Spol
        {
            get
            {
                return spol;
            }
            set
            {
                spol = value;
                NotifyPropertyChanged("Spol");

            }
        }

        public override string ToString()
        {
            return Titula + " " + Ime + " " + Prezime + " - " + Specijalizacija;
        }

        public override void Validate()
        {
            base.Validate();
            if (DatumZaposlenja >= DateTime.Now.Date.AddDays(1))
            {
                throw new OsobaException("Greška! Unesite ispravan datum!");
            }
            else if (String.IsNullOrEmpty(Titula) && String.IsNullOrEmpty(Specijalizacija))
            {
                throw new OsobaException("Prvo popunite obavezna polja: Specijalizacija i Titula");
            }
            else if (String.IsNullOrEmpty(Titula))
            {
                throw new OsobaException("Prvo popunite obavezno polje: Titula");
            }
            else if (String.IsNullOrEmpty(Specijalizacija))
            {
                throw new OsobaException("Prvo popunite obavezno polje: Specijalizacija");
            }

        }

        public override event PropertyChangedEventHandler PropertyChanged;
        public override void NotifyPropertyChanged(string naziv)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(naziv));

        }

    }

  [Serializable]
    public class Pacijent : Osoba, INaplati, INotifyPropertyChanged
        {

        public Pacijent() : base() { }

        public Pacijent(string Id, string ime, string prezime, string telefon, string adresa, string datRodj, bool popust, double d)
            : base(Id, ime, prezime, telefon)
        {
            DatRodjenja = datRodj;
            Popust = popust;
            Dug = d;
        }

        string adresa;

        public override string ID { get; set; }

        
        public string Adresa
        {
            get 
            { 
                return adresa;
            }
            set 
            {
                adresa = value;
               NotifyPropertyChanged("Adresa");
            }
        }
        string datRodjenja;
            public string DatRodjenja
        {
            get
            {
                return datRodjenja;
            }
            set
            {
                datRodjenja = value;
                NotifyPropertyChanged("DatRodjenja");
            }
        }
            public List<Termin> Zakazani_termini = new List<Termin>();
            public List<PosjetaKlinici> Pregled = new List<PosjetaKlinici>();
            bool popust;
            public bool Popust
            {
                get
                {
                    return popust;
                }
                set
                {
                    popust = value;
                    NotifyPropertyChanged("Popust");

                }
            }

            public override string ToString()
            {
                return Ime + " " + " " + Prezime +"  Datum rođenja: "+ DatRodjenja+"  ID: " + ID + "  Telefon: +387" + Telefon;
            }

            public double Dug { get; set; }
            public void ObracunajNoviPregled()
            {
                Dug += 50;
            }
            public double Racun()
            {
                if (Popust == true)
                    return Dug *= 0.9;
                else
                    return Dug;
            }
            public override void Validate()
            {
                base.Validate();

                

                if (Convert.ToDateTime(DatRodjenja) >= DateTime.Now.Date.AddDays(1))
                {
                    throw new OsobaException("Greška! Unesite ispravan datum!");

                }
               
            }

            public override event PropertyChangedEventHandler PropertyChanged;
            public override void NotifyPropertyChanged(string naziv)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(naziv));

            }

        }

    }
