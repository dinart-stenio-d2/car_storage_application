using Car.Storage.Application.Administrators.Data.Repositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace Car.Storage.Application.Administrators.Data.Repositories.EFContext
{
    public class CarStorageContext : DbContext
    {
        public DbSet<Entities.Car> Cars { get; set; }
        public DbSet<CarOwner> CarOwners { get; set; }

        public CarStorageContext(DbContextOptions<CarStorageContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Car>(entity =>
            {
                entity.HasKey(c => c.Id).IsClustered();
                entity.Property(c => c.Brand).IsRequired().HasMaxLength(50);
                entity.Property(c => c.Model).IsRequired().HasMaxLength(50);
                entity.Property(c => c.Year).IsRequired();
                entity.Property(c => c.Color).IsRequired().HasMaxLength(20);
                entity.Property(c => c.IsRunning).IsRequired();
                entity.Property(c => c.IsNew).IsRequired();
                entity.Property(c => c.IsForSale).IsRequired();
                entity.Property(c => c.Price).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(c => c.VehicleIdentificationNumber).IsRequired().HasMaxLength(50);
                entity.Property(c => c.CarPlate).HasMaxLength(10);

                entity.HasOne(c => c.Owner)
                      .WithMany()
                      .HasForeignKey("Id");
            });

            modelBuilder.Entity<CarOwner>(entity =>
            {
                entity.HasKey(o => o.Id).IsClustered(); ;
                entity.Property(o => o.FirstName).HasMaxLength(50);
                entity.Property(o => o.LastName).HasMaxLength(50);
                entity.Property(o => o.Address).HasMaxLength(100);
                entity.Property(o => o.PhoneNumber).HasMaxLength(20);
                entity.Property(o => o.Email).HasMaxLength(50);
                entity.Property(o => o.DocumentNumber).IsRequired().HasMaxLength(20);
                entity.Property(o => o.DocumentType).IsRequired().HasMaxLength(20);
                entity.Property(o => o.DocumentExpiryDate).IsRequired().HasMaxLength(10);
                entity.Property(o => o.DocumentIssuingAuthority).IsRequired().HasMaxLength(50);
                entity.Property(o => o.DocumentDateOfCreation).IsRequired().HasMaxLength(10);
            });
        }
    }
}
