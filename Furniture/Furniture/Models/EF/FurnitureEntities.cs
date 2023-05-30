using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Furniture.Models.EF
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public partial class FurnitureEntities : IdentityDbContext<ApplicationUser>
    {
        public FurnitureEntities()
            : base("name=FurnitureEntities", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Order_tb> Order_tb { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<News>()
                .Property(e => e.img)
                .IsUnicode(false);

            modelBuilder.Entity<Order_tb>()
                .HasMany(e => e.OrderDetails)
                .WithOptional(e => e.Order_tb)
                .HasForeignKey(e => e.OrderID);
            Database.SetInitializer<FurnitureEntities>(null);
            base.OnModelCreating(modelBuilder);
                
        }
        public static FurnitureEntities Create()
        {
            return new FurnitureEntities();
        }
    }
}
