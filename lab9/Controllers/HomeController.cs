using lab9.Models;
using lab9.ViewModels.HomeViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;

namespace lab9.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<Order> _orders = new();
        private static Coord _coord = new();
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
    }
}