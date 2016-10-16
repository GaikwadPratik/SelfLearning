using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObjects
{
    public class HappyHutUserInfo
    {
        public String UserId { get; set; }
        public String Username { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String MobileNumber { get; set; }
        public String AddressLine1 { get; set; }
        public String AddressLine2 { get; set; }
        public int CityId { get; set; }
        public String CityName { get; set; }
        public int StateId { get; set; }
        public String StateName { get; set; }
        public bool IsFirstLogin { get; set; }
        public String LastUpdateDt { get; set; }
        public String CreateDt { get; set; }
    }
}
