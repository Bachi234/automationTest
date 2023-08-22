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
        public IActionResult Index(string searchSubject)
        {
            if (!string.IsNullOrEmpty(searchSubject))
            {
                var elasticData = _tblElasticData.GetElasticDataBySubject(searchSubject);
                return View(elasticData);
            }

            return View(new List<tblElasticData>());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
