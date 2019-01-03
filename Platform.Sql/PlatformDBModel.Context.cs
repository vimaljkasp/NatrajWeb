﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PlatformDBEntities : DbContext
    {
        public PlatformDBEntities()
            : base("name=PlatformDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerAgriculture> CustomerAgricultures { get; set; }
        public virtual DbSet<CustomerBank> CustomerBanks { get; set; }
        public virtual DbSet<DCAddress> DCAddresses { get; set; }
        public virtual DbSet<DCOrder> DCOrders { get; set; }
        public virtual DbSet<DCOrderDtl> DCOrderDtls { get; set; }
        public virtual DbSet<DCPaymentDetail> DCPaymentDetails { get; set; }
        public virtual DbSet<DCWallet> DCWallets { get; set; }
        public virtual DbSet<DistributionCenter> DistributionCenters { get; set; }
        public virtual DbSet<DockMilkCollection> DockMilkCollections { get; set; }
        public virtual DbSet<DockMilkCollectionDetail> DockMilkCollectionDetails { get; set; }
        public virtual DbSet<DockRejectedMilk> DockRejectedMilks { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<MilkProcessing> MilkProcessings { get; set; }
        public virtual DbSet<NumberMaster> NumberMasters { get; set; }
        public virtual DbSet<OrderStatu> OrderStatus { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductBatch> ProductBatches { get; set; }
        public virtual DbSet<ProductBatchDetail> ProductBatchDetails { get; set; }
        public virtual DbSet<ProductBatchQuality> ProductBatchQualities { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductSubCategory> ProductSubCategories { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<ScreenSecurityCode> ScreenSecurityCodes { get; set; }
        public virtual DbSet<UserPermission> UserPermissions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VLC> VLCs { get; set; }
        public virtual DbSet<VLCMilkCollection> VLCMilkCollections { get; set; }
        public virtual DbSet<VLCMilkCollectionDtl> VLCMilkCollectionDtls { get; set; }
    }
}
