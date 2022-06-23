using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Ejercicio1_3.Models
{
    public class Personas
    {
        [PrimaryKey,AutoIncrement]
        public int id { get; set; }

        public string nombre { get; set; }

        public string Apellido { get; set; }

        public int edad { get; set; }

        public string correo { get; set; }

        public string direccion { get; set; }
    }
}
