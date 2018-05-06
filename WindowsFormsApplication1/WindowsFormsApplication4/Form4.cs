using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WindowsFormsApplication4
{
    public partial class Form4 : Form
    {
        string naziv;
        public Form4(string NazivFoldera)
        {
            InitializeComponent();
            naziv = NazivFoldera;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
              try
                {
                    
                    XmlDocument dom = new XmlDocument();
                    dom.Load(naziv);

                    treeView1.Nodes.Clear();

                    treeView1.Nodes.Add(new TreeNode("Lista pacijenata"));
                    TreeNode Cvor = new TreeNode();
                    Cvor = treeView1.Nodes[0];

                  
                    dodajCvor(dom.DocumentElement, Cvor);
                    // treeView1.ExpandAll();
                }
                catch (XmlException xmlEx)
                {
                    MessageBox.Show(xmlEx.Message);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    this.Close();
                }
        
        }

        private void dodajCvor(XmlNode XmlCvor, TreeNode drvoCvor)
        {
            XmlNode XmlTekuciCvor;
            TreeNode drvoTekuciCvor;
            XmlNodeList XmlListCvorova;
            int i;
           
            if (XmlCvor.HasChildNodes)
            {
                
                XmlListCvorova = XmlCvor.ChildNodes;
                for (i = 0; i <= XmlListCvorova.Count - 1; i++)
                {
                    
                        XmlTekuciCvor = XmlCvor.ChildNodes[i];

                        if (XmlTekuciCvor.Name != "Zauzet")
                        drvoCvor.Nodes.Add(new TreeNode(XmlTekuciCvor.Name));
                        drvoTekuciCvor = drvoCvor.Nodes[i];

                        if (XmlTekuciCvor.Name!="Zauzet")
                        dodajCvor(XmlTekuciCvor, drvoTekuciCvor);  
                        
                }
            }
            else
            {
                if(string.IsNullOrEmpty(XmlCvor.InnerText.ToString())==false)
                drvoCvor.Text = XmlCvor.InnerText.ToString();
            }
        }

    }

    }

