using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MindTrivia.Models;
using MindTrivia.Datos;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography.X509Certificates;

namespace MindTrivia.Controllers
{
    public class MantenedorController : BaseController
    {
        UsuarioDatos UsuarioDatos = new UsuarioDatos();
        PreguntasDatos PreguntasDatos = new PreguntasDatos();



        public IActionResult Index()
        {

            var oLista = PreguntasDatos.Listar();
            return View(oLista);
        }

        public IActionResult Listar()
        {
            var oLista = UsuarioDatos.Listar();

            return View(oLista);
        }

        public IActionResult Guardar()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Guardar(UsuarioModel oUsuario)

        {
            if (!ModelState.IsValid)
                return View();

            var repetido = UsuarioDatos.UsuarioUnico(oUsuario);

            if (repetido > 0)
            {
                ViewBag.JavaScriptFunction = string.Format("ShowError();");
                return View();
            }
            else
            {
                Console.WriteLine("Usuario no Repetido");
                var resp = UsuarioDatos.Guardar(oUsuario);
                if (resp)
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    return View();
                }
            }

           
        }

        public IActionResult Editar(int IdUsuario)
        {

            return View();
        }
        [HttpPost]
        public IActionResult Editar(UsuarioModel oUsuario)
        {
            if (!ModelState.IsValid)
                return View();

            var respuesta = UsuarioDatos.Editar(oUsuario);

            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
                return View();
        }

        public IActionResult Eliminar(int IdUsuario)
        {
            var oUsuario = UsuarioDatos.Obtener(IdUsuario);
            return View(oUsuario);
        }
        [HttpPost]
        public IActionResult Eliminar(UsuarioModel oUsuario)
        {
            var respuesta = UsuarioDatos.Eliminar(oUsuario.IdUsuario);

            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
                return View();
        }

        [HttpPost]
        public IActionResult Validar(UsuarioModel oUsuario)
        {
            var bandera = UsuarioDatos.Validar(oUsuario.Usuario, oUsuario.Passw);
            if (bandera == 1)
            {
                var usuario = oUsuario.Usuario;
                HttpContext.Session.SetString("usuario", usuario);


                return RedirectToAction("Index", "Mantenedor");
            }
            else
            {


                return RedirectToAction("Login", "Login", new { param1 = "1" });

            }

        }

        public IActionResult GuardarPreguntas(PreguntasModel oPreguntas)
        {
            if (!ModelState.IsValid)
                return View();




            var resp = PreguntasDatos.Guardar(oPreguntas);
            if (resp)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            var resp = PreguntasDatos.Obtener(id);

            return View(resp);

        }
    }
}
