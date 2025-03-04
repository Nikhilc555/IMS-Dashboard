using IMS_Dashboard.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IMS_Dashboard;

public partial class ImsDbContext : DbContext
{
    public ImsDbContext(DbContextOptions<ImsDbContext> options) : base(options) { }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ImportInventory> ImportInventories { get; set; }

    public virtual DbSet<ShipmentDetail> ShipmentDetails { get; set; }

    public virtual DbSet<SystemUser> SystemUsers { get; set; }

    public virtual DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }
    public virtual DbSet<Suppliers> Suppliers { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=DESKTOP-8IC7CPL;Database=ims_db;User Id=sa1;Password=office;Trusted_Connection=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Category>(entity =>
        //{
        //    entity.ToTable("Category");

        //    entity.Property(e => e.Id).HasColumnName("id");
        //    entity.Property(e => e.CategoryName)
        //        .HasMaxLength(50)
        //        .IsUnicode(false)
        //        .HasColumnName("category_name");
        //});

        //modelBuilder.Entity<Manufacturer>(entity =>
        //{
        //    entity.Property(e => e.Id).HasColumnName("id");
        //    entity.Property(e => e.ManufacturerName)
        //        .HasMaxLength(100)
        //        .IsUnicode(false)
        //        .HasColumnName("manufacturer_name");
        //});

        //modelBuilder.Entity<Product>(entity =>
        //{
        //    entity.Property(e => e.Id).HasColumnName("id");
        //    entity.Property(e => e.Category).HasColumnName("category");
        //    entity.Property(e => e.Description)
        //        .HasMaxLength(200)
        //        .IsUnicode(false)
        //        .HasColumnName("description");
        //    entity.Property(e => e.IsActive).HasColumnName("is_active");
        //    entity.Property(e => e.Manufacurer).HasColumnName("manufacurer");
        //    entity.Property(e => e.Price)
        //        .HasColumnType("decimal(18, 2)")
        //        .HasColumnName("price");
        //    entity.Property(e => e.ProductCode)
        //        .HasMaxLength(20)
        //        .IsUnicode(false)
        //        .HasColumnName("product_code");
        //    entity.Property(e => e.ProductName)
        //        .HasMaxLength(100)
        //        .IsUnicode(false)
        //        .HasColumnName("product_name");
        //    entity.Property(e => e.Uom).HasColumnName("uom");
        //});

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Role1)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("role");
        });

        //modelBuilder.Entity<ShipmentDetail>(entity =>
        //{
        //    entity.ToTable("shipment_details");

        //    entity.Property(e => e.Id).HasColumnName("id");
        //    entity.Property(e => e.CreatedBy).HasColumnName("created_by");
        //    entity.Property(e => e.CreatedOn)
        //        .HasColumnType("datetime")
        //        .HasColumnName("created_on");
        //    entity.Property(e => e.CtnNo).HasColumnName("ctn_no");
        //    entity.Property(e => e.ProductId).HasColumnName("product_id");
        //    entity.Property(e => e.Qty).HasColumnName("qty");
        //    entity.Property(e => e.ShippingMark)
        //        .HasMaxLength(50)
        //        .IsUnicode(false)
        //        .HasColumnName("shipping_mark");
        //    entity.Property(e => e.Weight)
        //        .HasColumnType("decimal(18, 2)")
        //        .HasColumnName("weight");
        //});

        modelBuilder.Entity<SystemUser>(entity =>
        {
            entity.ToTable("system_users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("created_on");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("datetime")
                .HasColumnName("updated_on");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.NameOfUser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name_of_user");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_name");
        });
        //modelBuilder.Entity<UnitOfMeasure>(entity =>
        //{
        //    entity.ToTable("Unit_of_Measure");

        //    entity.Property(e => e.Id).HasColumnName("id");
        //    entity.Property(e => e.Uom)
        //        .HasMaxLength(50)
        //        .IsUnicode(false)
        //        .HasColumnName("uom");
        //});

        //OnModelCreatingPartial(modelBuilder);
    }

    //    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
