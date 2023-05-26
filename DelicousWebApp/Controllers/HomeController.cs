using DelicousWebApp.DAL;
using DelicousWebApp.Models;
using DelicousWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DelicousWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly DelicousDbContext _context;

        public HomeController(DelicousDbContext delicousDbContext)
        {
            _context = delicousDbContext;
        }

        public IActionResult Index()
        {
            List<Chef> chefs = _context.Chefs.Include(c=>c.Job).Take(3).ToList();   
            HomeVM homeVM = new HomeVM()
            {
                Chefs = chefs,
            };
            return View(homeVM);
        }

    }
}