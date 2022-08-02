using Project.Core.Entity.Employee;
using System.Collections.Generic;

namespace Project.Service.DropDown
{
    public interface IDropDown
    {
        IEnumerable<DropDownEntity> GetDeparmentList();

        IEnumerable<DropDownEntity> GetReportingManagerList();
    }
}
