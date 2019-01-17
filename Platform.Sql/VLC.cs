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
    
    public partial class VLC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VLC()
        {
            this.Customers = new HashSet<Customer>();
            this.DockMilkCollectionDetails = new HashSet<DockMilkCollectionDetail>();
            this.DockRejectedMilks = new HashSet<DockRejectedMilk>();
            this.VLCMilkCollections = new HashSet<VLCMilkCollection>();
        }
    
        public int VLCId { get; set; }
        public string VLCCode { get; set; }
        public string VLCName { get; set; }
        
        public Nullable<System.DateTime> VLCEnrollmentDate { get; set; }
        public string AgentName { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string VLCAddress { get; set; }
        public string Village { get; set; }
        public string City { get; set; }
        public string VLCState { get; set; }
        public string Password { get; set; }
        public string AlternateContact { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string VLCAgentAadhaar { get; set; }
        public Nullable<decimal> CLR { get; set; }
        public Nullable<decimal> FAT { get; set; }
        public Nullable<decimal> DifferenceCLR { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer> Customers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DockMilkCollectionDetail> DockMilkCollectionDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DockRejectedMilk> DockRejectedMilks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VLCMilkCollection> VLCMilkCollections { get; set; }
    }
}
