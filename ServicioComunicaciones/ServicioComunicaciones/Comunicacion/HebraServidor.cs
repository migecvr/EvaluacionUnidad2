using LecturasModel.DAL;
using ServidorSocketUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServicioComunicaciones.Comunicacion
{
    public class HebraServidor
    {
        private ILecturasDAL lecturasDAL = LecturasDALArchivos.GetInstancia();

        private int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);

        public void cambiaPuerto() 
        {
            Console.WriteLine("S: Servidor se inicia por defecto en puerto {0}", puerto);

            bool valido = false;
            do
            {

                Console.WriteLine("¿Quieres cambiar el numero de puerto? s/n");
                string datoIngresado = Console.ReadLine().Trim();
                if (datoIngresado.ToLower() == "s" || datoIngresado.ToLower() == "n")
                {
                    if (datoIngresado.ToLower() == "s")
                    {
                        do
                        {
                            Console.WriteLine("Puerto:");
                            valido = Int32.TryParse(Console.ReadLine().Trim(), out this.puerto);
                            if (valido && (this.puerto >= 0 && this.puerto <= 65535))
                            {
                                valido = true;
                            }
                            else
                            {
                                Console.WriteLine("El numero de puerto ingresado no es valido");
                                valido = false;
                            }
                        } while (!valido);
                    }
                    else { valido = true; }
                }

            } while (!valido);
        }
        public void Ejecutar()
        {
            
            ServerSocket servidor = new ServerSocket(puerto);
            Console.WriteLine("S: Servidor iniciado en el puerto {0}", puerto);

            
            

            if (servidor.Iniciar())
            {
                while (true)
                {
                    //Console.WriteLine("");
                    Console.WriteLine("S: Esperando cliente....");
                    Socket cliente = servidor.ObtenerCliente();
                    Console.WriteLine("S: Cliente recibido");
                    ClienteCom clienteCom = new ClienteCom(cliente);

                    HebraCliente clienteThread = new HebraCliente(clienteCom);
                    Thread t = new Thread(new ThreadStart(clienteThread.ejecutar));
                    t.IsBackground = true;
                    t.Start();
                }
            }
            else
            {
                Console.WriteLine("FALLO¡¡¡, se puede iniciar server en {0}", puerto);
            }
        }
    }
}
