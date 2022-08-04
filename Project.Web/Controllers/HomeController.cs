using AutoMapper;
using Project.Service.DropDown;
using Project.Service.Employee;
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
        private readonly IEmployee _EmployeeService;

       

        public HomeController(IEmployee EmployeeService)
        {
            _EmployeeService = EmployeeService;
        }
        
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult DashBoard()
        {
            return View();
        }

        public ActionResult Create(int? id)
        {
            EmployeeModel obj = new EmployeeModel();
            if(id > 0)
            {
                obj = Mapper.Map<EmployeeModel>(_EmployeeService.GetEmployeeById(id));
            }
            return View(obj);
        }


       
    }
}
