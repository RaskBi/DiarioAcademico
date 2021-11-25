using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiarioAcademico.Models
{
    public class Perfil
    {
        public int idPerfil { get; set; }
        public string per_nickname { get; set; }
        public string per_nombre { get; set; }
        public string per_apellido { get; set; }
        public int per_edad { get; set; }
        public string per_institucion { get; set; }
        public int Registro_idRegistro { get; set; }

    }
}
