using FirstWebAppInDocker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using MySqlConnector;

namespace FirstWebAppInDocker.Controllers
{
    public class HomeController : Controller
    {
        private readonly string? _connectionString;

        // Dependency Injection (send dependency to constructor)
        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Check if the connection to MariaDB is successful
        public async Task<IActionResult> Index()
        {
            string viewModel1 = "Connected to MariaDB successfully!";
            string viewModel2 = "Failed to connect to MariaDB";
            try
            {
                // Put MySqlConnection in a method to make it testable (make the code loosely coupled)
                await using var connection = new MySqlConnection(_connectionString);
                await connection.OpenAsync();

                return View("Index", viewModel1);
            }
            catch (Exception ex)
            {
                return View("Index", viewModel2 + " " + ex.Message);
            }
            
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Privacy()
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
