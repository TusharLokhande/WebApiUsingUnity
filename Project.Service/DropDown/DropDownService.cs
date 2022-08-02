using Projec.Data.Repository;
using Project.Core.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.DropDown
{
    public class DropDownService : IDropDown
    {

        IRepository<DropDownEntity> _DropService;

        public DropDownService(IRepository<DropDownEntity> DropService)
        {
            _DropService = DropService;
        }

  

        public IEnumerable<DropDownEntity> GetDeparmentList()
        {
            SqlCommand command = new SqlCommand("sp_GetDepartments");
            command.CommandType = CommandType.StoredProcedure;
            var list = _DropService.GetRecords(command);
            return list;
        }

        public IEnumerable<DropDownEntity> GetReportingManagerList()
        {
            SqlCommand command = new SqlCommand("sp_GetReportingManager");
            command.CommandType = CommandType.StoredProcedure;
            var list = _DropService.GetRecords(command);
            return list;
        }


    }

}
