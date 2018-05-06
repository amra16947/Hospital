using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Klinika123;

namespace WindowsFormsApplication1
{
    public partial class UserLjekar : UserControl
    {
        public BindingList<Pacijent> lista_pacijenata = new BindingList<Pacijent>();
        public BindingList<Ljekar> lista_ljekara = new BindingList<Ljekar>();
        public BindingList<Termin> Evidencija_termina = new BindingList<Termin>();

        public bool postojiTabelaUBazi = false;
        public LjekarDB ljekarDB = new LjekarDB();
       // public IzuzetciDB izuzetakDB= new IzuzetciDB();

        public UserLjekar()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = "";
            radioButton1.Checked = true;


            int newID = 1;
            if (lista_ljekara.Count > 0)
                newID = lista_ljekara.Max(t => Int32.Parse(t.ID)) + 1;
            textBox17.Text = newID.ToString();
            textBox17.Enabled = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
          
                string temp="F";
                bool postojiID = false;
                if (radioButton1.Checked) temp = "M";
                
                Ljekar novi = new Ljekar()
                {
                    ID = textBox17.Text,
                    Ime = textBox13.Text,
                    Prezime = textBox14.Text,
                    Telefon = userTelefon3.textBox1.Text + userTelefon3.textBox2.Text + userTelefon3.textBox3.Text,
                    Specijalizacija = comboBox1.Text,
                    Titula = comboBox2.Text,
                    DatumZaposlenja = dateTimePicker4.Value,
                    Spol = temp
                };

                novi.Validate();


                if (MessageBox.Show("Unos novog ljekara?", "Upit", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (postojiTabelaUBazi)
                    {

                        //Insertuj u bazu
                        bool resp = ljekarDB.InsertLjekar(novi);
                        if (resp)
                        {
                            MessageBox.Show("Uspjesno snimljen ljekar u bazu!");
                            //ako je uspjesno dodaj ga i u listuEmployee-a
                            lista_ljekara.Add(novi);

                            textBox13.Text = "";
                            textBox14.Text = "";
                            textBox17.Text = "";
                            radioButton1.Checked = true;
                            radioButton2.Checked = false;
                            comboBox1.Text = "";
                            dateTimePicker4.Value = DateTime.Now;
                            userTelefon3.textBox1.Text = "";
                            userTelefon3.textBox2.Text = "";
                            userTelefon3.textBox3.Text = "";
                            toolStripStatusLabel1.Text = "";

                            int newID = 1;
                            if (lista_ljekara.Count > 0)
                                newID = lista_ljekara.Max(t => Int32.Parse(t.ID)) + 1;
                            textBox17.Text = newID.ToString();
                            textBox17.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show("Ljekar nije snimljen u bazu!");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Prvo kreirajte tabelu u bazi kako biste unijeli ljekara!");
                    }

                  
                }
            }
            catch (OsobaException ex)
            {
                toolStripStatusLabel1.Text = ex.Message;
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = ex.Message;
            }

        }

        private void button12_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Brisanje u toku...", "Obavještenje", MessageBoxButtons.OK);
            textBox13.Text = "";
            textBox14.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            dateTimePicker4.Value = DateTime.Now;
            comboBox1.Text = "";
            userTelefon3.textBox1.Text = "";
            userTelefon3.textBox2.Text = "";
            userTelefon3.textBox3.Text = "";
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
