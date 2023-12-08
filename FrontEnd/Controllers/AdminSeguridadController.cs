using FrontEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Controllers
{
    public class AdminSeguridadController : Controller
    {
        private readonly ILogger<AdminSeguridadController> _logger;

        public AdminSeguridadController(ILogger<AdminSeguridadController> logger)
        {
            _logger = logger;
        }

        //[Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
