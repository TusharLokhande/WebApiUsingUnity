using Project.Framework.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models.Employee
{
    public class EmployeeModel:BaseModel
    {
        #region Lists
        public List<DropDownModel> Departmentlist { get; set; }
        #endregion


        public EmployeeModel()
        {
            Departmentlist = new List<DropDownModel>();
        }


        #region Property
        public long Id { get; set; }
        public long TotalCount { get; set; }

        public string EName { get; set; }


        public string DateOfBirth { get; set; }

        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }

        public string ReportingManagerName { get; set; }

        public int ReportingManagerId { get; set; }

        public string Email { get; set; }

        public int isActive { get; set; }
        #endregion
    }
}