using AuctionApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionApp.API.Data
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options)
        {
        }

        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de relaciones
            modelBuilder.Entity<Auction>()
                .HasOne(a => a.Seller)
                .WithMany(u => u.Auctions)
                .HasForeignKey(a => a.SellerId);

            modelBuilder.Entity<Auction>()
                .HasOne(a => a.WinningBid)
                .WithOne()
                .HasForeignKey<Auction>(a => a.WinningBidId)
                .IsRequired(false);

            // Configuración de tipos de columna para MySQL
            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .HasColumnType("varchar(255)");

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasColumnType("varchar(255)");

            modelBuilder.Entity<Auction>()
                .Property(a => a.Title)
                .HasColumnType("varchar(255)");

            modelBuilder.Entity<Auction>()
                .Property(a => a.Description)
                .HasColumnType("text");

            modelBuilder.Entity<Auction>()
                .Property(a => a.ImageUrl)
                .HasColumnType("varchar(255)");

            // Datos de ejemplo
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "usuario1", Email = "usuario1@example.com" },
                new User { Id = 2, Username = "usuario2", Email = "usuario2@example.com" }
            );
            
            // Agregar subastas de ejemplo
            modelBuilder.Entity<Auction>().HasData(
                new Auction
                {
                    Id = 1,
                    Title = "iPhone 13 Pro Max - Como nuevo",
                    Description = "Teléfono en perfecto estado, solo 6 meses de uso. Incluye cargador original, auriculares y caja.",
                    ImageUrl = "https://images.unsplash.com/photo-1632661674596-df8be070a5c5",
                    StartingPrice = 699.99M,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(7),
                    SellerId = 1
                },
                new Auction
                {
                    Id = 2,
                    Title = "PlayStation 5 Digital Edition",
                    Description = "Consola PS5 Digital Edition en perfecto estado. Incluye 2 mandos DualSense y 3 juegos digitales.",
                    ImageUrl = "https://images.unsplash.com/photo-1606813907291-d86efa9b94db",
                    StartingPrice = 450M,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(5),
                    SellerId = 1
                },
                new Auction
                {
                    Id = 3,
                    Title = "Bicicleta de montaña Trek Marlin 7",
                    Description = "Bicicleta de montaña Trek Marlin 7 talla M. Poco uso, en excelentes condiciones. Ideal para rutas de montaña.",
                    ImageUrl = "https://images.unsplash.com/photo-1485965120184-e220f721d03e",
                    StartingPrice = 850M,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(10),
                    SellerId = 2
                }
            );
        }
    }
}