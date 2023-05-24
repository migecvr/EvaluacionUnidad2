﻿using LecturasModel.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecturasModel.DAL
{
    public class LecturasDALArchivos : ILecturasDAL
    {
        //implementar Singleton:
        //1. Contructor tiene que ser private
        private LecturasDALArchivos() { }

        //2. Debe poseer un atributo del mismo tipo de la clase y estatico
        private static LecturasDALArchivos instancia;
        //3. tener un metodo getIntance, que de vuelva una referencia al atributo
        public static ILecturasDAL GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new LecturasDALArchivos();
            }
            return instancia;
        }
        //como vamos a hacer para que 2 hebras no accedan a la vez a este archivo?????


        private static string url = Directory.GetCurrentDirectory();
        private static string archivo = url + "/lecturas.txt";

        public void AgregarLectura(Lectura lectura)
        {
            try
            {
                using (StreamWriter write = new StreamWriter(archivo, true))
                {
                    write.WriteLine(lectura.NroMedidor + ";" + lectura.Fecha + ";" + lectura.ValorConsumo);
                    write.Flush();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public List<Lectura> ObtenerLecturas()
        {
            List<Lectura> lista = new List<Lectura>();
            try
            {
                using (StreamReader read = new StreamReader(archivo))
                {
                    string texto = "";
                    do
                    {
                        texto = read.ReadLine();
                        if (texto != null)
                        {
                            string[] arr = texto.Trim().Split(';');
                            Lectura lectura = new Lectura()
                            {
                                NroMedidor = Convert.ToInt32(arr[0]),
                                Fecha = arr[1],
                                ValorConsumo = Convert.ToDouble(arr[2])
                            };
                            lista.Add(lectura);
                        }

                    } while (texto != null);
                }

            }
            catch (Exception ex)
            {
                lista = null;
            }
            return lista;
        }
    }
}
