using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using session03.Controllers;
using System.ComponentModel.DataAnnotations;

namespace session03.Models
{
    public class Usuarios
    {
        [Key]
        public int IdUsuario { get; set;}
        public string NombreCompleto { get; set;}
        public string Correo { get; set;}
        public string Clave {get; set;}
        }
}
