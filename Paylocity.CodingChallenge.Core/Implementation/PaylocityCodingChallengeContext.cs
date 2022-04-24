using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using Paylocity.CodingChallenge.Core.Interfaces;
using Paylocity.CodingChallenge.Entities;

namespace Paylocity.CodingChallenge.Core.Implementation
{
    public partial class PaylocityCodingChallengeContext : DbContext, IPaylocityCodingChallengeContext
    {
        private readonly ILogger logger;

        public PaylocityCodingChallengeContext(ILogger<PaylocityCodingChallengeContext> logger)
        {
            this.logger = logger;
        }

        public PaylocityCodingChallengeContext(DbContextOptions<PaylocityCodingChallengeContext> options, ILogger<PaylocityCodingChallengeContext> logger)
            : base(options)
        {
            this.logger = logger;
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DependentType> DependentTypes { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeDependent> EmployeeDependents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            logger.LogInformation("PaylocityCodingChallengeContext-OnModelCreating-Start");
            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentName).HasMaxLength(50);

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DependentType>(entity =>
            {
                entity.ToTable("DependentType");

                entity.Property(e => e.DependentType1).HasColumnName("DependentType");

                entity.Property(e => e.Description).HasMaxLength(50);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmployeeId).HasMaxLength(10);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
                entity.Property(e => e.JoiningDate).HasColumnType("datetime");



                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_Employee_Department");
            });

            modelBuilder.Entity<EmployeeDependent>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DependentName).HasMaxLength(50);

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.DependentTypeNavigation)
                    .WithMany(p => p.EmployeeDependents)
                    .HasForeignKey(d => d.DependentType)
                    .HasConstraintName("FK_EmployeeDependents_DependentType");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeDependents)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_EmployeeDependents_Employee");
            });

            OnModelCreatingPartial(modelBuilder);
            logger.LogInformation("PaylocityCodingChallengeContext-OnModelCreating-End");
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
