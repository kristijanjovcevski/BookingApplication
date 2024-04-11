using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApplication.Domain.Domain
{
    public class BookReservation : BaseEntity
    {
        public Guid BookListId { get; set; }

        public BookList? BookingList { get; set; }

        public Guid ReservationId { get; set; }

        public Reservation? Reservation { get; set; }

        public int NumberOfNights { get; set; }
    }
}
