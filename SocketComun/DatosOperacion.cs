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

        public double calcular(DatosOperacion obj)
        {
            double res = 0;
            int op = (int)obj.operacion;

            if (op == 0)
            {
                res = obj.operando1 + obj.operando2;
            }
            else if (op == 1)
            {
                res = obj.operando1 - obj.operando2;
            }
            else if (op == 2)
            {
                res = obj.operando1 * obj.operando2;
            }
            else if (op == 3)
            {
                res = obj.operando1 / obj.operando2;
            }

            return res;
        }
    }

}
