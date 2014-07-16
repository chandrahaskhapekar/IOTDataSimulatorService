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
    
    public partial class User
    {
        public User()
        {
            this.Simulators = new HashSet<Simulator>();
            this.UserInRoles = new HashSet<UserInRole>();
        }
    
        public decimal UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public System.DateTime LastLoginDate { get; set; }
        public bool IsFirstTimeLogin { get; set; }
        public int LoginFailureAttempts { get; set; }
        public System.DateTime CreateDate { get; set; }
        public bool isActive { get; set; }
        public string PinCode { get; set; }
        public string CreatedBy { get; set; }
        public string Salt { get; set; }
    
        public virtual ICollection<Simulator> Simulators { get; set; }
        public virtual ICollection<UserInRole> UserInRoles { get; set; }
    }
}