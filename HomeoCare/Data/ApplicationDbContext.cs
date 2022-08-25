using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HomeoCare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HomeoCare.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Move Identity to "Auth" Schema:
            modelBuilder.Entity<ApplicationUser>().Ignore(c => c.Email).Ignore(c => c.NormalizedEmail)
                .Ignore(c => c.PhoneNumber).Ignore(c => c.PhoneNumberConfirmed)
                .ToTable("TBL_UserIdentity", "Auth")
                .Property(b => b.Id).HasColumnType("uniqueidentifier").HasColumnName("UserID");


            modelBuilder.Entity<IdentityRole<Guid>>().ToTable("TBD_Roles", "Auth");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("TBR_UserRoles", "Auth")
                .Property(b => b.UserId).HasColumnName("UserID").HasColumnType("uniqueidentifier");

            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("TBR_RoleBasedClaims", "Auth");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("TBR_UserBasedClaims", "Auth")
                .Property(b => b.UserId).HasColumnName("UserID").HasColumnType("uniqueidentifier");

            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("TBR_ExternalLogins", "Auth")
                .Property(b => b.UserId).HasColumnName("UserID").HasColumnType("uniqueidentifier");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("TBR_ExternalLoginTokens", "Auth")
                .Property(b => b.UserId).HasColumnName("UserID").HasColumnType("uniqueidentifier");

            modelBuilder.Entity<ShoppingCart>().Property(p => p.RowVersion).IsConcurrencyToken().ValueGeneratedOnAddOrUpdate();

            this.SeedCategory(modelBuilder);
            this.SeedProduct(modelBuilder);
        }
        private void SeedCategory(ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(
                        new Category { Id = 2, Name = "Category 2", DisplayOrder = 2 },
                        new Category { Id = 3, Name = "Category 3", DisplayOrder = 3 }
                        );
        }


        private void SeedProduct(ModelBuilder builder)
        {

            Product product = new Product()
            {
                ProductID = 1,
                ProductName = "Hydration Lotion",
                CategoryId = 1,
                Description = "Looking after your skins health is of utmost importance because extra effort always shows, even on your skin. Thats why you should pay extra attention to what you choose for your skin. Introducing the Vaseline Ice cool Hydration lotion which rapidly cools skin by -3 degree C.",
                ShortDesc = "his range also consists of the Vaseline with Body Ice Cream 165g which is a gel crme.",
                ImagePath = "04e4c6e4-acd4-4f06-8d83-0b0b5370a01f.jfif",
                SellingPrice = 44,
                Quantity = 10,
                QuantityOnHand = 10
            };

            Product product2 = new Product()
            {
                ProductID = 2,
                ProductName = "Bio Cucumber",
                CategoryId = 1,
                Description = "Say goodbye to tired and puffy skin as you gently apply this pore tightening toner from Biotique. It combines the cooling effect.",
                ShortDesc = "Cucumber, with its cooling property, helps to reduce skin puffiness and tiredness so as to restore.",
                ImagePath = "619932b1-132a-40bf-8455-bd913123dd29.jfif",
                SellingPrice = 32,
                Quantity = 10,
                QuantityOnHand = 10
            };


            Product product3 = new Product()
            {
                ProductID = 3,
                ProductName = "Body Lotion",
                CategoryId = 2,
                Description = "Smooth skin begins with the NIVEA Aloe Hydration Body Lotion. Ideal for all skin types, this lotion is mild enough to be used daily. Though it is primarily designed for use on the body.",
                ShortDesc = "Enriched with aloe vera extracts, this alcohol-free lotion soothes dry and irritated skin, and nourishes.",
                ImagePath = "88a12012-645b-4254-b5d2-a9f541f5200b.jfif",
                SellingPrice = 56,
                Quantity = 10,
                QuantityOnHand = 10
            };

            Product product4 = new Product()
            {
                ProductID = 4,
                ProductName = "Papaya Exfoliating",
                CategoryId = 2,
                Description = "Do you suffer from lifeless and lifeless skin? If yes, this rejuvenating face wash gel by Biotique is the face wash that will help you. This face wash gel combines groundnut, papaya and neem to give your skin the cleaning and nourishment it needs",
                ShortDesc = "This naturally tones your skin and leaves it supple, soft and moisturized to ensure your skin has a natural glow.",
                ImagePath = "3fe85a01-7d19-4e3d-b3fc-e037b27a4296.jfif",
                SellingPrice = 74,
                Quantity = 10,
                QuantityOnHand = 10
            };

            Product product5 = new Product()
            {
                ProductID = 5,
                ProductName = "BIOTIQUE BIO Fruit BIO Fruit",
                CategoryId = 3,
                Description = "Let the goodness of natural ingredients, such as Fruits and Multani Mitti, of the Biotique BIO Fruit Whitening & Depigmentation Face Pack lighten your skin and give you flawless charm. By adding this pack to your regular skincare routine, you can get smooth and soft skin with a youthful texture.",
                ShortDesc = "Enriched with nutrients and essential vitamins, fruits give you bright and beautiful skin.",
                ImagePath = "5c52cf14-43b5-4079-9963-223c5e6fdfc8.jfif",
                SellingPrice = 68,
                Quantity = 10,
                QuantityOnHand = 10
            };


            Product product6 = new Product()
            {
                ProductID = 6,
                ProductName = "Fairness Cream",
                CategoryId = 3,
                Description = "A brainchild of a chemist, Olay was born out of love and was Graham Wulff’s gift to his doting wife. Graham wanted to create a new beauty product that would leave his wife’s skin nourished and moisturized without leaving a greasy feel. Graham and Dinah started an extensive research and finally created the legendary Oil of Olay Beauty Fluid.",
                ShortDesc = "This cream works wonders on your skin as a fairness cream, spot remover, moisturizer.",
                ImagePath = "9547f45c-1a08-4ac8-9890-7bf54600575a.jfif",
                SellingPrice = 95,
                Quantity = 10,
                QuantityOnHand = 10
            };


            Product product7 = new Product()
            {
                ProductID = 7,
                ProductName = "test",
                CategoryId = 1,
                Description = "A brainchild of a chemist, Olay was born out of love and was Graham Wulff’s gift to his doting wife. Graham wanted to create a new beauty product that would leave his wife’s skin nourished and moisturized without leaving a greasy feel. Graham and Dinah started an extensive research and finally created the legendary Oil of Olay Beauty Fluid.",
                ShortDesc = "This cream works wonders on your skin as a fairness cream, spot remover, moisturizer.",
                ImagePath = "5453d81e-0270-4097-ab30-791cb0ad5023.jpg",
                SellingPrice = 49,
                Quantity = 10,
                QuantityOnHand = 10
            };

            Product product8 = new Product()
            {
                ProductID = 8,
                ProductName = "test 2",
                CategoryId = 2,
                Description = "A brainchild of a chemist, Olay was born out of love and was Graham Wulff’s gift to his doting wife. Graham wanted to create a new beauty product that would leave his wife’s skin nourished and moisturized without leaving a greasy feel. Graham and Dinah started an extensive research and finally created the legendary Oil of Olay Beauty Fluid.",
                ShortDesc = "This cream works wonders on your skin as a fairness cream, spot remover, moisturizer.",
                ImagePath = "f17c0590-9d20-4263-a7c0-ed7b10fef8fe.png",
                SellingPrice = 52,
                Quantity = 10,
                QuantityOnHand = 10
            };

            builder.Entity<Product>().HasData(product, product2, product3, product4, product5, product6, product7, product8);
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderList> OrderList { get; set; }
        public DbSet<PaymentsDetails> PaymentsDetails { get; set; }
        public DbSet<AddressDetails> AddressDetails { get; set; }
        public DbSet<PersonalDetails> PersonalDetails { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
    }



}
