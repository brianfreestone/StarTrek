namespace June2018.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class StarTrekProductionModel : DbContext
    {
        public StarTrekProductionModel()
            : base("name=StarTrekProductionDBContext")
        {
        }

        public virtual DbSet<StarTrekProduction> StarTrekProductions { get; set; }
        public virtual DbSet<StarTrekProductionType> StarTrekProductionTypes { get; set; }
        public virtual DbSet<StarTrekSeriesName> StarTrekSeriesNames { get; set; }

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

            modelBuilder.Entity<StarTrekProductionType>()
                .Property(e => e.ProductionType)
                .IsUnicode(false);

            //modelBuilder.Entity<StarTrekProductionType>()
            //    .HasMany(e => e.StarTrekProductions)
            //    .WithOptional(e => e.StarTrekProductionType)
            //    .HasForeignKey(e => e.ProductionTypeID);

            modelBuilder.Entity<StarTrekSeriesName>()
                .Property(e => e.SeriesName)
                .IsUnicode(false);

            //modelBuilder.Entity<StarTrekSeriesName>()
            //    .HasMany(e => e.StarTrekProductions)
            //    .WithOptional(e => e.StarTrekSeriesName)
            //    .HasForeignKey(e => e.SeriesID);
        }

    
    }
}
