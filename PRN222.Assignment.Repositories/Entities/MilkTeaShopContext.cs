using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PRN222.Assignment.Repositories.Entities;

public partial class MilkTeaShopContext : DbContext
{
    public MilkTeaShopContext()
    {
    }

    public MilkTeaShopContext(DbContextOptions<MilkTeaShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Combo> Combos { get; set; }

    public virtual DbSet<ComboItem> ComboItems { get; set; }

    public virtual DbSet<MilkTeaProduct> MilkTeaProducts { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderCombo> OrderCombos { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<OrderItemTopping> OrderItemToppings { get; set; }

    public virtual DbSet<ProductSize> ProductSizes { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    public virtual DbSet<Topping> Toppings { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    private string GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
        return configuration["ConnectionStrings:DefaultConnection"];
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GetConnectionString());
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__46A222CD0350A74D");

            entity.ToTable("Account");

            entity.HasIndex(e => e.Email, "UQ_Account_Email").IsUnique();

            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("first_name");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("role");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__D54EE9B47B0FEDBE");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .HasColumnName("category_name");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
        });

        modelBuilder.Entity<Combo>(entity =>
        {
            entity.HasKey(e => e.ComboId).HasName("PK__Combo__18F74AA31CC2737D");

            entity.ToTable("Combo");

            entity.Property(e => e.ComboId).HasColumnName("combo_id");
            entity.Property(e => e.ComboName)
                .HasMaxLength(100)
                .HasColumnName("combo_name");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(255)
                .HasColumnName("image_path");
            entity.Property(e => e.IsAvailable)
                .HasDefaultValue(true)
                .HasColumnName("is_available");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("money")
                .HasColumnName("total_price");
        });

        modelBuilder.Entity<ComboItem>(entity =>
        {
            entity.HasKey(e => e.ComboItemId).HasName("PK__ComboIte__A1CBC087774AB7ED");

            entity.ToTable("ComboItem");

            entity.HasIndex(e => new { e.ComboId, e.ProductSizeId }, "UQ_ComboItem").IsUnique();

            entity.Property(e => e.ComboItemId).HasColumnName("combo_item_id");
            entity.Property(e => e.ComboId).HasColumnName("combo_id");
            entity.Property(e => e.ProductSizeId).HasColumnName("product_size_id");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");

            entity.HasOne(d => d.Combo).WithMany(p => p.ComboItems)
                .HasForeignKey(d => d.ComboId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ComboItem__combo__6E01572D");

            entity.HasOne(d => d.ProductSize).WithMany(p => p.ComboItems)
                .HasForeignKey(d => d.ProductSizeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ComboItem__produ__6EF57B66");
        });

        modelBuilder.Entity<MilkTeaProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__MilkTeaP__47027DF5C2732F48");

            entity.ToTable("MilkTeaProduct");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.BasePrice)
                .HasColumnType("money")
                .HasColumnName("base_price");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(255)
                .HasColumnName("image_path");
            entity.Property(e => e.IsAvailable)
                .HasDefaultValue(true)
                .HasColumnName("is_available");
            entity.Property(e => e.LastModified)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("last_modified");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .HasColumnName("product_name");

            entity.HasOne(d => d.Category).WithMany(p => p.MilkTeaProducts)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MilkTeaPr__categ__3F466844");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order__465962296D3DE133");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.DeliveryAddress)
                .HasMaxLength(255)
                .HasColumnName("delivery_address");
            entity.Property(e => e.DeliveryFee)
                .HasColumnType("money")
                .HasColumnName("delivery_fee");
            entity.Property(e => e.Notes)
                .HasMaxLength(500)
                .HasColumnName("notes");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("order_date");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(20)
                .HasColumnName("payment_method");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .HasDefaultValue("Pending")
                .HasColumnName("payment_status");
            entity.Property(e => e.ProcessedBy).HasColumnName("processed_by");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending")
                .HasColumnName("status");
            entity.Property(e => e.Subtotal)
                .HasColumnType("money")
                .HasColumnName("subtotal");
            entity.Property(e => e.Tax)
                .HasColumnType("money")
                .HasColumnName("tax");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("money")
                .HasColumnName("total_amount");

            entity.HasOne(d => d.Account).WithMany(p => p.OrderAccounts)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__account_i__4F7CD00D");

            entity.HasOne(d => d.ProcessedByNavigation).WithMany(p => p.OrderProcessedByNavigations)
                .HasForeignKey(d => d.ProcessedBy)
                .HasConstraintName("FK__Order__processed__571DF1D5");
        });

        modelBuilder.Entity<OrderCombo>(entity =>
        {
            entity.HasKey(e => e.OrderComboId).HasName("PK__OrderCom__34BF1CD7025A823B");

            entity.ToTable("OrderCombo");

            entity.HasIndex(e => new { e.OrderId, e.ComboId }, "UQ_OrderCombo").IsUnique();

            entity.Property(e => e.OrderComboId).HasColumnName("order_combo_id");
            entity.Property(e => e.ComboId).HasColumnName("combo_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("money")
                .HasColumnName("unit_price");

            entity.HasOne(d => d.Combo).WithMany(p => p.OrderCombos)
                .HasForeignKey(d => d.ComboId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderComb__combo__74AE54BC");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderCombos)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderComb__order__73BA3083");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__OrderIte__3764B6BC1342B8F4");

            entity.ToTable("OrderItem");

            entity.Property(e => e.OrderItemId).HasColumnName("order_item_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductSizeId).HasColumnName("product_size_id");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");
            entity.Property(e => e.SpecialInstructions)
                .HasMaxLength(255)
                .HasColumnName("special_instructions");
            entity.Property(e => e.Subtotal)
                .HasComputedColumnSql("([quantity]*[unit_price])", true)
                .HasColumnType("money")
                .HasColumnName("subtotal");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("money")
                .HasColumnName("unit_price");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__order__59FA5E80");

            entity.HasOne(d => d.ProductSize).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductSizeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__produ__5AEE82B9");
        });

        modelBuilder.Entity<OrderItemTopping>(entity =>
        {
            entity.HasKey(e => e.OrderItemToppingId).HasName("PK__OrderIte__7DCEF8150734AD82");

            entity.ToTable("OrderItemTopping");

            entity.HasIndex(e => new { e.OrderItemId, e.ToppingId }, "UQ_OrderItemTopping").IsUnique();

            entity.Property(e => e.OrderItemToppingId).HasColumnName("order_item_topping_id");
            entity.Property(e => e.OrderItemId).HasColumnName("order_item_id");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            entity.Property(e => e.ToppingId).HasColumnName("topping_id");

            entity.HasOne(d => d.OrderItem).WithMany(p => p.OrderItemToppings)
                .HasForeignKey(d => d.OrderItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__order__5FB337D6");

            entity.HasOne(d => d.Topping).WithMany(p => p.OrderItemToppings)
                .HasForeignKey(d => d.ToppingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__toppi__60A75C0F");
        });

        modelBuilder.Entity<ProductSize>(entity =>
        {
            entity.HasKey(e => e.ProductSizeId).HasName("PK__ProductS__062A9A6824D3EABB");

            entity.ToTable("ProductSize");

            entity.HasIndex(e => new { e.ProductId, e.SizeId }, "UQ_ProductSize").IsUnique();

            entity.Property(e => e.ProductSizeId).HasColumnName("product_size_id");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.SizeId).HasColumnName("size_id");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductSizes)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductSi__produ__48CFD27E");

            entity.HasOne(d => d.Size).WithMany(p => p.ProductSizes)
                .HasForeignKey(d => d.SizeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductSi__size___49C3F6B7");
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => e.SizeId).HasName("PK__Size__0DCACE3106488F58");

            entity.ToTable("Size");

            entity.Property(e => e.SizeId).HasColumnName("size_id");
            entity.Property(e => e.PriceModifier)
                .HasColumnType("money")
                .HasColumnName("price_modifier");
            entity.Property(e => e.SizeName)
                .HasMaxLength(20)
                .HasColumnName("size_name");
        });

        modelBuilder.Entity<Topping>(entity =>
        {
            entity.HasKey(e => e.ToppingId).HasName("PK__Topping__141E1E060DF4A1D3");

            entity.ToTable("Topping");

            entity.Property(e => e.ToppingId).HasColumnName("topping_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(255)
                .HasColumnName("image_path");
            entity.Property(e => e.IsAvailable)
                .HasDefaultValue(true)
                .HasColumnName("is_available");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            entity.Property(e => e.ToppingName)
                .HasMaxLength(50)
                .HasColumnName("topping_name");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__85C600AF8DBB1DF1");

            entity.ToTable("Transaction");

            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");
            entity.Property(e => e.Amount)
                .HasColumnType("money")
                .HasColumnName("amount");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProcessedBy).HasColumnName("processed_by");
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("transaction_date");
            entity.Property(e => e.TransactionType)
                .HasMaxLength(20)
                .HasColumnName("transaction_type");

            entity.HasOne(d => d.Order).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_Transaction_Order");

            entity.HasOne(d => d.ProcessedByNavigation).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.ProcessedBy)
                .HasConstraintName("FK_Transaction_ProcessedBy");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
