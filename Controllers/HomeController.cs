using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using automationTest.Models;
using Microsoft.EntityFrameworkCore;
using automationTest.Context;
using automationTest.ViewModel;
using automationTest.Service;

namespace automationTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ElasticDataService _tblElasticData;

        public HomeController(ElasticDataService elasticDataServices)
        { 
            _tblElasticData = elasticDataServices;
        }

        public IActionResult Index(string searchSubject, int page = 1, int pageSize = 10)
        {
            var elasticData = _tblElasticData.GetElasticDataBySubject(searchSubject);

            int totalItems = elasticData.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            ViewBag.SearchSubject = searchSubject;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;

            elasticData = elasticData.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return View(elasticData);
        }

      


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
