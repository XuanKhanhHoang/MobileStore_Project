﻿using Microsoft.AspNetCore.Mvc;
using MobileStore_Project.Models;
using System.Diagnostics;

namespace Project_BE_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Route("/LoginRegister")]
        public IActionResult LoginRegister()
        {
            return View();
        }
        [Route("/Cart")]
        public IActionResult Cart() {
            return View();
        }
        [Route("/ProductInformation")]
        public IActionResult ProductInformation()
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