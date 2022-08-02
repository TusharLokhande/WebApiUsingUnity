using Projec.Data.Repository;
using Project.Core.Entity.Employee;
using Project.Service.Employee;
using Project.Web.Infrastructure;
using Project.Web.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Unity;
using Unity.Lifetime;

namespace Project.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            var container = new UnityContainer();
            container.RegisterType(typeof(IRepository<>), typeof(BaseRepository<>)); 
            container.RegisterType<IEmployee, EmployeeService>();
            config.DependencyResolver = new UnityResolver(container);
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
