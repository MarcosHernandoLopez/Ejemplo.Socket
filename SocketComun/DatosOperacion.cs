using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketComun
{
    class DatosOperacion
    {
        double operando1 { get; set; }
        double operando2 { get; set; }
        TipoOperacion operacion { get; set; }
    }

    public enum TipoOperacion
    {
        suma,
        resta,
        multiplicacion,
        division
    }

}
