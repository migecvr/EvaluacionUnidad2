using LecturasModel.DAL;
using LecturasModel.DTO;
using ServicioComunicaciones.Comunicacion;
using ServidorSocketUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServicioComunicaciones
{
    public partial class Program
    {
        

        static bool Menu()
        {
            bool continuar = true;
            Console.WriteLine("");
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("Selecciones una opcion");
            Console.WriteLine(" 1. Ingresar \n 2. Mostrar \n 0.Salir");
            switch (Console.ReadLine().Trim())
            {
                case "1":
                    Ingresar();
                    break;
                case "2":
                    Mostrar();
                    break;
                case "0":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Ingrese de Nuevo");
                    break;
            }
            return continuar;
        }

        
        static void Main(string[] args)
        {
            
            HebraServidor hebra = new HebraServidor();
            hebra.cambiaPuerto();
            Thread t = new Thread(new ThreadStart(hebra.Ejecutar));
            t.IsBackground = true;
            t.Start();
            
            while (Menu()) ;
        }

        

        
    }
}
