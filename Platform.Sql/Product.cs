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
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.DCOrderDtls = new HashSet<DCOrderDtl>();
            this.ProductBatches = new HashSet<ProductBatch>();
        }
    
        public int ProductId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> SubCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Unit { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<decimal> SGST { get; set; }
        public Nullable<decimal> CGST { get; set; }
        public Nullable<decimal> IGST { get; set; }
        public Nullable<decimal> Acidity { get; set; }
        public Nullable<decimal> FAT { get; set; }
        public string REF1 { get; set; }
        public string REF2 { get; set; }
        public string REF3 { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DCOrderDtl> DCOrderDtls { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ProductSubCategory ProductSubCategory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductBatch> ProductBatches { get; set; }
    }
}
