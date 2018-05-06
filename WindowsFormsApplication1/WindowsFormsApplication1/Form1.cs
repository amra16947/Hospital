using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Klinika123;
using WindowsFormsApplication2;
using WindowsFormsApplication3;
using WindowsFormsApplication4;
using WindowsFormsApplication5;
using WindowsFormsApplication6;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;



namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
      
        public BindingList<Termin> Evidencija_termina = new BindingList<Termin>();
        IzuzetciDB izuzetakDB = new IzuzetciDB();
        public int ID = 1;
        List<string> path_za_pjesme = new List<string>();
        List<string> tipovi_vremena = new List<string>();
        
        
      
        Pacijent noviID = new Pacijent();
        private string naziv_xml;

        /*
           Što se tiče 4. zadatka iz ove zadaće 3, ja sam u svojoj formi primjenjivala principe sličnosti i udaljenosti.

           Vizuelno sam povezivala elemente preko groupbox-ova, labele sam približila textbox-ovima.
         
          Princip sličnosti sam iskoristila tako što sam grupirala podatke u odvojene groupbox-eve, gdje unutar njih su textbox-evi iste velicine, labele poravnate itd..
          
           U postojećim klasam sam dodala metodu void Validate(), koja mi pomaže prilikom validacije na nivou klase.
         
       
           Takođe, pored svega toga, u terćoj zadaći sam implementirao vlastitu korisničku kontrolu, gdje sam takođe primijenjivala odmah principe bliskosti, sličnosti i ostale gestalt principe.
           Takođe, u ovoj zadaći je implementirana i validacija svih ulaznih podataka, tako da je sada forma postala znatno ozbiljnija.
           
           Implementirano je i upravljanje izuzecima, mada se sami izuzeci nisu puno pojavljivali, obzirom da je validacija pokrila veliki broj eventualnih izuzetaka.
           
*/


        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;

        public Form1()
        {
            InitializeComponent();
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox4.Visible = false;
            groupBox10.Visible = false;
            groupBox11.Visible = false;
            groupBox12.Visible = false;
           // groupBox14.Visible = false;
            toolStripStatusLabel1.Text = "";
            toolStripStatusLabel2.Text = "";
            toolStripStatusLabel3.Text = "";
            //toolStripStatusLabel4.Text = "";

            timer1.Start();
            

              
            // Check box 1 ručno unošenje
            checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox7.Controls.Add(this.checkBox1);
            checkBox1.AutoSize = true;
            checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            checkBox1.Location = new System.Drawing.Point(35, 48);
            checkBox1.Margin = new System.Windows.Forms.Padding(4);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(71, 21);
            checkBox1.TabIndex = 12;
            checkBox1.Text = "Popust";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);

            //CheckBox2
            checkBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox12.Controls.Add(this.checkBox2);
            checkBox2.AutoSize = true;
            checkBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            checkBox2.Location = new System.Drawing.Point(37, 52);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new System.Drawing.Size(117, 22);
            checkBox2.TabIndex = 12;
            checkBox2.Text = "Račun plaćen";
            checkBox2.UseVisualStyleBackColor = true;

           
            dataGridView1.DataSource = userLjekar1.lista_ljekara;
           
            toolStripStatusLabel5.Text = "";

            groupBox16.Visible = false;
            groupBox13.Visible = false;
            listBox1.DataSource = userLjekar1.lista_ljekara;
            listBox2.DataSource = userLjekar1.lista_pacijenata;
            label56.Text = "";
            label57.Text = "";

            path_za_pjesme.Add("happy.wav");
            path_za_pjesme.Add("kisa_pada.wav");
            path_za_pjesme.Add("oblacno.wav");
            path_za_pjesme.Add("magla.wav");

            tipovi_vremena.Add("Pretežno sunčano");
            tipovi_vremena.Add("Kišovito");
            tipovi_vremena.Add("Promjenjivo oblačno");
            tipovi_vremena.Add("Maglovito");
            
           
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
          

                bool temp, postojiID = false;
                if (checkBox1.Checked) temp = true;
                else temp = false;

                Pacijent novi = new Pacijent()
                {
                    Ime = textBox1.Text,
                    Prezime = textBox2.Text,
                    Telefon = userTelefon1.textBox1.Text + userTelefon1.textBox2.Text + userTelefon1.textBox3.Text,
                    Adresa = textBox3.Text,
                    ID = textBox5.Text,
                    DatRodjenja = dateTimePicker1.Value.ToShortDateString(),
                    Popust = temp


                };

                novi.Validate();

                foreach (Osoba o in userLjekar1.lista_pacijenata)
                {

                    if (o.ID == textBox5.Text)
                        postojiID = true;

                }
                if (textBox5.Text == "" || postojiID == true)
                    MessageBox.Show("ID već postoji.", "Upozorenje", MessageBoxButtons.OK);

                else if (MessageBox.Show("Unos pacijenta?", "Upit", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    userLjekar1.lista_pacijenata.Add(novi);
                    dataGridView1.DataSource = userLjekar1.lista_pacijenata;
                    
                    MessageBox.Show("Novi pacijent je unesen.", "Obavještenje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    userTelefon1.textBox1.Text = "";
                    userTelefon1.textBox2.Text = "";
                    userTelefon1.textBox3.Text = "";
                    textBox5.Text = "";
                    checkBox1.Checked = false;
                    dateTimePicker1.Value = DateTime.Now;
                    toolStripStatusLabel1.Text = "";
                    errorProvider1.Clear();

                    //postavi ID
                    int newID = 1;
                    if (userLjekar1.lista_pacijenata.Count > 0)
                        newID = userLjekar1.lista_pacijenata.Max(t => Int32.Parse(t.ID)) + 1;
                    textBox5.Text = newID.ToString();
                    textBox5.Enabled = false;
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


        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Brisanje u toku...", "Obavještenje", MessageBoxButtons.OK);
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            userTelefon1.textBox1.Text = "";
            userTelefon1.textBox2.Text = "";
            userTelefon1.textBox3.Text = "";
            textBox5.Text = "";
            checkBox1.Checked = false;
            dateTimePicker1.Value = DateTime.Now;
            errorProvider1.Clear();
            toolStripStatusLabel1.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string pomocID = textBox6.Text;
            bool postoji = false;
            label27.Visible = true;

            foreach (Osoba o in userLjekar1.lista_pacijenata)
                {
                    Pacijent p = o as Pacijent;
                    if (p != null)
                        postoji = true;
                }

                if (postoji == false)
                    MessageBox.Show("Nije registrovan ni jedan pacijent", "Obavještenje", MessageBoxButtons.OK);

                postoji = false;


                foreach (Osoba o in userLjekar1.lista_pacijenata)
            {
                Pacijent p = o as Pacijent;

            if (p != null)
            {
                if (p.ID == pomocID)
                {
                    noviID = p;
                    postoji = true;
                    label8.Text = p.Ime;
                    label9.Text = p.Prezime;
                    label10.Text = p.Adresa;
                    label11.Text = p.Telefon;
                    if (string.IsNullOrEmpty(p.Telefon))
                        label27.Visible = false;
                    groupBox1.Visible = true;
                }

             }
            }
             if (postoji == false)
            {
                groupBox1.Visible = false;
                MessageBox.Show("Ne postoji pacijent s tim ID.", "Obavještenje", MessageBoxButtons.OK);

            }
            else
            {
                 comboBox3.Items.Clear();
                 foreach (Osoba p in userLjekar1.lista_ljekara)
                 {
                     Ljekar lj= p as Ljekar;
                     if(lj!=null)
                     {
                         comboBox3.Items.Add(lj.ToString());
                     }


                 }
                MessageBox.Show("Ispravan ID.", "Obavještenje", MessageBoxButtons.OK);
                groupBox1.Visible = true;

                button3.Enabled = false;
               


            }

        }

        private void button8_Click(object sender, EventArgs e) //pregled unos id
        {
            comboBox6.Items.Clear();
            string pomocID = textBox6.Text;
            bool postoji = false;

            foreach (Osoba o in userLjekar1.lista_pacijenata)
            {
                Pacijent p = o as Pacijent;
                if (p != null)
                    postoji = true;
            }
            if(postoji==false)
                MessageBox.Show("Obavještenje", "Nije registrovan ni jedan pacijent", MessageBoxButtons.OK);

            postoji = false;

            foreach (Osoba o in userLjekar1.lista_pacijenata)
            {
                Pacijent p = o as Pacijent;
            if(p!=null)
            {
                if (p.ID==textBox7.Text)
                {
                    noviID = p;
                    postoji = true;
                    label12.Text = p.Ime;
                    label16.Text = p.Prezime;
                    label19.Text = p.Telefon;

                    foreach (Termin t in p.Zakazani_termini)
                        comboBox6.Items.Add(t.ToString());
                }
                    
             }
            }
            

            if (postoji == false)
            {
                groupBox2.Visible = false;
                groupBox10.Visible = false;
                groupBox4.Visible = false;
                MessageBox.Show("Ne postoji pacijent s tim ID.", "Obavještenje", MessageBoxButtons.OK);

            }
            else
            {
                groupBox10.Visible = true;
                groupBox4.Visible = true;

            }

        }

        private void button5_Click(object sender, EventArgs e) //Unos termina
        {
            Termin novi = new Termin();
            List<string> zauzeti= new List<string>();
            List<string> sviTermini= new List<string>();
            sviTermini.Add("07:00");
            sviTermini.Add("08:00");
            sviTermini.Add("09:00");
            sviTermini.Add("10:00");
            sviTermini.Add("11:00");
            sviTermini.Add("12:00");
            sviTermini.Add("13:00");
            sviTermini.Add("14:00");


            Ljekar novi_ljekar = (Ljekar)comboBox3.SelectedValue;

            foreach (Termin t in userLjekar1.Evidencija_termina)
            {
                 if (t.Doktor == novi_ljekar)
                {
                    if (t.Datum == dateTimePicker2.Value.ToShortDateString())
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
                if (postoji == false)
                    comboBox4.Items.Add(svaki);
            }
        }

        private void button9_Click(object sender, EventArgs e) //unos pregleda
        {
            try
            {
                PosjetaKlinici novi_pregled = new PosjetaKlinici()
              {
                  TerminP = (Termin)comboBox6.SelectedValue,

                  Dijagnoza = textBox8.Text,
                  Terapija = textBox9.Text,
                  RezultatiAnalize = textBox10.Text,
                  Alergije = textBox11.Text,
                  Lijekovi = textBox12.Text

              };

                novi_pregled.Validate();

                if (MessageBox.Show("Unosom pregleda?", "Upit", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    foreach (Osoba o in userLjekar1.lista_pacijenata)
                    {
                        Pacijent p = o as Pacijent;
                        if (p != null)
                        {
                            if (p.ID == textBox7.Text)
                            {
                                p.Pregled.Add(novi_pregled);
                                p.ObracunajNoviPregled();
                            }

                        }
                    }
                    noviID.Pregled.Add(novi_pregled);
                    MessageBox.Show("Pregled uspješno unesen.", "Obavještenje!", MessageBoxButtons.OK);
                    groupBox2.Visible = false;
                    groupBox10.Visible = false;
                    groupBox4.Visible = false;
                    button8.Enabled = true;
                    button13.Enabled = true;
                    textBox7.Text = "";
                    textBox8.Text = "";
                    textBox9.Text = "";
                    textBox10.Text = "";
                    textBox11.Text = "";
                    textBox12.Text = "";

                    foreach (Osoba o in userLjekar1.lista_pacijenata)
                    {
                        Pacijent p = o as Pacijent;
                        if (p != null)
                        {
                            for (int i = 0; i < p.Zakazani_termini.Count; i++)
                            {

                                if (p.Zakazani_termini[i].ToString() == comboBox6.Text)
                                {

                                    p.Zakazani_termini.RemoveAt(i);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel3.Text = ex.Message; 
            }
            
        }


        private void button5_Click_1(object sender, EventArgs e) //zakazivanje termina
        {
            try
            {
                if (comboBox3.Items.Count == 0)
                    MessageBox.Show("Ne postoji ni jedan ljekar, prvo unesite ljekare", "Obavještenje", MessageBoxButtons.OK);
                else if (comboBox4.Items.Count == 0)
                    MessageBox.Show("Izaberite drugi datum ili ljekara, svi termini za navedeni datum su zauzeti.", "Obavještenje", MessageBoxButtons.OK);
                else if (comboBox3.Text != "" && comboBox4.Text != "")
                {

                    Termin novi = new Termin()
                    {
                        Doktor = comboBox3.SelectedItem as Ljekar,
                        Termin_vrijeme = comboBox4.Text,
                        Datum = dateTimePicker2.Value.ToShortDateString(),
                        Zauzet = true,
                        Doktor1 = comboBox3.Text

                    };
                    novi.Validate();

                    userLjekar1.Evidencija_termina.Add(novi);
                    for (int i = 0; i < userLjekar1.lista_pacijenata.Count; i++)
                    {
                        Pacijent p = userLjekar1.lista_pacijenata[i] as Pacijent;
                        if (p != null)
                        {
                            if (p.ID == textBox6.Text)
                            {
                                p.Zakazani_termini.Add(novi);
                                userLjekar1.lista_pacijenata[i] = p;
                            }
                        }
                    }

                    MessageBox.Show("Uspješno zakazan novi termin.", "Obavještenje", MessageBoxButtons.OK);
                    textBox6.Text = "";
                    comboBox3.Text = "";
                    comboBox4.Items.Clear();
                    groupBox1.Visible = false;
                    button3.Enabled = true;


                }
                else MessageBox.Show("Izaberite ljekara ili termin ispravo.", "Obavještenje", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                toolStripStatusLabel2.Text = ex.Message;
            }
           

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e) //odustani od unosa termina
        {
            groupBox1.Visible = false;
            textBox6.Text = "";
            button3.Enabled = true;
           


        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void button7_Click(object sender, EventArgs e)
        {
            Form3 nova = new Form3(userLjekar1.lista_ljekara, userLjekar1.Evidencija_termina);
            nova.Show();
        }

        private void button13_Click(object sender, EventArgs e) //oDABERI zakazani pregled
        {
            if (comboBox6.Items.Count == 0)
                MessageBox.Show("Nemate zakazanih termina.", "Obavještenje", MessageBoxButtons.OK);
            else
            {  
                if(MessageBox.Show("Da li ste izabrali pregled?", "Upit", MessageBoxButtons.OKCancel)==DialogResult.OK)
                {
                Termin t = comboBox6.SelectedItem as Termin;
            
                groupBox2.Visible=true;
                button8.Enabled = false;
                button13.Enabled = false;
                
                }

            }
        }

      

      

        private void label39_Click(object sender, EventArgs e)
        {

        }

        private void groupBox11_Enter(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e) //pronadji ID za račun
        {
            string pomocID = textBox16.Text;
            bool postoji = false;

            foreach (Osoba o in userLjekar1.lista_pacijenata)
                {
                    Pacijent p = o as Pacijent;
                    if (p != null)
                        postoji = true;
                }

                if (postoji == false)
                    MessageBox.Show("Nije registrovan ni jedan pacijent", "Obavještenje", MessageBoxButtons.OK);

                postoji = false;


                foreach (Osoba o in userLjekar1.lista_pacijenata)
            {
                Pacijent p = o as Pacijent;

            if (p != null)
            {
                if (p.ID == pomocID)
                {
                    noviID = p;
                    postoji = true;
                    label37.Text = DateTime.Now.ToString();
                    label40.Text = p.Dug.ToString();
                    if(p.Popust== true)
                    label42.Text = "10 %";
                    else
                    label42.Text = "0 %";
                    label44.Text = p.Racun().ToString();
                    if (p.Dug == 0)
                    {
                        MessageBox.Show("Pacijent nema dugovanja. Svoje dugove uredno izmiruje.", "Obavještenje", MessageBoxButtons.OK);
                        
                    }
                    else
                    {
                        groupBox11.Visible = true;
                        groupBox12.Visible = true;
                    }

                }

             }
            }
             if (postoji == false)
            {
                groupBox11.Visible = false;
                groupBox12.Visible = false;
                MessageBox.Show("Ne postoji pacijent s tim ID.", "Obavještenje", MessageBoxButtons.OK);

            }
           

        }

        private void button15_Click(object sender, EventArgs e) //Potvrdi placanje
        {
            if (checkBox2.Checked == true)
            {
                MessageBox.Show("Račun je plaćen.", "Obavještenje!", MessageBoxButtons.OK);
                foreach (Osoba o in userLjekar1.lista_pacijenata)
                {
                    Pacijent p = o as Pacijent;
                    if (p != null)
                        if (p.ID == textBox16.Text)
                            p.Dug = 0;
                }
            }
            else if (checkBox2.Checked == false)
                MessageBox.Show("Pacijent ima dugovanja još uvijek!", "Obavještenje!", MessageBoxButtons.OK);
              groupBox11.Visible = false;
              groupBox12.Visible = false;

        }

        private void comboBox4_Click(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();

            {

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


                Ljekar novi_lj = comboBox3.SelectedItem as Ljekar;



                foreach (Termin t in userLjekar1.Evidencija_termina)
                {
                    if (t.Doktor == novi_lj)
                    {
                        if (t.Datum == dateTimePicker2.Value.ToShortDateString())
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
                    if (postoji == false)
                        comboBox4.Items.Add(svaki);
                }
            }

        }
     
        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "Obavezno polje!");
                toolStripStatusLabel1.Text = "Popunite obavezno polje!";
            }
            
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "Obavezno polje");
                toolStripStatusLabel1.Text = "Popunite obavezno polje!";
            }


        }

        private void dateTimePicker1_Validating(object sender, CancelEventArgs e)
        {
            if ((sender as DateTimePicker).Value.Date >= DateTime.Now.Date.AddDays(1))
            {
                errorProvider1.SetError(dateTimePicker1, "Neispravan datum!");
                toolStripStatusLabel1.Text = "Greška!";
            }
            else
            {
                errorProvider1.Clear();
                toolStripStatusLabel1.Text = "";
            }

        }

        private void textBox5_Validating(object sender, CancelEventArgs e)
        {

            if (String.IsNullOrEmpty(textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "Obavezno polje!");
                toolStripStatusLabel1.Text = "Greška!";
            }
            else
            {
                errorProvider1.Clear();
                toolStripStatusLabel1.Text = "";
            }
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                errorProvider1.Clear();
                toolStripStatusLabel1.Text = "";
            }

        }

        private void textBox2_Validated(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox2.Text))
            {
                errorProvider1.Clear();
                toolStripStatusLabel1.Text = "";
            }
           

        }


        private void userTelefon1_Validating(object sender, CancelEventArgs e)
        {
            int i = 1;
            

            if (string.IsNullOrEmpty(userTelefon1.textBox1.Text) || string.IsNullOrEmpty(userTelefon1.textBox2.Text) || string.IsNullOrEmpty(userTelefon1.textBox3.Text))
            {
                errorProvider1.SetError(userTelefon1, "Format:  (+387) xx/xxx-xxx ");
                toolStripStatusLabel1.Text="Obavezan format broja telefona je (+387) XX/XXX-XXX "; 
                i=-1;
            }
            else if(userTelefon1.textBox1.Text.Count()!=2 || userTelefon1.textBox2.Text.Count()!=3 || userTelefon1.textBox3.Text.Count()!=3)
            {
                 errorProvider1.SetError(userTelefon1, "Format:  (+387) xx/xxx-xxx ");
                toolStripStatusLabel1.Text="Obavezan format broja telefona je (+387) XX/XXX-XXX "; 
                i=-1;
            }
            else if(i==1)
            {
                foreach(char c in userTelefon1.textBox1.Text)
                    if(c<'0' || c>'9')
                    {
                        errorProvider1.SetError(userTelefon1, "Neispravan unos!");
                        toolStripStatusLabel1.Text="NEISPRAVAN UNOS! Obavezan format broja telefona je (+387) XX/XXX-XXX "; 
                        i=-1;
                    }

                foreach(char c in userTelefon1.textBox2.Text)
                    if(c<'0' || c>'9')
                    {
                        errorProvider1.SetError(userTelefon1, "Neispravan unos!");
                        toolStripStatusLabel1.Text="NEISPRAVAN UNOS! Obavezan format broja telefona je (+387) XX/XXX-XXX "; 
                        i=-1;
                    }

                foreach(char c in userTelefon1.textBox3.Text)
                    if(c<'0' || c>'9')
                    {
                        errorProvider1.SetError(userTelefon1, "Neispravan unos!");
                        toolStripStatusLabel1.Text="NEISPRAVAN UNOS! Obavezan format broja telefona je (+387) XX/XXX-XXX "; 
                        i=-1;
                    }

            }
          
            if(i==1)
            {
                errorProvider1.Clear();
                toolStripStatusLabel1.Text = "";
            }
        }

        //UNAKRSNA VALIDACIJA
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if ((DateTime.Now.Date.Year - (sender as DateTimePicker).Value.Date.Year) >= 60)
            { //da bi nekako pokazala primjer unakrsne validacije, pretpostavila sam da osoba starija od 60 godina, je penzioner sigurn
                // a to znaci da ima popust
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }
        }

        private void dateTimePicker2_Validating(object sender, CancelEventArgs e)
        {
            if ((sender as DateTimePicker).Value.Date < DateTime.Now.Date)
            {
                errorProvider1.SetError(dateTimePicker2, "Datum neispravan!");
                toolStripStatusLabel2.Text = " Nije moguće zakazati termin za datum koji je prošao.";
            }
            else
            {
                errorProvider1.Clear();
                toolStripStatusLabel2.Text = "";

            }
        }

        private void textBox8_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(textBox8.Text))
            {
                errorProvider1.SetError(textBox8, "Obavezno polje");
                toolStripStatusLabel3.Text = "Popunite obavezno polje";
   
            }
            else
            {
                errorProvider1.Clear();
                toolStripStatusLabel3.Text = "";
            }
        }

        private void textBox9_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(textBox9.Text))
            {
                errorProvider1.SetError(textBox9, "Obavezno polje");
                toolStripStatusLabel3.Text = "Popunite obavezno polje";

            }
            else
            {
                errorProvider1.Clear();
                toolStripStatusLabel3.Text = "";
            }

        }

        private void textBox12_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(textBox12.Text))
            {
                errorProvider1.SetError(textBox12, "Obavezno polje");
                toolStripStatusLabel3.Text = "Popunite obavezno polje";

            }
            else
            {
                errorProvider1.Clear();
                toolStripStatusLabel3.Text = "";
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            button8.Enabled = true;
            button13.Enabled = true;
            errorProvider1.Clear();
            toolStripStatusLabel3.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            groupBox2.Visible = false;
            groupBox10.Visible = false;
            groupBox4.Visible = false;
        }


        public string FileName;

     
        public string prethodni;
       

     /*   private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            bool neispravan = false;
            
            if (e.ColumnIndex == 3)
            {
            
                string value = (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value).ToString();
                if (value.ToString() != string.Empty)
                {
                    if (value.Count() != 8)
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = prethodni;
                        neispravan = true;
                        toolStripStatusLabel5.Text = "Unijeli ste neispravan format broja telefona (format ima 8 cifara)";

                    }
                    else
                    {
                        foreach (char c in value)
                            if (c < '0' || c > '9')
                            {
                                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = prethodni;
                                neispravan = true;
                                toolStripStatusLabel5.Text = "Unijeli ste neispravan format broja telefona (format ima 8 cifara)";
                            }
                    }

                    if (neispravan == false)
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = value;
                        toolStripStatusLabel5.Text = "";
                    }
               
                }

                else
                {

                    toolStripStatusLabel5.Text = "Unijeli ste neispravan format broja telefona (format ima 8 cifara)";
                }
            }
            }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            prethodni = (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value).ToString();
        }
        */
        private void treeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 formaTreeView = new Form4(naziv_xml);
            formaTreeView.Show();
        }

        

        private void groupBox6_Enter(object sender, EventArgs e)
        {
            int newID = 1;
            if (userLjekar1.lista_pacijenata.Count > 0)
                newID = userLjekar1.lista_pacijenata.Max(t => Int32.Parse(t.ID)) + 1;
            textBox5.Text = newID.ToString();
            textBox5.Enabled = false;
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            userLjekar1.lista_ljekara = userLjekar1.ljekarDB.ReadAllLjekar();
            listBox1.DataSource = userLjekar1.lista_ljekara;
            
        }

        private void creareDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool result = userLjekar1.ljekarDB.CreateLjekarTable();
            if (result)
            {
                MessageBox.Show("Uspjesno kreirana tabela");
                userLjekar1.postojiTabelaUBazi = true;
            
            }
            else
                MessageBox.Show("Greska pri kreiranju tabele");
        }

        private void dropDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool result = userLjekar1.ljekarDB.DropLjekarTable();
            if (result)
            {
                MessageBox.Show("Uspjesno obrisana tabela");
                userLjekar1.postojiTabelaUBazi = false;
            }
            else
                MessageBox.Show("Greska pri brisanju tabele");
        }

        private void Obrisi_Click(object sender, EventArgs e)
        {
            Ljekar obrisi_ljekara = (Ljekar)listBox1.SelectedItem;
           
                userLjekar1.ljekarDB.DeleteLjekar(obrisi_ljekara);
            userLjekar1.lista_ljekara.Remove(obrisi_ljekara);
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            
            Ljekar izmjeni_ljekara = (Ljekar)listBox1.SelectedItem;
            if (izmjeni_ljekara != null)
            {
                label30.Text = izmjeni_ljekara.Ime;
                label33.Text = izmjeni_ljekara.Prezime;
                label58.Text = izmjeni_ljekara.Specijalizacija;
                textBox4.Text = izmjeni_ljekara.Titula;
                string[] niz = new string[8];
                int i = 0;
                foreach (char c in izmjeni_ljekara.Telefon)
                {
                    niz[i] = c.ToString();
                    i++;
                }
                userTelefon3.textBox1.Text = niz[0] + niz[1];
                userTelefon3.textBox2.Text = niz[2] + niz[3] + niz[4];
                userTelefon3.textBox3.Text = niz[5] + niz[6] + niz[7];

                groupBox16.Visible = true;
                Obrisi.Enabled = false;
            }
        }

        private void button20_Click(object sender, EventArgs e)//sačuvaj izmjene
        {
            try
            {
                Ljekar izmjeni_ljekara = (Ljekar)listBox1.SelectedItem;
                Ljekar proba = new Ljekar()
                { 
                    ID="1111111",
                    Ime = label30.Text,
                    Prezime = label33.Text,
                    Specijalizacija = label58.Text,
                    Titula = textBox4.Text,
                    
                    Telefon = userTelefon3.textBox1.Text + userTelefon3.textBox2.Text + userTelefon3.textBox3.Text

                };

                proba.Validate();

                izmjeni_ljekara.Titula = textBox4.Text;
                izmjeni_ljekara.Telefon = userTelefon3.textBox1.Text + userTelefon3.textBox2.Text + userTelefon3.textBox3.Text;

                userLjekar1.ljekarDB.UpdateLjekar(izmjeni_ljekara);

                groupBox16.Visible = false;
                Obrisi.Enabled = true;
            }
            catch (OsobaException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            groupBox16.Visible = false;
            Obrisi.Enabled = true;
        }

        private void button18_Click_1(object sender, EventArgs e)//Obrisi pacijenta
        {
            Pacijent obrisi_pacijenta = (Pacijent)listBox2.SelectedItem;

            userLjekar1.lista_pacijenata.Remove(obrisi_pacijenta);
        }

        private void button16_Click_1(object sender, EventArgs e)
        {
            try
            {
                Pacijent izmjeni_pacijenta = (Pacijent)listBox2.SelectedItem;

                Pacijent proba = new Pacijent()
                   {
                       Ime = "proba",
                       Prezime = "Proba",
                       ID = "11000001",
                       Telefon = userTelefon2.textBox1.Text + userTelefon2.textBox2.Text + userTelefon2.textBox3.Text,

                   };

                proba.Validate();
                if (izmjeni_pacijenta.Popust == true)
                    checkBox3.Checked = true;

                if (MessageBox.Show("Sačuvaj promjene?", "Upit", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    foreach (Pacijent p in userLjekar1.lista_pacijenata)
                    {
                        if (izmjeni_pacijenta.ID == p.ID)
                        {
                            p.Adresa = textBox13.Text;
                           p.Telefon = userTelefon2.textBox1.Text + userTelefon2.textBox2.Text + userTelefon2.textBox3.Text;
                        }
                    }
                  
                    groupBox13.Visible = false;
                    button18.Enabled = true;
                }
            }
            catch (OsobaException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            
            Pacijent izmjeni_pac = (Pacijent)listBox2.SelectedItem;
            if (izmjeni_pac != null)
            {
                label55.Text = izmjeni_pac.Ime;
                label53.Text = izmjeni_pac.Prezime;
                label50.Text = izmjeni_pac.DatRodjenja;
                textBox13.Text = izmjeni_pac.Adresa;
                string[] niz = new string[8];
                int i = 0;
                foreach (char c in izmjeni_pac.Telefon)
                {
                    niz[i] = c.ToString();
                    i++;
                }
                userTelefon2.textBox1.Text = niz[0] + niz[1];
                userTelefon2.textBox2.Text = niz[2] + niz[3] + niz[4];
                userTelefon2.textBox3.Text = niz[5] + niz[6] + niz[7];

                groupBox13.Visible = true;
                button18.Enabled = false;
            }
        }

        private void listBox2_Click(object sender, EventArgs e)
        {
            groupBox13.Visible = false;
            button18.Enabled = true;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<Pacijent>));
                SaveFileDialog sfd = new SaveFileDialog() { FileName = "pacijenti.xml", AddExtension = true, DefaultExt = "xml", };
                if (sfd.ShowDialog() == DialogResult.OK && sfd.FileName.EndsWith(".xml"))
                {
                    using (Stream s = File.Create(sfd.FileName))
                        xs.Serialize(s, userLjekar1.lista_pacijenata.ToList<Pacijent>());
                }
            }
            catch (SerializationException p)
            {
                Izuzetak novi = new Izuzetak(ID, p.GetType().ToString(), DateTime.Now);
                izuzetakDB.InsertIzuzetak(novi);
                ID++;

                MessageBox.Show(p.Message);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                OpenFileDialog opf = new OpenFileDialog() { CheckFileExists = true };
                if (opf.ShowDialog() == System.Windows.Forms.DialogResult.OK && opf.FileName.EndsWith(".xml"))
                {
                    using (FileStream fs = new FileStream(opf.FileName, FileMode.Open))
                    {
                        naziv_xml = opf.FileName;
                        FileName = opf.FileName;
                        XmlReader reader = XmlReader.Create(fs);
                        
                          XmlSerializer xs = new XmlSerializer(typeof(BindingList<Pacijent>));
                    
                        BindingList<Pacijent> tmp = xs.Deserialize(reader) as BindingList<Pacijent>;
                       
                        if (tmp != null)
                        {
                            userLjekar1.lista_pacijenata = tmp;
                            dataGridView1.DataSource = userLjekar1.lista_pacijenata;
                            listBox2.DataSource = userLjekar1.lista_pacijenata;

                        }
                    }
                }
            }
            catch (SerializationException p)
            {
                Izuzetak novi = new Izuzetak(ID, p.GetType().ToString(), DateTime.Now);
                izuzetakDB.InsertIzuzetak(novi);
                ID++;

                MessageBox.Show(p.Message);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {

                IFormatter serializer = new BinaryFormatter();
                SaveFileDialog std = new SaveFileDialog() { FileName = "pacijenti.bin", AddExtension = true, DefaultExt = "bin", };
                if (std.ShowDialog() == DialogResult.OK && std.FileName.EndsWith(".bin"))
                {
                        using (Stream s = File.Create(std.FileName))
                        serializer.Serialize(s, userLjekar1.lista_pacijenata.ToList<Pacijent>());
                   
                }
            }
            catch (SerializationException p)
            {
                BindingList<Izuzetak> izuzetci = new BindingList<Izuzetak>();

                izuzetci = izuzetakDB.ReadAllIzuzetak();
                Izuzetak novi = new Izuzetak(izuzetci.Count + 1, p.Message, DateTime.Now);
                 izuzetakDB.InsertIzuzetak(novi);
                ID++;

                MessageBox.Show(p.Message);
                
                
            }
            catch (IOException p)
            {
            }
        }

        private void listViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 listViewprikaz = new Form2(naziv_xml);
            listViewprikaz.Show();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void createDBIzuzetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool result = izuzetakDB.CreateIzuzetakTable();
            if (result)
            {
                MessageBox.Show("Uspjesno kreirana tabela");
                userLjekar1.postojiTabelaUBazi = true;

            }
            else
                MessageBox.Show("Greska pri kreiranju tabele");
        }

        private void loadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BindingList<Izuzetak> izuzetci = new BindingList<Izuzetak>();

            izuzetci = izuzetakDB.ReadAllIzuzetak();
            Form5 nova = new Form5(izuzetci);
            nova.Show();
        }

        private void binarnaPacijentToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }



        private delegate void Update();

        int brojac_koji_nisu_null = new int();

        bool izuzetak_xml_i_txt;

        public void brojacDatoteka()
        {
            string[] fajlovi1 = new string[1];
            string[] fajlovi2 = new string[1];

            List<string> rez = new List<string>();

            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                izuzetak_xml_i_txt = false;

                try
                {
                    fajlovi1 = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.xml", SearchOption.AllDirectories);
                   fajlovi2 = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.txt", SearchOption.AllDirectories);
                }
                catch (Exception ex)
                {
                    izuzetak_xml_i_txt = true;
                }

                foreach (string s in fajlovi1)
                    rez.Add(s);

                foreach (string s in fajlovi2)
                    rez.Add(s);



                brojac_koji_nisu_null = 0;

                for (int i = 0; i < rez.Count; i++)
                {
                    if (rez[i] != null)
                    {
                        listView1.Invoke(new Update(() => listView1.Items.Add(rez[i])));
                        brojac_koji_nisu_null++;

                        this.Invoke(new Action(() =>
                        {
                            label56.Text = brojac_koji_nisu_null.ToString();
                        }));
                    }

                    Thread.Sleep(20);
                }


                this.Invoke(new Action(() =>
                {
                    if (izuzetak_xml_i_txt == true)
                        label56.Text = brojac_koji_nisu_null.ToString() + " (došlo je do bacanja\n izuzetka prilikom pretrage)";
                    else
                        label56.Text = brojac_koji_nisu_null.ToString();

                    druga_nit = new Thread(delegate()
                    {
                        druga_kraj();
                    });
                    druga_nit.IsBackground = true;
                    druga_nit.Start();
                }));
            }

            return;
        }



        DateTime vrijeme_poc, vrijeme_kraj;

        private Thread izlistavanje;

        private Thread druga_nit;

        private void button4_Click(object sender, EventArgs e)
        {
            label56.Text = "";
            label57.Text = "";
     
            vrijeme_poc = DateTime.Now;

            listView1.Items.Clear();

            izlistavanje = new Thread(delegate()
            {
                brojacDatoteka();
            });

            izlistavanje.SetApartmentState(ApartmentState.STA);

            izlistavanje.IsBackground = true;
            izlistavanje.Start();
     

        }

        private void button11_Click(object sender, EventArgs e)
        {
            izlistavanje.Abort();

            this.Invoke(new Action(() =>
            {
                druga_nit = new Thread(delegate()
                {
                    druga_kraj();
                });
                druga_nit.IsBackground = true;
                druga_nit.Start();
            }));
        }

        public void druga_kraj()
        {
            vrijeme_kraj = DateTime.Now;

            TimeSpan izmedju = vrijeme_kraj - vrijeme_poc;

            this.Invoke(new Action(() =>
            {
                label57.Text = izmedju.TotalSeconds.ToString() + " sekundi";
            }));
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        int tajmer = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (tajmer == imageList1.Images.Count)
            {
                pictureBox1.Image = imageList1.Images[tajmer-1];
                tajmer++;
            }
            if (tajmer > imageList1.Images.Count && tajmer < imageList1.Images.Count + 10)
            {
                tajmer++;
                return;
            }
            if (tajmer == imageList1.Images.Count + 10) tajmer = 0;

            pictureBox1.Image = imageList1.Images[tajmer];

            tajmer++;
        }

        private void groupBox14_Enter(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            Form6 nova = new Form6();
            nova.Show();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            muzika = new Thread(delegate()
            {
                dajMuziku(2);
            });

            muzika.SetApartmentState(ApartmentState.STA);

            muzika.IsBackground = true;
            muzika.Start();
        }

        List<string> listaVremena = new List<string>();
        List<int> listaMax = new List<int>();
        List<int> listaMin = new List<int>();
        private Thread vrijeme;

        private void button17_Click(object sender, EventArgs e) //učitaj
        {
            vrijeme = new Thread(delegate()
            {
                dajVrijeme();
            });

            vrijeme.SetApartmentState(ApartmentState.STA);

            vrijeme.IsBackground = true;
            vrijeme.Start();

        }

        private String dajKod(String url)
        {
            try
            {
                WebClient client = new WebClient();
                Byte[] source = client.DownloadData(url);
                String s = System.Text.Encoding.Default.GetString(source);
                return s;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message); return e.Message;
            }
        }

        private void dajVrijeme()
        {
            String dokxml = dajKod("http://rss.theweathernetwork.com/weather/bkxx0004");

            string prviDatum = string.Empty;
            string drugiDatum = string.Empty;
            string treciDatum = string.Empty;
            string prviVrijeme = string.Empty;
            string drugiVrijeme = string.Empty;
            string treciVrijeme = string.Empty;
            int brojacdana = 1;
            bool jeLiProsaoDanas = false;

            try
            {
                for (int i = 0; i < dokxml.Count(); i++)
                {
                    if (dokxml.Substring(i, 7) == "pubDate") jeLiProsaoDanas = true;

                    if (jeLiProsaoDanas == true && dokxml.Substring(i, 6) == "<title")
                    {
                        for (int j = i + 7; ; j++)
                        {
                            if (dokxml[j] == '<') break;
                            if (brojacdana == 1) prviDatum += dokxml[j];
                            if (brojacdana == 2) drugiDatum += dokxml[j];
                            if (brojacdana == 3) treciDatum += dokxml[j];
                        }

                        brojacdana++;
                    }

                    if (brojacdana > 1 && dokxml.Substring(i, 12) == "<description")
                    {
                        brojacdana--;

                        for (int j = i + 13; ; j++)
                        {
                            if (dokxml[j] == '<') break;
                            if (brojacdana == 1) prviVrijeme += dokxml[j];
                            if (brojacdana == 2) drugiVrijeme += dokxml[j];
                            if (brojacdana == 3) treciVrijeme += dokxml[j];
                        }

                        brojacdana++;
                    }


                    if (dokxml.Substring(i, 10) == "</channel>") break;
                }
            }
            catch (Exception e)
            {
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show("Dogodila se greška");
                }));
            }


            pomocnaVrijeme(prviVrijeme, 0);
            pomocnaVrijeme(drugiVrijeme, 1);
            pomocnaVrijeme(treciVrijeme, 2);


            this.Invoke(new Action(() =>
            {
                groupBox15.Text = prviDatum;
                groupBox17.Text = drugiDatum;
                groupBox18.Text = treciDatum;

                textBox14.Text = listaVremena[0];
                textBox15.Text = listaMax[0].ToString() + "°C";
                textBox17.Text = listaMin[0].ToString() + "°C";

                textBox20.Text = listaVremena[1];
                textBox19.Text = listaMax[1].ToString() + "°C";
                textBox18.Text = listaMin[1].ToString() + "°C";

                textBox23.Text = listaVremena[2];
                textBox22.Text = listaMax[2].ToString() + "°C";
                textBox21.Text = listaMin[2].ToString() + "°C";
            }));
        }


        void pomocnaVrijeme(string prog, int broj)
        {
            if (broj == 0)
            {
                listaVremena.Add(string.Empty);
                listaVremena.Add(string.Empty);
                listaVremena.Add(string.Empty);
                listaMin.Add(0);
                listaMin.Add(0);
                listaMin.Add(0);
                listaMax.Add(0);
                listaMax.Add(0);
                listaMax.Add(0);
            }

            bool postavio = false, za_break = false;
            string najvisa = string.Empty;
            string najniza = string.Empty;


            for (int i = 0; i < prog.Count(); i++)
            {
                char c = prog[i];
                if (((c < 'Z' && c > 'A') || (c < 'z' && c > 'a')) && postavio == false)
                {
                    for (int j = i; ; j++)
                    {
                        c = prog[j];
                        if (c == ',')
                        {
                            postavio = true;
                            break;
                        }

                        listaVremena[broj] += c;
                    }
                }

                if (prog.Substring(i, 4) == "High")
                {
                    for (int j = i + 5; ; j++)
                    {
                        c = prog[j];
                        if (c == '&')
                        {
                            break;
                        }

                        najvisa += c;
                    }
                }

                if (prog.Substring(i, 3) == "Low")
                {
                    for (int j = i + 4; ; j++)
                    {
                        c = prog[j];
                        if (c == '&')
                        {
                            break;
                        }

                        najniza += c;
                        za_break = true;
                    }
                }

                if (za_break == true) break;
            }

            int broj_max, broj_min;
            Int32.TryParse(najvisa, out broj_max);
            Int32.TryParse(najniza, out broj_min);
            listaMax[broj] = broj_max;
            listaMin[broj] = broj_min;

            if (listaVremena[broj] == "Mainly sunny") listaVremena[broj] = "Pretežno sunčano";
            else if (listaVremena[broj] == "Rainy") listaVremena[broj] = "Kišovito";
            else if (listaVremena[broj] == "Variable cloudiness") listaVremena[broj] = "Promjenjivo oblačno";
            else if (listaVremena[broj] == "Fog") listaVremena[broj] = "Maglovito";
            else listaVremena[broj] = "Nedefinisano";
        }




        private Thread muzika;



        void dajMuziku(int broj)
        {
            int tv = new int();

            if (broj == 1)
            {
                for (int i = 0; i < 4; i++)
                    if (textBox14.Text == tipovi_vremena[i]) tv = i;
            }

            if (broj == 2)
            {
                for (int i = 0; i < 4; i++)
                    if (textBox20.Text == tipovi_vremena[i]) tv = i;
            }

            if (broj == 3)
            {
                for (int i = 0; i < 4; i++)
                    if (textBox18.Text == tipovi_vremena[i]) tv = i;
            }


            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            player.SoundLocation = path_za_pjesme[tv];
            player.Play();


        }

        private void button19_Click(object sender, EventArgs e)
        {
            muzika = new Thread(delegate()
            {
                dajMuziku(1);
            });

            muzika.SetApartmentState(ApartmentState.STA);

            muzika.IsBackground = true;
            muzika.Start();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            muzika = new Thread(delegate()
            {
                dajMuziku(3);
            });

            muzika.SetApartmentState(ApartmentState.STA);

            muzika.IsBackground = true;
            muzika.Start();
        }




       

       

        


        }

  
    }
