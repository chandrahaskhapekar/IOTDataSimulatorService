//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IoTDataSimulationServiceBLL.DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserInRole
    {
        public decimal ID { get; set; }
        public decimal UserID { get; set; }
        public decimal RoleID { get; set; }
    
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
