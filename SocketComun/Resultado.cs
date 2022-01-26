using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketComun
{
    public class Resultado
    {
        public Resultado()
        { }

        public Resultado(double operando1, double operando2, double valor)
        {
            this.operando1 = operando1;
            this.operando2 = operando2;
            this.valor = valor;
        }

        public double operando1 { get; set; }
        public double operando2 { get; set; }
        public double valor { get; set; }

    }
}
