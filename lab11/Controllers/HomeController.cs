using lab_11.Filters;
using lab_11.Models;
using lab_11.ViewModels.HomeViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;

namespace lab_11.Controllers
{
    [LogAction]
    [UserCount]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static readonly List<Order> _orders = new();
        private static Coord _coord = new();
        private readonly string logFilePath = "actionLog.txt";
        private readonly string userCountFilePath = "userCount.txt";
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult ViewLogs()
        {
            try
            {
                var logs = System.IO.File.ReadAllLines(logFilePath);
                return View(logs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to read logs file.");
                return View(Array.Empty<string>());
            }
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SetCoord(string lat, string lon)
        {
            if (Double.TryParse(lat, NumberStyles.Float, CultureInfo.InvariantCulture, out double numberLat)
                && Double.TryParse(lon, NumberStyles.Float, CultureInfo.InvariantCulture, out double numberLon))
            {
                Console.WriteLine(numberLat);
                Console.WriteLine(numberLon);
                _coord.lat = numberLat;
                _coord.lon = numberLon;
            }
            else
            {
                throw new Exception();
            }
            return RedirectToAction("GetWeather");
        }
        [HttpGet]
        public ActionResult GetWeather()
        {
            Console.WriteLine(_coord);
            return View(_coord);
        }
        [HttpGet]
        public ActionResult AddOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostOrder(Order order, int amount)
        {
            _orders.Add(order);
            for (int i = 1; i < amount; i++)
            {
                Order newOrder = new();
                newOrder.OrderPrice = order.OrderPrice;
                newOrder.OrderName = order.OrderName;
                _orders.Add(newOrder);
            }
            return View("AddOrder");
        }
        public ActionResult ShowOrders(ShowStyles showStyle)
        {
            Console.WriteLine(showStyle);
            ShowOrdersViewModel showOrdesrViewModel = new(_orders, showStyle);
            return View(showOrdesrViewModel);
        }
        public ActionResult DeleteOrder(int Id, ShowStyles showStyle)
        {
            Console.WriteLine(showStyle);
            _orders.RemoveAll(x => x.OrderId == Id);
            return RedirectToAction("ShowOrders", new { showStyle });
        }
        public void LogAction(string action)
        {
            string logMessage = $"{action} - {DateTime.Now}";
            System.IO.File.AppendAllText("actionLog.txt", logMessage + Environment.NewLine);
        }

        [UserCount]
        public IActionResult UserStatistics()
        {
            return Content($"Total Unique Users: {GetUserCount()}");
        }

        private int GetUserCount()
        {
            var userCount = 0;
            if (System.IO.File.Exists(userCountFilePath))
            {
                userCount = int.Parse(System.IO.File.ReadAllText(userCountFilePath));
            }
            return userCount;
        }

        public void UpdateUserCount()
        {
            var userCount = GetUserCount();
            userCount++;
            System.IO.File.WriteAllText(userCountFilePath, userCount.ToString());
        }

    }
}