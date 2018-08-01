using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DynamoDB.Repository.TestWebApp.DataAccess;
using Microsoft.AspNetCore.Mvc;
using DynamoDB.Repository.TestWebApp.Models;

namespace DynamoDB.Repository.TestWebApp.Controllers
{
    public class HomeController : Controller
    {
        private IMovieRepository MovieRepo { get; set; }
        public HomeController(IMovieRepository movieRepo)
        {
            MovieRepo = movieRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
