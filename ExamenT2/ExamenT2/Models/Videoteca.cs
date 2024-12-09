using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using ExamenT2.Controllers;
using System.ComponentModel.DataAnnotations;

namespace ExamenT2.Models
{
    public class Videoteca
    {

        [Key]
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? tipo { get; set; }
        public int veces_vista { get; set; }
        public decimal rating { get; set; }
        public DateTime? fecha { get; set; }


    }
}
