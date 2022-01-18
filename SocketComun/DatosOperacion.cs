using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketComun
{
    public class DatosOperacion
    {
        public DatosOperacion()
        {
        }

        public DatosOperacion(double operando1, double operando2, TipoOperacion operacion)
        {
            this.operando1 = operando1;
            this.operando2 = operando2;
            this.operacion = operacion;
        }

        public double operando1 { get; set; }
        public double operando2 { get; set; }
        public TipoOperacion operacion { get; set; }
    }

}
