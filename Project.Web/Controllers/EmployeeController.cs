using AutoMapper;
using Project.Core.Entity.Employee;
using Project.Service.DropDown;
using Project.Service.Employee;
using Project.Web.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web.Http;
using System.Web.Routing;

namespace Project.Web.Controllers
{

    [System.Web.Http.Route("api/{employee}/{action}") ]
    public class EmployeeController : ApiController
    {
        private readonly IEmployee _EmployeeService;
        private readonly IDropDown _DropDownService;
        public EmployeeController(IEmployee EmployeeService, IDropDown dropDownService)
        {
            _EmployeeService = EmployeeService;
            _DropDownService = dropDownService;
        }


        public IEnumerable<EmployeeModel> Get()
        {
           
            var op = Mapper.Map<IEnumerable<EmployeeModel>>(_EmployeeService.GetEmployees(10, 1, "EName", "ASC", ""));
            return op;
        }

        [HttpGet]
        public IEnumerable<DropDownModel> DropDown()
        {
            var l = Mapper.Map<IEnumerable<DropDownModel>>(_DropDownService.GetDeparmentList());
            return l;
        }
        
        [HttpGet]
        public IEnumerable<DropDownModel> reportingmanager()
        {
            var l = Mapper.Map<IEnumerable<DropDownModel>>(_DropDownService.GetReportingManagerList());
            return l;
        }


        [HttpPost]
        public void Post(EmployeeModel emp)
        {
            var op = Mapper.Map<EmployeeModel, EmployeeEntity>(emp);
            _EmployeeService.InsertAndUpdate(op);
        }

    }
}