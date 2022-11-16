using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ExpressionBuilder.Test.Database
{
    public class DbDataContext : DbContext
    {
        public DbSet<Products> Products { get; set; }
        public DbSet<Orders> Orders { get; set; }

        public DbDataContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetails>(entity => { entity.HasKey(e => new { e.OrderID, e.ProductID }); });
            base.OnModelCreating(modelBuilder);
        }
    }

    public class Products
    {
        [Key]
        public int ProductID { get; set; }

        public bool Discontinued { get; set; }
        public decimal UnitPrice { get; set; }
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
        public Categories Categories { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }

    public class Categories
    {
        [Key]
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }
    }

    public class Orders
    {
        [Key]
        public int OrderID { get; set; }

        public string ShipRegion { get; set; }
    }

    public class OrderDetails
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public Orders Orders { get; set; }
        public float Discount { get; set; }
    }
}