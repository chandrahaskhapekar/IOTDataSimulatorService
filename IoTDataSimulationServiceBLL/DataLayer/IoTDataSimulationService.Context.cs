﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class IoTDataSimulatorDBEntities : DbContext
    {
        public IoTDataSimulatorDBEntities()
            : base("name=IoTDataSimulatorDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SchemaFile> SchemaFiles { get; set; }
        public virtual DbSet<SimulatorProcessDetail> SimulatorProcessDetails { get; set; }
        public virtual DbSet<Simulator> Simulators { get; set; }
        public virtual DbSet<URLDetail> URLDetails { get; set; }
        public virtual DbSet<UserInRole> UserInRoles { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}