using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using BookingApplication.Domain.Identity;
using BookingApplication.Domain.Domain;


namespace BookingApplication.Repository
{
    public class ApplicationDbContext : IdentityDbContext<BookingApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BookReservation>()
                .HasKey(k => new { k.BookListId, k.ReservationId });

            base.OnModelCreating(builder);
        }
        public virtual DbSet<BookingApplication.Domain.Domain.ScaffoldModelTest> ScaffoldModelTests { get; set; }
        public virtual DbSet<BookingApplication.Domain.Domain.Apartment> Apartments { get; set; }
        public virtual DbSet<BookingApplication.Domain.Domain.Reservation> Reservations { get; set; }

        public virtual DbSet<BookingApplication.Domain.Domain.BookReservation> BookReservation { get; set; }

        public virtual DbSet<BookingApplication.Domain.Domain.BookList> BookList { get; set; }

    }
}
