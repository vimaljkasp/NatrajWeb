//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Platform.Sql
{
    using System;
    using System.Collections.Generic;
    
    public partial class DCPaymentDetail
    {
        public int DCPaymentId { get; set; }
        public Nullable<int> DCId { get; set; }
        public Nullable<int> DCOrderId { get; set; }
        public Nullable<decimal> PaymentCrAmount { get; set; }
        public Nullable<decimal> PaymentDrAmount { get; set; }
        public System.DateTime PaymentDate { get; set; }
        public string PaymentReceivedBy { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentComments { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual DCOrder DCOrder { get; set; }
        public virtual DistributionCenter DistributionCenter { get; set; }
    }
}
