using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Adarsh_Studio.Models;

public partial class AdarshSdbContext : DbContext
{
    public AdarshSdbContext()
    {
    }

    public AdarshSdbContext(DbContextOptions<AdarshSdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BookingMaster> BookingMasters { get; set; }

    public virtual DbSet<CityMaster> CityMasters { get; set; }

    public virtual DbSet<EnquiryMaster> EnquiryMasters { get; set; }

    public virtual DbSet<FeedbackMaster> FeedbackMasters { get; set; }

    public virtual DbSet<LoginMaster> LoginMasters { get; set; }

    public virtual DbSet<PackageMaster> PackageMasters { get; set; }

    public virtual DbSet<ServiceMaster> ServiceMasters { get; set; }

    public virtual DbSet<ServicePicMaster> ServicePicMasters { get; set; }

    public virtual DbSet<StaffMaster> StaffMasters { get; set; }

    public virtual DbSet<SubcribeMaster> SubcribeMasters { get; set; }

    public virtual DbSet<UpdatesMaster> UpdatesMasters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Read connection from appsettings.json.
            optionsBuilder.UseSqlServer(WebApplication.CreateBuilder().Configuration.GetSection("ConnectionStrings")["ConStr"]);
        }
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookingMaster>(entity =>
        {
            entity.HasKey(e => e.BookingId);

            entity.ToTable("BookingMaster");

            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ClientName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("Created_On");
            entity.Property(e => e.EmailId)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Remark)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.ShootingDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.CurrentCityNavigation).WithMany(p => p.BookingMasterCurrentCityNavigations)
                .HasForeignKey(d => d.CurrentCity)
                .HasConstraintName("FK_BookingMaster_CityMaster1");

            entity.HasOne(d => d.LocationOfShootingNavigation).WithMany(p => p.BookingMasterLocationOfShootingNavigations)
                .HasForeignKey(d => d.LocationOfShooting)
                .HasConstraintName("FK_BookingMaster_CityMaster");
        });

        modelBuilder.Entity<CityMaster>(entity =>
        {
            entity.HasKey(e => e.CityId);

            entity.ToTable("CityMaster");

            entity.Property(e => e.CityName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("Created_On");
        });

        modelBuilder.Entity<EnquiryMaster>(entity =>
        {
            entity.HasKey(e => e.EnquiryId);

            entity.ToTable("EnquiryMaster");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.EmailId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MobNo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.QueryMsg)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FeedbackMaster>(entity =>
        {
            entity.HasKey(e => e.FeedbackId);

            entity.ToTable("FeedbackMaster");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.EmailId)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.FeedbackMsg).IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TitleOfFeedback)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LoginMaster>(entity =>
        {
            entity.HasKey(e => e.AdminId);

            entity.ToTable("LoginMaster");

            entity.Property(e => e.AdminId)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.AdminPass)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("Admin_Pass");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("Created_On");
            entity.Property(e => e.IsBlocked).HasColumnName("Is_Blocked");
            entity.Property(e => e.LastLoginDt)
                .HasColumnType("datetime")
                .HasColumnName("Last_Login_DT");
            entity.Property(e => e.LoginCount).HasColumnName("Login_Count");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("datetime")
                .HasColumnName("Updated_On");
            entity.Property(e => e.VerificationCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VerificationCodeExpiry).HasColumnType("datetime");
        });

        modelBuilder.Entity<PackageMaster>(entity =>
        {
            entity.HasKey(e => e.PackageId);

            entity.ToTable("PackageMaster");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Detail1)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Detail2)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Detail3)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Detail4)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PackageTitle)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ServiceMaster>(entity =>
        {
            entity.HasKey(e => e.ServiceId);

            entity.ToTable("ServiceMaster");

            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("Created_On");
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Exclusions)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Inclusions)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ServiceType)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("datetime")
                .HasColumnName("Updated_On");
        });

        modelBuilder.Entity<ServicePicMaster>(entity =>
        {
            entity.HasKey(e => e.PicId);

            entity.ToTable("ServicePicMaster");

            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("Created_On");
            entity.Property(e => e.PicFileName)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.PicFolderName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PicSizeInKb).HasColumnName("PicSize_InKB");
            entity.Property(e => e.PicType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Remark)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.Service).WithMany(p => p.ServicePicMasters)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK_ServicePicMaster_ServiceMaster");
        });

        modelBuilder.Entity<StaffMaster>(entity =>
        {
            entity.HasKey(e => e.StaffId);

            entity.ToTable("StaffMaster");

            entity.Property(e => e.Address)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ImgFileName)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.ImgFolderName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ImgSizeInKb).HasColumnName("ImgSizeInKB");
            entity.Property(e => e.ImgType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Specialization)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SubcribeMaster>(entity =>
        {
            entity.ToTable("SubcribeMaster");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UpdatesMaster>(entity =>
        {
            entity.HasKey(e => e.UpdateId);

            entity.ToTable("UpdatesMaster");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdateMsg)
                .HasMaxLength(300)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
