using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Klinika123
{
    public class OsobaException: Exception
    {
        public OsobaException()
            : base()
        {
        }

        public OsobaException(string poruka): base(poruka)
        {
            MessageBox.Show( poruka,"Greška");
            
        }


    }
}
