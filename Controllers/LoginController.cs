using Microsoft.AspNetCore.Mvc;
using MindTrivia.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MindTrivia.Models;
using Microsoft.AspNetCore.Http;

namespace MindTrivia.Controllers
{
    public class LoginController : BaseController
    {
        UsuarioDatos UsuarioDatos = new UsuarioDatos();
        public IActionResult Index()
        {
           

            return View();
        }


        public IActionResult Registrar()
        {
           

            return View();
        }

        public IActionResult Login(string param1)
        {
            Console.WriteLine(param1);
            if (param1 == "1")
            {
                ViewBag.JavaScriptFunction2 = string.Format("ShowError1();");
            }

            

            return View();
        }


    }
}
