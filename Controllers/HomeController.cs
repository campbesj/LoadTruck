using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoadTruck.Models;


namespace LoadTruck.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //ViewBag.Title = "Home Page";
            //TruckLoad tl = new TruckLoad();
            //tl.store();
            return View();
        }
    }
}
