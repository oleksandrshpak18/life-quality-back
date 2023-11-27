﻿using life_quality_back.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace life_quality_back.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<TreatmentStrategy> TreatmentStrategies { get; set; }
    }
}
