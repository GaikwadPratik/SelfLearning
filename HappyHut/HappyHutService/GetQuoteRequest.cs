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
    
    public partial class GetQuoteRequest
    {
        public System.Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public int ServiceId { get; set; }
        public System.DateTime PreferredDate { get; set; }
        public System.TimeSpan PreferredTime { get; set; }
        public string AdditionalInfo { get; set; }
        public bool IsEmailSent { get; set; }
        public Nullable<System.DateTime> EmailSentDt { get; set; }
        public System.DateTime LastUpdateDt { get; set; }
        public System.DateTime CreateDt { get; set; }
    
        public virtual ServicesInArea ServicesInArea { get; set; }
    }
}
