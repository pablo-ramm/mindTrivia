using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MindTrivia.Models
{
    public class UsuarioModel
    {
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Passw { get; set; }
        public string Email { get; set; }
        public string Pais { get; set; }
        public int Edad { get; set; }


    }
}
