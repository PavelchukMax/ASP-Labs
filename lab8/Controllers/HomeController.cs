﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lab8.Models;
using lab8.ViewModels.HomeViewModels;

namespace lab8.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<Order> _orders = new();
        public ActionResult Index()
        {
            return View();
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
        public ActionResult ShowOrder(ShowStyles showStyle)
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