using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TashanSofrasi.EntityLayer.Entities;

namespace TashanSofrasi.DataAccessLayer.Concrete
{
    public class TashanSofrasiContext : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($"Server=(localdb)\\MSSQLLocalDB; Database=TashanSofrasiDB; TrustServerCertificate=True");
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>().ToTable(tb => tb.HasTrigger("trg_BookingNotif")); //Bildirim gönderme için triggerın EF ile uyumlu çalışmasını sağlama işlemi (Rezervasyon)
			modelBuilder.Entity<Contact>().ToTable(tb => tb.HasTrigger("trg_ContactNotif"));
            modelBuilder.Entity<Order>().ToTable(tb => tb.HasTrigger("SumCashRegisters"));
            modelBuilder.Entity<Order>().ToTable(tb => tb.HasTrigger("trg_PreventMultipleOrdersForSameTable"));
            modelBuilder.Entity<Order>().ToTable(tb => tb.HasTrigger("UpdateMenuTableStatusAfterInsert"));
            modelBuilder.Entity<Order>().ToTable(tb => tb.HasTrigger("UpdateMenuTableStatusAfterUpdate"));
            modelBuilder.Entity<Order>().ToTable(tb => tb.HasTrigger("trg_OrderNotif"));
            modelBuilder.Entity<OrderDetail>().ToTable(tb => tb.HasTrigger("SetOrderTotalPrice"));
            modelBuilder.Entity<OrderDetail>().ToTable(tb => tb.HasTrigger("trg_SetUnitPriceOnInsertOrUpdate"));
            modelBuilder.Entity<IdentityUserLogin<int>>().HasKey(x => new { x.LoginProvider, x.ProviderKey });
            modelBuilder.Entity<IdentityUserRole<int>>().HasKey(userRole => new { userRole.UserId, userRole.RoleId });
            modelBuilder.Entity<IdentityUserToken<int>>().HasKey(token => new { token.UserId, token.LoginProvider, token.Name });

        }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Footer> Footers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<CashRegister> CashRegisters { get; set; }
        public DbSet<MenuTable> MenuTables { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}
