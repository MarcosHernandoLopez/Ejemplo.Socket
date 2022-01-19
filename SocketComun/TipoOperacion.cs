using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SocketComun
{
   public enum TipoOperacion: int
    {
        Suma = 0,
        Resta = 1,
        Multiplicacion = 2,
        Division = 3
    }
}
