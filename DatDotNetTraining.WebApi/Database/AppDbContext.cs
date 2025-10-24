using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatDotNetTraining.WebApi.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Tbl_Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var sb = new SqlConnectionStringBuilder()
                {
                    DataSource = ".",
                    InitialCatalog = "DAT_Dev",
                    UserID = "sa",
                    Password = "sasa@123",
                    TrustServerCertificate = true
                };
                optionsBuilder.UseSqlServer(sb.ConnectionString);
            }
        }
    }

    [Table("Tbl_Product")]
    public class Tbl_Product
    {
        [Key]
        [Column("ProductId")]
        public int Id { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsDelete { get; set; }
    }
}
