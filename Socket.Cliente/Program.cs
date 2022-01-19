﻿using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Linq;
using SocketComun;
using System.Threading.Tasks;
using System.IO;

namespace Calculator.Cliente
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DatosOperacion datos = new DatosOperacion(1, 1, TipoOperacion.Suma);
            Console.WriteLine("Console C#\r");
            Console.WriteLine("------------------------\n");

            while (true)
            {
                DatosOperacion op = new DatosOperacion();

                Console.Write("Dime el primer número para operar: ");

                double operando1 = Convert.ToDouble(Console.ReadLine());

                Console.Write("Dime el segundo número para operar: ");

                double operando2 = Convert.ToDouble(Console.ReadLine());

                string operacion = "";

                while (operacion != "Suma" && operacion != "Multiplicacion" && operacion != "Resta" && operacion != "Division")
                {

                    Console.Write("Operación a realizar (Suma, Resta, Multiplicacion, Division): ");
                    operacion = Console.ReadLine();

                    if (operacion != "Suma" && operacion != "Multiplicacion" && operacion != "Resta" && operacion != "Division")
                    {
                        Console.WriteLine("Operación no válida");
                    }
                    else
                    {


                        switch (operacion)
                        {
                            case "Suma":
                                op = new DatosOperacion(operando1, operando2, TipoOperacion.Suma);
                                break;
                            case "Resta":
                                op = new DatosOperacion(operando1, operando2, TipoOperacion.Resta);
                                break;
                            case "Multiplicacion":
                                op = new DatosOperacion(operando1, operando2, TipoOperacion.Resta);
                                break;
                            case "Division":
                                op = new DatosOperacion(operando1, operando2, TipoOperacion.Resta);
                                break;

                        }
                    }

                }

                string mensaje = Metodos.Serialize(op);
                var resultado = EnviaMenaje(mensaje);

                Console.WriteLine(resultado);
                Console.WriteLine();
            }

            Console.Write("Press any key to close the Calculator console app...");
            Console.ReadKey();
        }

        static string EnviaMenaje(string mensaje)
        {
            try
            {
                // Connect to a Remote server
                // Get Host IP Address that is used to establish a connection
                // In this case, we get one IP address of localhost that is IP : 127.0.0.1
                // If a host has multiple addresses, you will get a list of addresses

                IPHostEntry host = Dns.GetHostEntry("localhost");
                //IPHostEntry host = Dns.GetHostEntry("infc13_profe");
                IPAddress ipAddress = host.AddressList[0];

                //IPAddress ipAddress = IPAddress.Parse("ip destino");

                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 2800);

                // Create a TCP/IP  socket.
                using Socket sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    // Connect to Remote EndPoint
                    sender.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}",
                        sender.RemoteEndPoint.ToString());

                    Console.WriteLine("Socket redad for {0}",
                        sender.LocalEndPoint.ToString());
                    DatosOperacion datos = new DatosOperacion(1, 1, TipoOperacion.Suma);
                    var cacheEnvio = Encoding.UTF8.GetBytes(mensaje);

                    // Send the data through the socket.
                    int bytesSend = sender.Send(cacheEnvio);

                    // Receive the response from the remote device.
                    byte[] bufferRec = new byte[1024];
                    int bytesRec1 = sender.Receive(bufferRec);

                    var resultado = Encoding.UTF8.GetString(bufferRec, 0, bytesRec1);

                    // Release the socket.
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                    return resultado;

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return null;
        }


    }
}
