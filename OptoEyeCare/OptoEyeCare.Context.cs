﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OptoEyeCare
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OptoEyeCareEntities : DbContext
    {
        public OptoEyeCareEntities()
            : base("name=OptoEyeCareEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<mst_User> mst_User { get; set; }
        public virtual DbSet<tblContactLenses> tblContactLenses { get; set; }
        public virtual DbSet<frame> frame { get; set; }
        public virtual DbSet<lenseDetails> lenseDetails { get; set; }
        public virtual DbSet<email_configuration> email_configuration { get; set; }
        public virtual DbSet<CYL> CYL { get; set; }
        public virtual DbSet<SPH> SPH { get; set; }
        public virtual DbSet<axis> axis { get; set; }
        public virtual DbSet<addition> addition { get; set; }
        public virtual DbSet<vision> vision { get; set; }
    }
}
