using AutoMapper;
using Project.Service.DropDown;
using Project.Web.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers
{
    public class HomeController : Controller
    {

        
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult DashBoard()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

       
    }
}
