﻿using SocketComun;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace Calculator.Servidor
{
    class Program
    {
        static void Main(string[] args)
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];

            //IPAddress ipAddress = IPAddress.Parse("ip escucha");

            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 2800);

            try
            {
                // Create a Socket that will use Tcp protocol
                using Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                // A Socket must be associated with an endpoint using the Bind method
                listener.Bind(localEndPoint);
                // Specify how many requests a Socket can listen before it gives Server busy response.
                // We will listen 10 requests at a time
                listener.Listen(10);

                Console.WriteLine("Waiting for a connection ..." + listener.LocalEndPoint.ToString());

                while (true)
                {
                    Socket handler = listener.Accept();

                    Console.WriteLine("Socket connected to {0}",
                        handler.RemoteEndPoint.ToString());

                    var cacheMenaje = new byte[4096];
                    int bytesMenaje = handler.Receive(cacheMenaje);

                    if (bytesMenaje > 0)
                    {
                        double resOp;
                        var mensaje = Encoding.UTF8.GetString(cacheMenaje, 0, bytesMenaje);
                        DatosOperacion obj = JsonSerializer.Deserialize<DatosOperacion>(mensaje);
                        Resultado res = null;
                        if (obj.operacion == TipoOperacion.Suma)
                        {
                            resOp = obj.operando1 + obj.operando2;

                        }
                        else if (obj.operacion == TipoOperacion.Resta)
                        {
                            resOp = obj.operando1 - obj.operando2;

                        }
                        else if (obj.operacion == TipoOperacion.Multiplicacion)
                        {
                            resOp = obj.operando1 * obj.operando2;

                        }
                        else
                        {
                            resOp = obj.operando1 / obj.operando2;
                        }
                        res = new Resultado(obj.operando1, obj.operando2, resOp);

                        string r = JsonSerializer.Serialize(res);
                        var respuesta = r;


                        var cacheRespuesta = Encoding.UTF8.GetBytes(respuesta);
                        handler.Send(cacheRespuesta);
                        Thread.Sleep(0);
                    }

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
        }

    }

}
