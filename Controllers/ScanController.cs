using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoadTruck.Models;

namespace LoadTruck.Controllers
{
    public class ScanController : Controller
    {
        // GET: Scan
        public ActionResult Index()
        {
            return View();
        }

        // GET: Scan/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Scan/Create
        public ActionResult Create()
        {
            TruckLoad tl = new TruckLoad();
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

        // POST: Scan/Create
        [HttpPost]
        public ActionResult Create(TruckLoad tl)
        {
            try
            {
                tl.hasError = false;
                // TODO: Add insert logic here
                if (tl.TrailerID == null)
                {
                    tl.hasError = true;
                    TempData["message"] = "Trailer Missing";
                }
                else
                {
                    string ret = tl.checkTrailer();
                    TempData["message"] = ret;
                    if (!ret.Contains("Success"))
                    {
                        tl.hasError = true;
                        tl.TrailerID = "";
                    }

                }

                if (!(tl.LawTag == null))
                {
                    Lawtag lt = new Lawtag();
                    lt.TagNum = tl.LawTag;
                    string ltmessage = lt.store();
                    TempData["message"] = ltmessage;
                    if (ltmessage.Contains("already"))
                    {
                        if (tl.LawTagDelete == "yes")
                        {
                            //do delete
                            tl.LawTagStatus = "none";
                            tl.LawTagDelete = "no";
                            tl.LawTag = null;
                            TempData["message"] = "Removed";
                        }
                        else
                        {
                            tl.LawTagStatus = "already";
                        }
                    }
                    else
                    {
                        tl.LawTag = null;
                    }
                }
                TempData["load"] = tl;
                return RedirectToAction("Create");
            }
            catch
            {
                return View();
            }
        }

        // GET: Scan/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Scan/Edit/5
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

        // GET: Scan/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Scan/Delete/5
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
