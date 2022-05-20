using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebBaraholkaAPI.Models.Db;

namespace WebBaraholkaAPI.DbProvider;

public class DataContext : DbContext, IDataProvider
{
    public DbSet<DbFoodProduct> FoodProducts { get; set; }
    
    public DataContext(DbContextOptions<DataContext> options) : base(options) {}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DbDepartmentConfiguration());
    }
}
public class DbDepartmentConfiguration : IEntityTypeConfiguration<DbFoodProduct>
{
    public void Configure(EntityTypeBuilder<DbFoodProduct> builder)
    {
        builder.ToTable("FoodProducts").HasKey(fp => fp.Id);

        builder.Property(fp => fp.Id).IsRequired();
        builder.Property(fp => fp.Name)
            .IsRequired()
            .HasColumnType("nvarchar(256")
            .HasMaxLength(256);
        builder.Property(fp => fp.Description).HasColumnType("nvarchar(max)");
        builder.Property(fp => fp.Proteins).IsRequired().HasColumnType("decimal(8, 2)");
        builder.Property(fp => fp.Fats).IsRequired().HasColumnType("decimal(8, 2)");
        builder.Property(fp => fp.Carbohydrates).IsRequired().HasColumnType("decimal(8, 2)");
        builder.Property(fp => fp.EnergyValue).IsRequired().HasColumnType("decimal(8, 2)");
    }
}