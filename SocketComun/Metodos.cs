using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocketComun
{
    public class Metodos
    {
        public static string Serialize<T>(T obj)
        {
            string jsonString = JsonSerializer.Serialize(obj);
            return jsonString;
        }
    }
}
