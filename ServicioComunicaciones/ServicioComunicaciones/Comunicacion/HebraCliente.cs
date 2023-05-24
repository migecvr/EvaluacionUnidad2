using LecturasModel.DAL;
using LecturasModel.DTO;
using ServidorSocketUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioComunicaciones.Comunicacion
{
    public class HebraCliente
    {
        private ClienteCom clienteCom;
        private ILecturasDAL lecturasDAL = LecturasDALArchivos.GetInstancia();

        public HebraCliente(ClienteCom clienteCom)
        {
            this.clienteCom = clienteCom;
        }

        public void ejecutar()
        {
            
            int nroMedidor;
            string fecha;
            double valorConsumo;

            bool valido;

            do
            {
                clienteCom.Escribir("Ingrese Numero de Medidor: ");
                valido = Int32.TryParse(clienteCom.Leer(), out nroMedidor);
            } while (!valido);

            do
            {
                clienteCom.Escribir("Ingrese Fecha: ");
                fecha = clienteCom.Leer();
                
            } while (fecha.Equals(string.Empty));

            do
            {
                clienteCom.Escribir("Ingrese valor de consumo (Kw/h): ");
                valido = Double.TryParse(clienteCom.Leer(), out valorConsumo);
            } while (!valido);

            clienteCom.Escribir("chao");

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

            clienteCom.Desconectar();
        }
    }
}
