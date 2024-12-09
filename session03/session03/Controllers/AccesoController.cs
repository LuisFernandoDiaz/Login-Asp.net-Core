using Microsoft.AspNetCore.Mvc;
using session03.Models;
using session03.ViewModels;
using session03.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace session03.Controllers
{
    public class AccesoController : Controller
    {

        private readonly session03Context _appBDContext;

        public AccesoController(session03Context appBDContext)
        {
            _appBDContext = appBDContext;
        }


        [HttpGet]
        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrarse(UsuarioVM modelo)
        {
            //aqui un ensaje de clave distinto
            if (modelo.Clave != modelo.ConfirmarClave)
            {
                ViewData["mensaje"] = "las contraseñas son distintas";
                return View();
            }
            //aqui el guardado de usuario
            Usuarios usuario = new Usuarios()
            {
                NombreCompleto = modelo.NombreCompleto,
                Correo = modelo.Correo,
                Clave = modelo.Clave
            };

            await _appBDContext.Usuarios.AddAsync(usuario);
            await _appBDContext.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM modelo)
        {
            Usuarios? usuarios_encontrado = await _appBDContext.Usuarios.Where(u => u.Correo == modelo.Correo && u.Clave == modelo.Clave).FirstOrDefaultAsync();
            if(usuarios_encontrado == null)
            {
                ViewData["mensaje"] = "No existe Usuario o contraseña";
                return View();

            }

            return RedirectToAction("Index", "Ventas");
        }



    }
}
