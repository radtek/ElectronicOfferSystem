﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ElectronicOfferSystem
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ElectronicOfferSystemDBContainer : DbContext
    {
        public ElectronicOfferSystemDBContainer()
            : base("name=ElectronicOfferSystemDBContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<NaturalBuilding> NaturalBuilding { get; set; }
        public virtual DbSet<LogicalBuilding> LogicalBuilding { get; set; }
        public virtual DbSet<Floor> Floor { get; set; }
        public virtual DbSet<Obligee> Obligee { get; set; }
        public virtual DbSet<Household> Household { get; set; }
        public virtual DbSet<CONST> BDCS_CONST { get; set; }
        public virtual DbSet<CONSTCLS> BDCS_CONSTCLS { get; set; }
        public virtual DbSet<Applicant> Applicant { get; set; }
        public virtual DbSet<Transfer> Transfer { get; set; }
        public virtual DbSet<FileInfo> FileInfo { get; set; }
    }
}
