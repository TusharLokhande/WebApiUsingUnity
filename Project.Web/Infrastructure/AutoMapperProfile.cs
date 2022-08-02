using AutoMapper;
using Project.Core.Entity.Employee;
using Project.Web.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Project.Web.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            
            Mapper.CreateMap<EmployeeModel, EmployeeEntity>();
            Mapper.CreateMap<EmployeeEntity, EmployeeModel>();
            Mapper.CreateMap<DropDownModel, DropDownEntity>();
            Mapper.CreateMap<DropDownEntity, DropDownModel>();
        }
    }
}