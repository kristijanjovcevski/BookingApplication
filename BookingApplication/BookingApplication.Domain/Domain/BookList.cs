using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApplication.Domain.Identity;

namespace BookingApplication.Domain.Domain
{
    public class BookList : BaseEntity
    {


      

        public string? CustomerId { get; set; }

        public BookingApplicationUser? Customer { get; set; }

        public ICollection<BookReservation>? Reservations { get; set; }

    }
}
