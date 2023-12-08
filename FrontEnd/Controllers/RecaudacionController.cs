using BackEnd.Datos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Controllers
{
    public class RecaudacionController : Controller
    {
        private readonly RutasContext _context;

        public RecaudacionController()
        {
            _context = new RutasContext();
        }

        // GET: Recaudacion
        public IActionResult Index()
        {
            return View(_context.VwRecaudacion.AsAsyncEnumerable());
        }
    }
}
