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

            SqlCommand command = new SqlCommand("EmployeMaster");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@pageSize", SqlDbType.BigInt).Value = pageSize;
            command.Parameters.Add("@start", SqlDbType.BigInt).Value = start;
            command.Parameters.Add("@SortColumn", SqlDbType.VarChar).Value = sortColumn;
            command.Parameters.Add("@SortOrder", SqlDbType.VarChar).Value = sortOrder;
            command.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = searchText;
            var list = _EmpRepository.GetRecords(command);
            return list;
        }

        public object InsertAndUpdate(EmployeeEntity entity)
        {
            SqlCommand command = new SqlCommand("sp_EmployeeInsertUpdate");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = entity.Id;
            command.Parameters.AddWithValue("@EName", SqlDbType.VarChar).Value = entity.EName;
            command.Parameters.AddWithValue("@Email", SqlDbType.VarChar).Value = entity.Email;
            command.Parameters.AddWithValue("@DateOfBirth", SqlDbType.Date).Value = entity.DateOfBirth;
            command.Parameters.AddWithValue("@DepartmentId", SqlDbType.Int).Value = entity.DepartmentId;
            command.Parameters.AddWithValue("@ReportingManagerId", SqlDbType.Int).Value = entity.ReportingManagerId;
            command.Parameters.AddWithValue("@isActive", SqlDbType.Bit).Value = entity.isActive;
            return _EmpRepository.ExecuteQuery(command);
        }
    }
}
