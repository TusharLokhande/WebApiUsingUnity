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

    [Route("api/{employee}/{action}") ]
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
            var data = Mapper.Map<IEnumerable<EmployeeModel>>(_EmployeeService.GetEmployees(10, 1, "EName", "ASC", ""));
            return data;
        }

        [HttpGet]
        public IEnumerable<DropDownModel> DropDown()
        {
            var data = Mapper.Map<IEnumerable<DropDownModel>>(_DropDownService.GetDeparmentList());
            return data;
        }
        
        [HttpGet]
        public IEnumerable<DropDownModel> reportingmanager()
        {
            var data = Mapper.Map<IEnumerable<DropDownModel>>(_DropDownService.GetReportingManagerList());
            return data;
        }

        [HttpGet]
        public EmployeeModel GetEmployeeById(int? Id)
        {
            var data = Mapper.Map<EmployeeModel>(_EmployeeService.GetEmployeeById(Id));
            return data;
        }

        [HttpPost]
        public void Post(EmployeeModel emp)
        {
            var op = Mapper.Map<EmployeeModel, EmployeeEntity>(emp);
            _EmployeeService.InsertAndUpdate(op);
        }

    }
}