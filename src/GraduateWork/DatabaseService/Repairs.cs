//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Repairs
    {
        public int Id { get; set; }
        public int Kod { get; set; }
        public System.DateTime OrderDate { get; set; }
        public int Status { get; set; }
        public bool IsWarranty { get; set; }
        public Nullable<int> RepairDeviceId { get; set; }
        public int WorkerId { get; set; }
        public int DeviceId { get; set; }
        public Nullable<int> PartId { get; set; }
        public int WorkId { get; set; }
    
        public virtual Devices Devices { get; set; }
        public virtual Parts Parts { get; set; }
        public virtual RepairDevices RepairDevices { get; set; }
        public virtual Works Works { get; set; }
        public virtual Users Users { get; set; }
    }
}
