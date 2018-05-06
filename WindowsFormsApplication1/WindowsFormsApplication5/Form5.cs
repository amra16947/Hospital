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

namespace WindowsFormsApplication5
{
    public partial class Form5 : Form
    {
        BindingList<Izuzetak> lista = new BindingList<Izuzetak>();
        public Form5(BindingList<Izuzetak> nova)
        {
            InitializeComponent();
            lista = nova;
            listBox1.DataSource = lista;
            label2.Text = lista.Count().ToString();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }
    }
}
