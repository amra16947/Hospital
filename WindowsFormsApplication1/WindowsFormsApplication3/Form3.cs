using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Klinika123;

namespace WindowsFormsApplication3
{
    public partial class Form3 : Form
    {
        BindingList<Ljekar> lista_ljekara1 = new BindingList<Ljekar>();
        BindingList<Termin> lista_termina = new BindingList<Termin>();
        public Form3(BindingList<Ljekar> ljekari, BindingList<Termin> termin)
        {
            InitializeComponent();
            lista_ljekara1 = ljekari;
            lista_termina = termin;

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            
            DateTime time = dateTimePicker1.Value;
            List<Termin> termini_datuma= new List<Termin>();
            List<string> zauzeti = new List<string>();
            
            List<string> sviTermini = new List<string>();
            sviTermini.Add("07:00");
            sviTermini.Add("08:00");
            sviTermini.Add("09:00");
            sviTermini.Add("10:00");
            sviTermini.Add("11:00");
            sviTermini.Add("12:00");
            sviTermini.Add("13:00");
            sviTermini.Add("14:00");

            foreach (Termin t in lista_termina)
            {
                if (t.Datum == time.ToShortDateString())
                {  
                    termini_datuma.Add(t);
                 
                }
            }
            
            foreach (Osoba o in lista_ljekara1)
            {
               listBox1.Items.Add(o.ToString());
                zauzeti.Clear();
                Ljekar lj = o as Ljekar;
                if (lj != null)
                {
                    foreach (Termin t in termini_datuma)
                    {
                        
                       if(lj.ToString()==t.Doktor1)
                        {
                            zauzeti.Add(t.Termin_vrijeme);
                            
                        }
                    }
                    
                    foreach (string svaki in sviTermini)
                    {
                        bool postoji = false;
                        foreach (string neki in zauzeti)
                        {
                            if (svaki == neki)
                                postoji = true;
                        }
                        if (postoji == true)
                            listBox1.Items.Add(svaki + " - z");
                        if (postoji == false)
                            listBox1.Items.Add(svaki + " - s");
                        

                    }
                }
            

            }


        }

      

       

       
    }
}
