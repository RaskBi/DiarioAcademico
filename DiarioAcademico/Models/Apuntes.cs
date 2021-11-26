using System;
using System.Collections.Generic;
using System.Text;

namespace DiarioAcademico.Models
{
    public class Apuntes
    {
        public int idApuntes { get; set; }
        public string apu_nombre { get; set; }
        public string apu_descripcion { get; set; }
        public int Materias_idMaterias { get; set; }
    }
}
