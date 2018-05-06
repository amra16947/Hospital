using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Klinika123;

namespace WindowsFormsApplication2
{
    public partial class Form2 : Form
    {
        string naziv_xml;
        BindingList<Pacijent> lista_pacijenata = new BindingList<Pacijent>();

        public Form2(string naziv)
        {
            InitializeComponent();
            naziv_xml = naziv;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                        
                        XmlReader reader = XmlReader.Create(naziv_xml);
                        XmlSerializer xs = new XmlSerializer(typeof(List<Pacijent>));
                        List<Pacijent> tmp = xs.Deserialize(reader) as List<Pacijent>;
                        if (tmp != null)
                        {
                            ListViewItem itm;
                            lista_pacijenata = new BindingList<Pacijent>(tmp);
                            listView1.View = View.Details;
                            listView1.GridLines = true;
                            listView1.FullRowSelect = true;
                            listView1.Columns.Add("ID", 50);
                            listView1.Columns.Add("Ime", 100);
                            listView1.Columns.Add("Prezime", 100);
                            listView1.Columns.Add("Telefon", 100);
                            listView1.Columns.Add("Adresa", 100);
                            listView1.Columns.Add("Datum rođenja", 100);
                            listView1.Columns.Add("Popust", 100);
                            listView1.Columns.Add("Dug", 100);

                            foreach (Pacijent p in lista_pacijenata)
                            {

                                itm = new ListViewItem(p.ID);
                                itm.SubItems.Add(p.Ime);
                                itm.SubItems.Add(p.Prezime);
                                itm.SubItems.Add(p.Telefon);
                                itm.SubItems.Add(p.Adresa);
                                itm.SubItems.Add(p.DatRodjenja);
                                itm.SubItems.Add(p.Popust.ToString());
                                itm.SubItems.Add(p.Dug.ToString());
                                listView1.Items.Add(itm);
                            }
                       }
                 }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }
    }
}