using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace MindTrivia.Controllers
{
    public class BaseController : Controller
    {
        public string usuario = "";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Read RoleId from cookie or session;
            usuario = HttpContext.Session.GetString("usuario");
            ViewBag.Test = "";//set cooki value
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewBag.Test = usuario;
            //Set RoleId to cookie or session
        }
    }
}
