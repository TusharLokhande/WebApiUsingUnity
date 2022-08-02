using Project.Framework.MVC;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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


        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]

        public DateTime? DateOfBirth { get; set; }

        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }

        public string ReportingManagerName { get; set; }

        public int ReportingManagerId { get; set; }

        public string Email { get; set; }

        public int isActive { get; set; }
        #endregion
    }
}