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
        private readonly TableViewModel _viewModel;
        private readonly ApplicationDbContext _context; // Added ApplicationDbContext dependency

        public HomeController(ApplicationDbContext context, TableViewModel viewModel)
        {
            _context = context;
            _viewModel = viewModel;
        }

        public IActionResult Index()
        {
            TableViewModel viewModel = new TableViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(string searchId)
        {
            
            var filteredEvents = _viewModel.tblEvents.Where(data => data.Mail_Number.Contains(searchId)).ToList();
            var viewModel = new TableViewModel
            {
                tblEvents = filteredEvents
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
