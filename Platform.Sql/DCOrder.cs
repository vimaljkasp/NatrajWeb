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
    
    public partial class DCOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DCOrder()
        {
            this.DCOrderDtls = new HashSet<DCOrderDtl>();
            this.DCPaymentDetails = new HashSet<DCPaymentDetail>();
        }
    
        public int DCOrderId { get; set; }
        public string DCOrderNumber { get; set; }
        public System.DateTime OrderDate { get; set; }
        public string BillNumber { get; set; }
        public int DCId { get; set; }
        public int OrderAddressId { get; set; }
        public decimal OrderPrice { get; set; }
        public Nullable<decimal> SGST { get; set; }
        public Nullable<decimal> CGST { get; set; }
        public Nullable<decimal> IGST { get; set; }
        public Nullable<decimal> OrderTax { get; set; }
        public Nullable<decimal> OrderDiscount { get; set; }
        public decimal OrderTotalPrice { get; set; }
        public decimal OrderPaidAmount { get; set; }
        public decimal TotalOrderQuantity { get; set; }
        public Nullable<decimal> TotalActualQuantity { get; set; }
        public Nullable<int> OrderStatusId { get; set; }
        public Nullable<System.DateTime> DeliveryExpectedDate { get; set; }
        public Nullable<System.DateTime> DeliveredDate { get; set; }
        public string DeliveredBy { get; set; }
        public string OrderComments { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual DCAddress DCAddress { get; set; }
        public virtual DistributionCenter DistributionCenter { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DCOrderDtl> DCOrderDtls { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DCPaymentDetail> DCPaymentDetails { get; set; }
    }
}
