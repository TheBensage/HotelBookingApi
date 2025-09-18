using HotelBooking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Hotel> Hotels => Set<Hotel>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Booking> Bookings => Set<Booking>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hotel>(e =>
        {
            e.HasKey(h => h.Id);
            e.Property(h => h.Name).IsRequired();

            e.HasMany(h => h.Rooms)
             .WithOne(r => r.Hotel)
             .HasForeignKey(r => r.HotelId)
             .IsRequired()
             .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Room>(e =>
        {
            e.HasKey(r => r.Id);
            e.Property(r => r.RoomNumber).IsRequired();
            e.Property(r => r.Type).IsRequired();

            e.HasMany(r => r.Bookings)
             .WithOne(b => b.Room)
             .HasForeignKey(b => b.RoomId)
             .IsRequired()
             .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Booking>(e =>
        {
            e.HasKey(b => b.Id);
            e.Property(b => b.CheckInDate).IsRequired();
            e.Property(b => b.CheckOutDate).IsRequired();
            e.Property(b => b.Guests).IsRequired();
            e.Property(b => b.Reference).IsRequired();
            e.HasIndex(b => b.Reference).IsUnique();
            e.HasOne(b => b.Room)
             .WithMany(r => r.Bookings)
             .HasForeignKey(b => b.RoomId)
             .IsRequired()
             .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
