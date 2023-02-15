using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebBaraholkaAPI.Models.Db;

namespace WebBaraholkaAPI.DbProvider;

public class DataContext : IdentityDbContext<DbApplicationUser>, IDataProvider
{
    public DbSet<DbFoodProduct> FoodProducts { get; set; }
    public DbSet<DbFoodProductCategory> FoodCategories { get; set; }
    public DbSet<DbConsumedFoodProduct> ConsumedFoodProducts { get; set; }
    public DbSet<DbConsumedFoodProductsRecord> ConsumedFoodProductRecords { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options) {}

    public async Task SaveAsync()
    {
        await SaveChangesAsync();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new DbFoodProductsConfiguration());
        modelBuilder.ApplyConfiguration(new DbFoodCategoriesConfiguration());
        modelBuilder.ApplyConfiguration(new DbApplicationUserConfiguration());
        modelBuilder.ApplyConfiguration(new DbConsumedFoodProductConfiguration());
        modelBuilder.ApplyConfiguration(new DbConsumedFoodProductsRecordsConfiguration());
    }
}
public class DbFoodProductsConfiguration : IEntityTypeConfiguration<DbFoodProduct>
{
    public void Configure(EntityTypeBuilder<DbFoodProduct> builder)
    {
        builder.ToTable(DbFoodProduct.TableName).HasKey(fp => fp.Id);

        builder.Property(fp => fp.Id)
            .IsRequired()
            .HasColumnType("nvarchar(256")
            .HasMaxLength(256);
        builder.Property(fp => fp.Description).HasColumnType("nvarchar(max)");
        builder.Property(fp => fp.Proteins).IsRequired().HasColumnType("decimal(8, 2)");
        builder.Property(fp => fp.Fats).IsRequired().HasColumnType("decimal(8, 2)");
        builder.Property(fp => fp.Carbohydrates).IsRequired().HasColumnType("decimal(8, 2)");
        builder.Property(fp => fp.EnergyValue).IsRequired().HasColumnType("decimal(8, 2)");
        builder.Property(fp => fp.FoodProductCategoryId).IsRequired();
        
        builder.HasOne(fp => fp.FoodProductCategory).WithMany(fc => fc.FoodProducts);
        builder.HasMany(fp => fp.ConsumedFoodProducts).WithOne(cfp => cfp.FoodProduct);
    }
}

public class DbFoodCategoriesConfiguration : IEntityTypeConfiguration<DbFoodProductCategory>
{
    public void Configure(EntityTypeBuilder<DbFoodProductCategory> builder)
    {
        builder.ToTable(DbFoodProductCategory.TableName).HasKey(fp => fp.Id);
        
        builder.Property(fc => fc.Id).IsRequired();
        builder.Property(fc => fc.Description).HasColumnType("nvarchar(max)");
        builder.HasMany(fc => fc.FoodProducts).WithOne(fc => fc.FoodProductCategory);
    }
}

public class DbApplicationUserConfiguration : IEntityTypeConfiguration<DbApplicationUser>
{
    public void Configure(EntityTypeBuilder<DbApplicationUser> builder)
    {
        builder.HasMany(au => au.ConsumedFoodProductsRecords).WithOne(cfpr => cfpr.User);
    }
}

public class DbConsumedFoodProductConfiguration : IEntityTypeConfiguration<DbConsumedFoodProduct>
{
    public void Configure(EntityTypeBuilder<DbConsumedFoodProduct> builder)
    {
        builder.ToTable(DbConsumedFoodProduct.TableName).HasKey(cfp => cfp.Id);
        
        builder.Property(cfp => cfp.Id).IsRequired();
        builder.Property(cfp => cfp.ConsumedMass)
            .IsRequired()
            .HasColumnType("decimal(8, 2)");
        builder.Property(cfp => cfp.FoodProductId)
            .IsRequired()
            .HasColumnType("nvarchar(256")
            .HasMaxLength(256);
        builder.Property(cfp => cfp.ConsumedFoodProductsRecordId).IsRequired();

        builder.HasOne(cfp => cfp.FoodProduct).WithMany(fp => fp.ConsumedFoodProducts);
        builder.HasOne(cfp => cfp.ConsumedFoodProductsRecord).WithMany(fp => fp.ConsumedFoodProducts);
    }
}

public class DbConsumedFoodProductsRecordsConfiguration : IEntityTypeConfiguration<DbConsumedFoodProductsRecord>
{
    public void Configure(EntityTypeBuilder<DbConsumedFoodProductsRecord> builder)
    {
        builder.ToTable(DbConsumedFoodProductsRecord.TableName).HasKey(cfp => cfp.Id);
        
        builder.Property(cfpr => cfpr.Id).IsRequired();
        builder.Property(cfpr => cfpr.RecordingTime).IsRequired().HasColumnType("date");
        builder.Property(cfpr => cfpr.UserId).HasColumnType("nvarchar(450)").IsRequired();

        builder.HasOne(cfpr => cfpr.User).WithMany(u => u.ConsumedFoodProductsRecords);
        builder.HasMany(cfpr => cfpr.ConsumedFoodProducts).WithOne(cfp => cfp.ConsumedFoodProductsRecord);
    }
}