using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using fsjr_challenge.Models;
using fsjr_challenge.Entities;

namespace fsjr_challenge.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            var GetData = context.UserData.ToList();
            var model = new HomeViewModel() { Users = GetData };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Send(HomeViewModel model)
        {
            var user = new UserData()
            {
                FirstName = model.UserData.FirstName,
                LastName = model.UserData.LastName,
                Participation = model.UserData.Participation
            };
            context.Add(user);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult GetData()
        {
            var GetData = context.UserData.ToList();
            var random = new Random();
            var colors = new List<string>();
            for (int i = 0; i < GetData.Count; i++)
            {
                colors.Add(String.Format("#{0:X6}", random.Next(0x1000000)));
            }

            return Json(new { Status = true, Users = GetData, colors = colors });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
