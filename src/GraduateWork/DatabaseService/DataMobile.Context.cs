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
    
    public partial class MobiDoc : DbContext
    {
        public MobiDoc()
            : base("name=MobiDoc")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Devices> Devices { get; set; }
        public virtual DbSet<Parts> Parts { get; set; }
        public virtual DbSet<PersonalData> PersonalData { get; set; }
        public virtual DbSet<RepairDevices> RepairDevices { get; set; }
        public virtual DbSet<Repairs> Repairs { get; set; }
        public virtual DbSet<Reviews> Reviews { get; set; }
        public virtual DbSet<Sellings> Sellings { get; set; }
        public virtual DbSet<Table> Table { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Works> Works { get; set; }
    }
}
