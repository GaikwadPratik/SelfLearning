//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HappyHutMiddleTier
{
    using System;
    using System.Collections.Generic;
    
    public partial class AreasInCity
    {
        public AreasInCity()
        {
            this.ServicesInAreas = new HashSet<ServicesInArea>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public int CityID { get; set; }
    
        public virtual City City { get; set; }
        public virtual ICollection<ServicesInArea> ServicesInAreas { get; set; }
    }
}
