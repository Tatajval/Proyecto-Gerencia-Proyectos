using BackEnd.Datos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Controllers
{
    public class UnidadesPorEstadoController : Controller
    {
        private readonly RutasContext _context;

        public UnidadesPorEstadoController()
        {
            _context = new RutasContext();
        }

        // GET: UnidadesPorEstadoController
        public IActionResult Index()
        {
            return View(_context.VwUnidadesPorEstado.AsAsyncEnumerable());
        }
    }
}
