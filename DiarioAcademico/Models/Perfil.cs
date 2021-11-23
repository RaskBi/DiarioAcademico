using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiarioAcademico.Models
{
    class Perfil
    {
        [PrimaryKey, AutoIncrement]
        public Guid perfilId { get; set; }
        [MaxLength (40)]
        public string per_nickName { get; set; }
        [MaxLength(40)]
        public string per_nombre { get; set; }
        [MaxLength(40)]
        public string per_apellido { get; set; }
        [MaxLength(3)]
        public int per_edad { get; set; }
        [MaxLength(40)]
        public string per_institucion { get; set; }

    }
}
