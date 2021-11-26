using System;
using System.Collections.Generic;
using System.Text;

namespace DiarioAcademico.Models
{
    public class Tareas
    {
        public int idApunidTareastes { get; set; }
        public string tar_nombre { get; set; }
        public string tar_desc { get; set; }
        public DateTime tar_fecha_inicio { get; set; }
        public DateTime tar_fecha_fin { get; set; }
        public int Materias_idMaterias { get; set; }
    }
}
