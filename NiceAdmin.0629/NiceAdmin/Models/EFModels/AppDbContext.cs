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
		public virtual DbSet<DessertImage> DessertImages { get; set; }
		public virtual DbSet<DessertOrderDetail> DessertOrderDetails { get; set; }
		public virtual DbSet<DessertOrder> DessertOrders { get; set; }
		public virtual DbSet<Dessert> Desserts { get; set; }
		public virtual DbSet<DessertTag> DessertTags { get; set; }
		public virtual DbSet<DiscountGroupItem> DiscountGroupItems { get; set; }
		public virtual DbSet<DiscountGroup> DiscountGroups { get; set; }
		public virtual DbSet<Discount> Discounts { get; set; }
		public virtual DbSet<Employee> Employees { get; set; }
		public virtual DbSet<LessonCategory> LessonCategories { get; set; }
		public virtual DbSet<LessonImage> LessonImages { get; set; }
		public virtual DbSet<LessonOrderDetail> LessonOrderDetails { get; set; }
		public virtual DbSet<LessonOrder> LessonOrders { get; set; }
		public virtual DbSet<Lesson> Lessons { get; set; }
		public virtual DbSet<LessonTag> LessonTags { get; set; }
		public virtual DbSet<MemberCoupon> MemberCoupons { get; set; }
		public virtual DbSet<Member> Members { get; set; }
		public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
		public virtual DbSet<Permission> Permissions { get; set; }
		public virtual DbSet<Promotion> Promotions { get; set; }
		public virtual DbSet<Role> Roles { get; set; }
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

			modelBuilder.Entity<Employee>()
				.HasMany(e => e.Roles)
				.WithMany(e => e.Employees)
				.Map(m => m.ToTable("EmployeeToRoles").MapLeftKey("EmployeeId").MapRightKey("RoleId"));

			modelBuilder.Entity<LessonCategory>()
				.HasMany(e => e.Lessons)
				.WithRequired(e => e.LessonCategory)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<LessonOrder>()
				.HasMany(e => e.LessonOrderDetails)
				.WithRequired(e => e.LessonOrder)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Lesson>()
				.HasMany(e => e.LessonImages)
				.WithRequired(e => e.Lesson)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Lesson>()
				.HasMany(e => e.LessonOrderDetails)
				.WithRequired(e => e.Lesson)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Lesson>()
				.HasMany(e => e.LessonTags)
				.WithRequired(e => e.Lesson)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.Property(e => e.Phone)
				.IsUnicode(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.DessertOrders)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.LessonOrders)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.MemberCoupons)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<OrderStatus>()
				.HasMany(e => e.DessertOrders)
				.WithRequired(e => e.OrderStatus)
				.HasForeignKey(e => e.DessertOrderStatusId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<OrderStatus>()
				.HasMany(e => e.LessonOrders)
				.WithRequired(e => e.OrderStatus)
				.HasForeignKey(e => e.LessonOrderStatusId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Permission>()
				.HasMany(e => e.Roles)
				.WithMany(e => e.Permissions)
				.Map(m => m.ToTable("RoleToPermissions").MapLeftKey("PermissionId").MapRightKey("RoleId"));

			modelBuilder.Entity<Teacher>()
				.HasMany(e => e.Lessons)
				.WithRequired(e => e.Teacher)
				.WillCascadeOnDelete(false);
		}

        public System.Data.Entity.DbSet<NiceAdmin.Models.ViewModels.TeachersVM.UpdateTeacherStatusVM> UpdateTeacherStatusVMs { get; set; }
    }
}
