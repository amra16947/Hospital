using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication6
{
    public partial class Form6 : Form
    {
        public BindingList<VrstaLijekova> lista_lijekova = new BindingList<VrstaLijekova>();
        
        public Form6()
        {
            InitializeComponent();

            VrstaLijekova analgetik = new VrstaLijekova()
            {
                vrsta = "analgetik",
                cijena = 0
            };
            lista_lijekova.Add(analgetik);
            VrstaLijekova antireumatik = new VrstaLijekova()
            {
                vrsta = "antireumatik",
                cijena = 0
            };
            lista_lijekova.Add(antireumatik);
            VrstaLijekova antipiretik = new VrstaLijekova()
            {
                vrsta = "antipiretik",
                cijena = 0
            };
            lista_lijekova.Add(antipiretik);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox2.Text) || String.IsNullOrEmpty(comboBox2.Text))
            {
                MessageBox.Show("Prvo unesite sva polja.");
            }
            else
            {
                VrstaLijekova novi = new VrstaLijekova()
            {
                
                vrsta = comboBox2.Text,
                cijena = (int)numericUpDown2.Value
            };

                if (comboBox2.Text == "analgetik")
                {
                    lista_lijekova[0].cijena+=novi.cijena;
                }
                else if (comboBox2.Text == "antireumatik")
                {
                    lista_lijekova[1].cijena+=novi.cijena;
                }
                else if (comboBox2.Text == "antipiretik")
                {
                    lista_lijekova[2].cijena+=novi.cijena;
                }

                MessageBox.Show("Uspješno dodan lijek");

            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            chart1.DataSource = lista_lijekova;
            chart1.DataBind();
           


         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            chart2.DataSource = lista_lijekova;
            chart2.DataBind();



           
        }
    }

    public class VrstaLijekova
    {
        
        public string vrsta {get; set;}
        public int cijena {get; set;}

       


    }
}
