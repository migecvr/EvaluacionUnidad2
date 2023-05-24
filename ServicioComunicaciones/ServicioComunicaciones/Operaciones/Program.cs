using LecturasModel.DAL;
using LecturasModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioComunicaciones
{
    public partial class Program
    {
        private static ILecturasDAL lecturasDAL = LecturasDALArchivos.GetInstancia();

        static void Ingresar()
        {
            int nroMedidor;
            string fecha;
            double valorConsumo;

            bool valido;



            do
            {
                Console.WriteLine("Ingrese Numero de Medidor: ");
                valido = Int32.TryParse(Console.ReadLine().Trim(), out nroMedidor);
            } while (!valido);

            do
            {
                Console.WriteLine("Ingrese Fecha: ");
                fecha = Console.ReadLine().Trim();

            } while (fecha.Equals(string.Empty));

            do
            {
                Console.WriteLine("Ingrese valor de consumo (Kw/h): ");
                valido = Double.TryParse(Console.ReadLine().Trim(), out valorConsumo);
            } while (!valido);

            Lectura lectura = new Lectura()
            {
                NroMedidor = nroMedidor,
                Fecha = fecha,
                ValorConsumo = valorConsumo
            };
            lock (lecturasDAL)
            {
                lecturasDAL.AgregarLectura(lectura);
            }


        }


        static void Mostrar()
        {
            List<Lectura> lecturas = null;
            lock (lecturasDAL)
            {
                lecturas = lecturasDAL.ObtenerLecturas();
            }
            bool indicaColor = true;
            foreach (Lectura lectura in lecturas)
            {
                if (indicaColor)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    indicaColor = false;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    indicaColor = true;
                }

                Console.WriteLine(lectura);


            }
            Console.ForegroundColor = ConsoleColor.White;

        }
    }
}
