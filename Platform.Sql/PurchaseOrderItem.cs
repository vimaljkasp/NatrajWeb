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
    
    public partial class PurchaseOrderItem
    {
        public int OrderItemId { get; set; }
        public Nullable<int> PurchaseOrderId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> SubCategoryId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<decimal> Quntity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public string Comments { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
