//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HappyHutService
{
    using System;
    using System.Collections.Generic;
    
    public partial class HappyHutUser
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public bool IsFirstLogin { get; set; }
        public System.DateTime LastUpdateDt { get; set; }
        public System.DateTime CreateDt { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
