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
    
    public partial class ProductBatchQuality
    {
        public int QualityId { get; set; }
        public Nullable<int> BatchId { get; set; }
        public Nullable<decimal> FAT { get; set; }
        public Nullable<decimal> SNF { get; set; }
        public Nullable<decimal> Acidity { get; set; }
        public Nullable<System.DateTime> TestDate { get; set; }
        public string Comments { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual ProductBatch ProductBatch { get; set; }
    }
}
