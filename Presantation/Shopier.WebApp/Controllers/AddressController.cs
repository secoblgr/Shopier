﻿using Microsoft.AspNetCore.Mvc;

namespace Shopier.WebApp.Controllers
{
    public class AddressController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetCity()
        {
            return Json(new { success = true });
        }
    }
}
