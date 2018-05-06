using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klinika123
{
    interface INaplati
    {
         double Dug { get; set; }
         void ObracunajNoviPregled();
         double Racun();
    }
}
