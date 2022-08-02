using Projec.Data.Repository;
using Project.Service.Employee;
using System.Web.Http;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using Unity.WebApi;

namespace Project.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            
            
          
            container.RegisterType(typeof(IRepository<>), typeof(BaseRepository<>));
            container.RegisterType<IEmployee, EmployeeService>();
            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}