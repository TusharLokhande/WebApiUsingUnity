using AutoMapper;
using Project.Service.Employee;
using Project.Web.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace Project.Web.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployee _EmployeeService;
        public EmployeeController(IEmployee EmployeeService)
        {
            _EmployeeService = EmployeeService;
        }


      
        public IEnumerable<EmployeeModel> Get()
        {
           
            var op = Mapper.Map<IEnumerable<EmployeeModel>>(_EmployeeService.GetEmployees(10, 1, "EName", "ASC", ""));
            return op;
        }


      

    }
}