﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseService
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DoctorPhoneEntities3 : DbContext
    {
        public DoctorPhoneEntities3()
            : base("name=DoctorPhoneEntities3")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ClientsDb> ClientsDbs { get; set; }
        public virtual DbSet<DevicesDb> DevicesDbs { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<PartsDb> PartsDbs { get; set; }
        public virtual DbSet<UsersDb> UsersDbs { get; set; }
        public virtual DbSet<WorksDb> WorksDbs { get; set; }
    }
}
