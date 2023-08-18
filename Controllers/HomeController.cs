using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using automationTest.Models;
using Microsoft.EntityFrameworkCore;
using automationTest.Context;
using automationTest.ViewModel;

namespace automationTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly tblEvent _events;
        public HomeController(ApplicationDbContext context, tblEvent events)
        {
            _context = context;
            _events = events;
        }
       
        public IActionResult Index()//will be considered as the Search Function 
        {
            var viewModel = new TableViewModel();
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Index(string searchId)
        {
            var filteredEvents = _events.tblEvent.Where(data => data.Mail_Number.Contains(searchId)).ToList();
            var viewModel = new TableViewModel
            {
                tblEvent = filteredEvents
            };
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
