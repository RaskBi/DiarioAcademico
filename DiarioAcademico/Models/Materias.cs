using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiarioAcademico.Models
{
    public class Materias
    {
        public int idMaterias { get; set; }
        public string mat_nombre { get; set; }
        public int Perfil_idPerfil { get; set; }
    }

}
