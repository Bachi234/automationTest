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
using System.Text;
using OfficeOpenXml;

namespace automationTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ElasticDataService _tblElasticData;
        private readonly ILogger _logger;

        public HomeController(ElasticDataService elasticDataServices, ILogger<HomeController> logger)
        {
            _tblElasticData = elasticDataServices;
            _logger = logger;
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
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

            if (pageSize == -1) // Display all data
            {
                elasticData = elasticData.ToList();
                ViewBag.PageSize = totalItems; // Set ViewBag.PageSize to the total number of items
            }
            else
            {
                elasticData = elasticData.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }

            return View(elasticData);
        }
        public IActionResult ExportAll(string searchSubject)
        {
            try
            {
                List<tblElasticData> allData = _tblElasticData.GetElasticDataBySubject(searchSubject);
                var sb = new StringWriter();

                sb.WriteLine("ID\tTo\tFrom\tEvent Type\tEvent Date\tChannel\tSubject"); // Headers

                foreach (var item in allData)
                {
                    sb.WriteLine($"{item.Id}\t{item.To}\t{item.From}\t{item.EventType}\t{item.EventDate}\t{item.Channel}\t{item.MessageCategory}");
                }

                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
                var stream = new MemoryStream(bytes);
                return File(stream, "text/tab-separated-values", "exported_data_all.tsv");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occured while exporting the data.");
                return BadRequest("An error occurred while exporting the data.");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
