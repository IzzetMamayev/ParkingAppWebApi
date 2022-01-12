using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ParkingAppWebApi.Models;

namespace ParkingAppWebApi.Data

{
    public partial class ParkingDBContext : DbContext
    {
        public ParkingDBContext()
        {
        }

        public ParkingDBContext(DbContextOptions<ParkingDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car_Models> Car_Models { get; set; }
        public virtual DbSet<Car_Types> AutoTypes { get; set; }
        public virtual DbSet<User_Cars> User_Cars { get; set; }
        public virtual DbSet<DateTypes> DateTypes { get; set; }
        public virtual DbSet<Inspectors> Inspectors { get; set; }
        public virtual DbSet<ParkingUsers> ParkingUsers { get; set; }
        public virtual DbSet<Parkings> Parkings { get; set; }
        public virtual DbSet<Prices> Prices { get; set; }
        public virtual DbSet<Zones> Zones { get; set; }
        public virtual DbSet<Payments> Payments { get; set; }
        public virtual DbSet<Nlog> Nlogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("ParkingDBConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Nlog>(entity =>
            {
                entity.ToTable("NLog");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Callsite).HasMaxLength(300);

                entity.Property(e => e.Level)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Logged).HasColumnType("datetime");

                entity.Property(e => e.Logger).HasMaxLength(300);

                entity.Property(e => e.MachineName).HasMaxLength(200);

                entity.Property(e => e.Message).IsRequired();
            });



        modelBuilder.Entity<Payments>(entity =>
            {
                entity.HasKey(e => e.IdPayment)
                    .HasName("PK_paymentsss");

                entity.ToTable("payments");

                entity.Property(e => e.IdPayment).HasColumnName("id_payment");

                entity.Property(e => e.DateFrom)
                    .HasColumnName("date_from")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateTo)
                    .HasColumnName("date_to")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdCar).HasColumnName("id_car");

                entity.Property(e => e.IdParking).HasColumnName("id_parking");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.IdZone).HasColumnName("id_zone");

                entity.Property(e => e.PayDate)
                    .HasColumnName("pay_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SerialNumber)
                    .HasColumnName("serial_number")
                    .HasMaxLength(9)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Car_Models>(entity =>
            {
                entity.HasKey(e => e.IdModel);

                entity.ToTable("car_models");

                entity.Property(e => e.IdModel).HasColumnName("id_model");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Car_Types>(entity =>
            {
                entity.HasKey(e => e.IdType);

                entity.ToTable("car_types");

                entity.Property(e => e.IdType).HasColumnName("id_type");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User_Cars>(entity =>
            {
                entity.HasKey(e => e.IdCar);

                entity.ToTable("user_cars");

                entity.Property(e => e.IdCar).HasColumnName("id_auto");

                entity.Property(e => e.Createdt)
                    .HasColumnName("createdt")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdModel).HasColumnName("id_model");

                entity.Property(e => e.IdType).HasColumnName("id_type");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.SerialNumber)
                    .HasColumnName("serial_number")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                //entity.HasOne(d => d.IdModelNavigation)
                //    .WithMany(p => p.Automobiles)
                //    .HasForeignKey(d => d.IdModel)
                //    .HasConstraintName("FK_automobiles_auto_models");

                //entity.HasOne(d => d.IdTypeNavigation)
                //    .WithMany(p => p.Automobiles)
                //    .HasForeignKey(d => d.IdType)
                //    .HasConstraintName("FK_automobiles_auto_types");

                //entity.HasOne(d => d.IdUserNavigation)
                //    .WithMany(p => p.Automobiles)
                //    .HasForeignKey(d => d.IdUser)
                //    .HasConstraintName("FK_automobiles_parking_users");
            });

            modelBuilder.Entity<DateTypes>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("date_types");

                entity.Property(e => e.IdDatetype).HasColumnName("id_datetype");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Inspectors>(entity =>
            {
                entity.HasKey(e => e.IdInspector);

                entity.ToTable("inspectors");

                entity.Property(e => e.IdInspector).HasColumnName("id_inspector");

                entity.Property(e => e.Createdt)
                    .HasColumnName("createdt")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdParking).HasColumnName("id_parking");

                entity.Property(e => e.IdZone).HasColumnName("id_zone");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .HasColumnName("surname")
                    .HasMaxLength(50);
            });
            modelBuilder.Entity<ParkingUsers>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.ToTable("parking_users");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.Createdt)
                    .HasColumnName("createdt")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .HasColumnName("surname")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Parkings>(entity =>
            {
                entity.HasKey(e => e.IdParking);

                entity.ToTable("parkings");

                entity.Property(e => e.IdParking).HasColumnName("id_parking");

                entity.Property(e => e.IdZone).HasColumnName("id_zone");

                entity.Property(e => e.Latitude)
                    .HasColumnName("latitude")
                    .HasMaxLength(50);

                entity.Property(e => e.Longitude)
                    .HasColumnName("longitude")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Prices>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("prices");

                entity.Property(e => e.DateType).HasColumnName("date_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdParking).HasColumnName("id_parking");

                entity.Property(e => e.IdZone).HasColumnName("id_zone");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Zones>(entity =>
            {
                entity.HasKey(e => e.IdZone);

                entity.ToTable("zones");

                entity.Property(e => e.IdZone).HasColumnName("id_zone");

                entity.Property(e => e.IdName)
                    .HasColumnName("id_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Latitude)
                    .HasColumnName("latitude")
                    .HasMaxLength(50);

                entity.Property(e => e.Longitude)
                    .HasColumnName("longitude")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
