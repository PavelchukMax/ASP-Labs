using lab_10.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace lab_10.Controllers
{
    public class HomeController : Controller
    {
        private static List<FormModel> registrations = new List<FormModel>();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Success()
        {
            return View(registrations);
        }
        public ActionResult RegForm()
        {
            return View(new FormModel());
        }
        [HttpPost]
        public IActionResult Register(FormModel formModel)
        {
            if (ModelState.IsValid)
            {
                registrations.Add(formModel);
                return View("Success", registrations);
            }
            else
                return View("RegForm", formModel);
        }
    }
}