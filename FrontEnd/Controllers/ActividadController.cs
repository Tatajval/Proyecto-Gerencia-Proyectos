using BackEnd.Datos;
using BackEnd.Negocio;
using FrontEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Controllers
{
    public class ActividadController : Controller
    {
        private readonly IActividades actividades;
        private readonly ILogger<ActividadController> _logger;

        public ActividadController(ILogger<ActividadController> logger)
        {
            actividades = new Actividades();
            _logger = logger;
        }

        // GET: ActividadController
        // [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(actividades.VerListaCompleta());
        }

        // GET: ActividadController/Details/5
        // [Authorize(Roles = "admin")]
        public ActionResult Info(string id)
        {
            return View(actividades.VerListaCompleta().Find(x => x.ObjID == id));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
