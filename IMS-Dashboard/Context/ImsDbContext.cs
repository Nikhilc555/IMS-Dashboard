using Microsoft.Data.SqlClient;
using System.Data;

namespace IMS_Dashboard;

public partial class ImsDbContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    public ImsDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("IMSDb");
    }
    public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
//    public ImsDbContext(DbContextOptions<ImsDbContext> options)
//        : base(options)
//    {
//    }

//    public virtual DbSet<Category> Categories { get; set; }

//    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

//    public virtual DbSet<Product> Products { get; set; }

//    public virtual DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=TEKCSZ-NB122;Initial Catalog=ims_db;Integrated Security=True;TrustServerCertificate=True;");

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Category>(entity =>
//        {
//            entity.ToTable("Category");

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.CategoryName)
//                .HasMaxLength(50)
//                .IsUnicode(false)
//                .HasColumnName("category_name");
//        });

//        modelBuilder.Entity<Manufacturer>(entity =>
//        {
//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.ManufacturerName)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("manufacturer_name");
//        });

//        modelBuilder.Entity<Product>(entity =>
//        {
//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.Category).HasColumnName("category");
//            entity.Property(e => e.Description)
//                .HasMaxLength(200)
//                .IsUnicode(false)
//                .HasColumnName("description");
//            entity.Property(e => e.IsActive).HasColumnName("is_active");
//            entity.Property(e => e.Manufacurer).HasColumnName("manufacurer");
//            entity.Property(e => e.Price)
//                .HasColumnType("decimal(18, 2)")
//                .HasColumnName("price");
//            entity.Property(e => e.ProductCode)
//                .HasMaxLength(20)
//                .IsUnicode(false)
//                .HasColumnName("product_code");
//            entity.Property(e => e.ProductName)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("product_name");
//            entity.Property(e => e.Uom).HasColumnName("uom");
//        });

//        modelBuilder.Entity<UnitOfMeasure>(entity =>
//        {
//            entity.ToTable("Unit_of_Measure");

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.Uom)
//                .HasMaxLength(50)
//                .IsUnicode(false)
//                .HasColumnName("uom");
//        });

//        OnModelCreatingPartial(modelBuilder);
//    }

//    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
