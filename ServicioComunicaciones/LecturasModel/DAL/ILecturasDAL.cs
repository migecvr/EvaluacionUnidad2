using LecturasModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecturasModel.DAL
{
    public interface ILecturasDAL
    {
        void AgregarLectura(Lectura lectura);
        List<Lectura> ObtenerLecturas();
    }
}
