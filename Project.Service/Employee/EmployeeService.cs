using Projec.Data.Repository;
using Project.Core.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Employee
{
    public class EmployeeService : IEmployee
    {

        private IRepository<EmployeeEntity> _EmpRepository;
        public EmployeeService(IRepository<EmployeeEntity> Emprepository)
        {
            _EmpRepository = Emprepository;
        }

        public IEnumerable<EmployeeEntity> GetEmployees(int pageSize, int start, string sortColumn, string sortOrder, string searchText)
        {

            SqlCommand command = new SqlCommand("EmployeeMaster");
            command.CommandType = CommandType.StoredProcedure;
      
            var list = _EmpRepository.GetRecords(command);
            return list;
        }

        public object InsertAndUpdate(EmployeeEntity entity)
        {
            SqlCommand command = new SqlCommand("sp_EmployeeInsertUpdate");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", entity.Id);
            command.Parameters.AddWithValue("@EName", entity.EName);
            command.Parameters.AddWithValue("@Email", entity.Email);
            command.Parameters.AddWithValue("@DateOfBirth", entity.DateOfBirth);
            command.Parameters.AddWithValue("@DepartmentId", entity.DepartmentId);
            command.Parameters.AddWithValue("@ReportingManagerId", entity.ReportingManagerId);
            command.Parameters.AddWithValue("@isActive", entity.isActive);
            return _EmpRepository.ExecuteQuery(command);
        }

       
    }
}
