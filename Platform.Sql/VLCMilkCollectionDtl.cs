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
    
    public partial class VLCMilkCollectionDtl
    {
        public int VLCMilkCollectionDtlId { get; set; }
        public int VLCMilkCollectionId { get; set; }
        public Nullable<decimal> Qunatity { get; set; }
        public Nullable<decimal> FAT { get; set; }
        public Nullable<decimal> CLR { get; set; }
        public Nullable<decimal> RatePerUnit { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<int> ProductId { get; set; }
    
        public virtual VLCMilkCollection VLCMilkCollection { get; set; }
    }
}