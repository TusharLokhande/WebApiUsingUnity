using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Framework.MVC
{
    public class BaseModel
    {

        public Int64 Id { get; set; }

        public Int64 UserId { get; set; }

        public Int64 RoleId { get; set; }

        public bool IsActive { get; set; }

        public Int64 CreatedBy { get; set; }

        public DateTime CreatedDateUtc { get; set; }

        public Int64 ModifiedBy { get; set; }

        public DateTime ModifiedDateUtc { get; set; }

        public Int64 TotalCount { get; set; }

        public Int64 TotalPage { get; set; }

        public string EncryptedParam { get; set; }

        public string Name { get; set; }

        public Int64 RowNum { get; set; }

        public string CreatedByName { get; set; }

        public string ModifiedByName { get; set; }
    }
}
