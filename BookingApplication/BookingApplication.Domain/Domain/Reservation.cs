using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApplication.Domain.Identity;

namespace BookingApplication.Domain.Domain
{
    public class Reservation : BaseEntity
    {

        [Required]
        public DateTime Check_in_date { get; set; }
        public Guid ApartmentId { get; set; }
        public Apartment? Apartment { get; set; }
        public virtual BookingApplicationUser? User { get; set; }

        public ICollection<BookReservation>? BookedReservations { get; set; }
    }
}
