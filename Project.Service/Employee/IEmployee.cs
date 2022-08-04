using Project.Core.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Employee
{
    public interface IEmployee
    {
        IEnumerable<EmployeeEntity> GetEmployees(int pageSize, int start, string sortColumn, string sortOrder, string searchText);
        object InsertAndUpdate(EmployeeEntity entity);
         EmployeeEntity GetEmployeeById(int? id);
    }
}
