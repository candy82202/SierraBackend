using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace NiceAdmin.Models.EFModels
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
            : base("name=AppDbContext")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CouponCategory> CouponCategories { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<CourseImage> CourseImages { get; set; }
        public virtual DbSet<CourseOrderDetail> CourseOrderDetails { get; set; }
        public virtual DbSet<CourseOrder> CourseOrders { get; set; }
        public virtual DbSet<Cours> Courses { get; set; }
        public virtual DbSet<CoursesCategory> CoursesCategories { get; set; }
        public virtual DbSet<CourseTag> CourseTags { get; set; }
        public virtual DbSet<DessertImage> DessertImages { get; set; }
        public virtual DbSet<DessertOrderDetail> DessertOrderDetails { get; set; }
        public virtual DbSet<DessertOrder> DessertOrders { get; set; }
        public virtual DbSet<Dessert> Desserts { get; set; }
        public virtual DbSet<DessertTag> DessertTags { get; set; }
        public virtual DbSet<DiscountGroupItem> DiscountGroupItems { get; set; }
        public virtual DbSet<DiscountGroup> DiscountGroups { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<MemberCoupon> MemberCoupons { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<OrderStatu> OrderStatus { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }
        public virtual DbSet<Specification> Specifications { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Desserts)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CouponCategory>()
                .HasMany(e => e.Coupons)
                .WithRequired(e => e.CouponCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Coupon>()
                .HasMany(e => e.MemberCoupons)
                .WithRequired(e => e.Coupon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseOrder>()
                .HasMany(e => e.CourseOrderDetails)
                .WithRequired(e => e.CourseOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cours>()
                .HasMany(e => e.CourseImages)
                .WithRequired(e => e.Cours)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cours>()
                .HasMany(e => e.CourseOrderDetails)
                .WithRequired(e => e.Cours)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cours>()
                .HasMany(e => e.CourseTags)
                .WithRequired(e => e.Cours)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoursesCategory>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.CoursesCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DessertOrder>()
                .HasMany(e => e.DessertOrderDetails)
                .WithRequired(e => e.DessertOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dessert>()
                .HasMany(e => e.DessertOrderDetails)
                .WithRequired(e => e.Dessert)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dessert>()
                .HasMany(e => e.DiscountGroupItems)
                .WithRequired(e => e.Dessert)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DiscountGroup>()
                .HasMany(e => e.DiscountGroupItems)
                .WithRequired(e => e.DiscountGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.CourseOrders)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.DessertOrders)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.MemberCoupons)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderStatu>()
                .HasMany(e => e.CourseOrders)
                .WithRequired(e => e.OrderStatu)
                .HasForeignKey(e => e.CourseOrderStatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderStatu>()
                .HasMany(e => e.DessertOrders)
                .WithRequired(e => e.OrderStatu)
                .HasForeignKey(e => e.DessertOrderStatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Permission>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.Permission)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Teacher>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.Teacher)
                .WillCascadeOnDelete(false);
        }
    }
}
