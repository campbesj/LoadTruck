using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoadTruck.Models;


namespace LoadTruck.Controllers
{
    public class LoadController : Controller
    {
        // GET: Load
        public ActionResult Index()
        {
            return View();
        }

        // GET: Load/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Load/Create
        public ActionResult Create()
        {
            TruckLoad tl = new TruckLoad();
            tl.Ship_Date = DateTime.Today.AddDays(1);
            if (!(TempData["load"] == null))
            {
                tl = (TruckLoad)TempData["load"];
            }
            if (!(TempData["message"] == null))
            {
                ViewBag.Message = TempData["message"].ToString();
            }
            return View(tl);
        }

        // POST: Load/Create
        [HttpPost]
        public ActionResult Create(TruckLoad tl)
        {
            try
            {
                tl.hasError = false;
                // TODO: Add insert logic here
                string message = tl.store();
                TempData["message"] = message;
                TempData["load"] = tl;
                if (message.Contains("Success"))
                {
                    return RedirectToAction("Create", "Scan");
                }
                else
                {
                    tl.hasError = true;
                    TempData["load"] = tl;
                    return RedirectToAction("Create");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Load/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Load/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Load/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Load/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
