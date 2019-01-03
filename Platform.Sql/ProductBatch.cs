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
    
    public partial class ProductBatch
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductBatch()
        {
            this.ProductBatchDetails = new HashSet<ProductBatchDetail>();
            this.ProductBatchQualities = new HashSet<ProductBatchQuality>();
        }
    
        public int BatchId { get; set; }
        public string BatchNo { get; set; }
        public Nullable<int> ProcessingId { get; set; }
        public Nullable<System.DateTime> ProductBatchDate { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<decimal> BatchQuantity { get; set; }
        public Nullable<decimal> BatchRemainingQunatity { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual MilkProcessing MilkProcessing { get; set; }
        public virtual Product Product { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductBatchDetail> ProductBatchDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductBatchQuality> ProductBatchQualities { get; set; }
    }
}