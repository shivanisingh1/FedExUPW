﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace c3i_fedex.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class C3_FedexCasedetailsEntities : DbContext
    {
        public C3_FedexCasedetailsEntities()
            : base("name=C3_FedexCasedetailsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<add_case_notes> add_case_notes { get; set; }
        public DbSet<agent_case_attempts_made> agent_case_attempts_made { get; set; }
        public DbSet<agent_case_details> agent_case_details { get; set; }
        public DbSet<agent_details> agent_details { get; set; }
        public DbSet<alternate_phone_numbers> alternate_phone_numbers { get; set; }
        public DbSet<bot_trigger> bot_trigger { get; set; }
        public DbSet<case_notes> case_notes { get; set; }
        public DbSet<folder_details> folder_details { get; set; }
        public DbSet<folder_ids> folder_ids { get; set; }
        public DbSet<login> logins { get; set; }
        public DbSet<package_details> package_details { get; set; }
        public DbSet<package_scan_details> package_scan_details { get; set; }
        public DbSet<primary_case_details> primary_case_details { get; set; }
        public DbSet<primary_rep_details> primary_rep_details { get; set; }
        public DbSet<primary_requirements> primary_requirements { get; set; }
        public DbSet<recipient_details> recipient_details { get; set; }
        public DbSet<shipper_details> shipper_details { get; set; }
        public DbSet<status> status { get; set; }
        public DbSet<status_case_details> status_case_details { get; set; }
        public DbSet<case_details> case_details { get; set; }
        public DbSet<time_zone_codes> time_zone_codes { get; set; }
        public DbSet<location_details_dest> location_details_dest { get; set; }
        public DbSet<location_details_origion> location_details_origion { get; set; }
        public DbSet<send_template> send_template { get; set; }
        public DbSet<geda_notifications> geda_notifications { get; set; }
        public DbSet<currency_codes> currency_codes { get; set; }
        public DbSet<send_temp> send_temp { get; set; }
        public DbSet<bot_password> bot_password { get; set; }
        public DbSet<update_bot_password> update_bot_password { get; set; }
    }
}
