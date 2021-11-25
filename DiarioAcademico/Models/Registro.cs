using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiarioAcademico.Models
{
    class Registro
    {
        [PrimaryKey, AutoIncrement]
        public int idRegistro { get; set; }
        [MaxLength(40)]
        public string log_email { get; set; }
        [MaxLength(40)]
        public string log_contrasenia { get; set; }
    }
}
