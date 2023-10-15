using lab_6.Models;
using lab_6.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace lab_6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static int _userId = 1;
        private static int _productId = 1;
        private static readonly List<User> _users = new List<User>();
        private static readonly List<Product> _products = new List<Product>();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Console.WriteLine(_products[0]);
            return View();
        }
        public IActionResult RegForm()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserV user)
        {
            if (ModelState.IsValid && int.TryParse(user.Age, out int age))
            {
                User newUser = new User(_userId, user.Name, user.Passw, age);
                _users.Add(newUser);
                Console.WriteLine(_userId);
                Console.WriteLine(_users.Count);
                Response.Cookies.Append("authId", _userId.ToString(), new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddMinutes(30),
                    IsEssential = true,
                    Secure = true,
                    HttpOnly = true,
                });
                _userId++;
                return RedirectToAction("OrderCounter");
            }
            return RedirectToAction("RegForm");
        }
        public IActionResult MakeOrders(OrderCounterV ordersCount)
        {
            int.TryParse(Request.Cookies["authId"], out int id);
            User? user = _users.FirstOrDefault(u => u.Id == id);
            if (int.TryParse(ordersCount.Count, out int count))
            {
                for (int i = 0; i < count; i++)
                {
                    _products.Add(new Product(_productId, null, null, user));
                    _productId++;
                }
            }
            return View("Index");
        }
        public IActionResult OrdersList()
        {
            int.TryParse(Request.Cookies["authId"], out int id);
            IEnumerable<Product> products = _products.Where(product => product.User.Id == id).ToList();
            OrderV orders = new OrderV(products);
            return View(orders);
        }
        public IActionResult OrderEdit(OrderV ordersView)
        {
            int.TryParse(Request.Cookies["authId"], out int id);
            User? user = _users.FirstOrDefault(usr => usr.Id == id);
            Console.WriteLine(user);
            List<Product> products = _products.Where(product => product.User == user).ToList();
            var orders = ordersView.Products.ToList();
            for (int i = 0; i < orders.Count(); i++)
            {
                Console.WriteLine(products[i]);
                Console.WriteLine(orders[i]);
                Console.WriteLine(user);
                products[i].NameTovar = orders[i].NameTovar;
                products[i].DescrTovar = orders[i].DescrTovar;
            }
            return View("Index");
        }
        public IActionResult EditOrdersView()
        {
            int.TryParse(Request.Cookies["authId"], out int id);
            IEnumerable<Product> products = _products.Where(product => product.User.Id == id).ToList();
            OrderV orders = new OrderV(products);
            return View("OrderEdit", orders);
        }
        public IActionResult OrderCounter()
        {
            string message = "Ви молодше 16ти ";
            if (Request.Cookies["authId"] != null)
            {
                int.TryParse(Request.Cookies["authId"], out int id);
                User? user = _users.FirstOrDefault(usr => usr.Id == id);
                if (user != null && user.Age > 16)
                    return View();
            }
            PermDenied prmDenied = new PermDenied(message);
            return View("PermDenied", prmDenied);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}