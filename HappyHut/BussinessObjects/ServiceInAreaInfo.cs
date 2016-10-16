using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObjects
{
    public class ServiceInAreaInfo
    {
        public int ServiceInAreaId { get; set; }
        public int ServiceID { get; set; }
        public String ServiceName { get; set; }
        public int AreaID { get; set; }
        public bool IsActive { get; set; }
    }
}
