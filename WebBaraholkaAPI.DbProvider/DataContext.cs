using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebBaraholkaAPI.Models.Db;

namespace WebBaraholkaAPI.DbProvider;

public class DataContext : DbContext, IDataProvider
{
    public DbSet<DbFoodProduct> FoodProducts { get; set; }
    
    public DataContext(DbContextOptions<DataContext> options) : base(options) {}

    public async Task SaveAsync()
    {
        await SaveChangesAsync();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DbFoodProductsConfiguration());
        modelBuilder.ApplyConfiguration(new DbFoodCategoriesConfiguration());
    }
}
public class DbFoodProductsConfiguration : IEntityTypeConfiguration<DbFoodProduct>
{
    public void Configure(EntityTypeBuilder<DbFoodProduct> builder)
    {
        builder.ToTable(DbFoodProduct.TableName).HasKey(fp => fp.Id);

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
        builder.Property(fp => fp.FoodCategoryId).IsRequired();
        builder.HasOne(fp => fp.FoodCategory).WithMany(fc => fc.FoodProducts);
    }
}

public class DbFoodCategoriesConfiguration : IEntityTypeConfiguration<DbFoodCategory>
{
    public void Configure(EntityTypeBuilder<DbFoodCategory> builder)
    {
        builder.ToTable(DbFoodCategory.TableName).HasKey(fp => fp.Id);
        
        builder.Property(fp => fp.Id).IsRequired();
        builder.Property(fp => fp.Description).HasColumnType("nvarchar(max)");
        builder.HasMany(fp => fp.FoodProducts).WithOne(fc => fc.FoodCategory);
    }
}