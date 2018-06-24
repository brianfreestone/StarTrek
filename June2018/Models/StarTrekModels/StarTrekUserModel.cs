namespace June2018.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class StarTrekUserModel : DbContext
    {
        public StarTrekUserModel()
            : base("name=StarTrekUserDBContext")
        {
        }

        public virtual DbSet<StarTrekProduction> StarTrekProductions { get; set; }
        public virtual DbSet<StarTrekUserData> StarTrekUserDatas { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StarTrekProduction>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<StarTrekProduction>()
                .Property(e => e.Episode)
                .IsUnicode(false);

            modelBuilder.Entity<StarTrekProduction>()
                .Property(e => e.ProdNum)
                .IsUnicode(false);

            modelBuilder.Entity<StarTrekProduction>()
                .Property(e => e.StarDate)
                .IsUnicode(false);

            modelBuilder.Entity<StarTrekProduction>()
                .HasMany(e => e.StarTrekUserDatas)
                .WithOptional(e => e.StarTrekProduction)
                .HasForeignKey(e => e.ProductionID);

            modelBuilder.Entity<User>()
                .Property(e => e.UserEmail)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserPassword)
                .IsUnicode(false);
        }
    }
}
