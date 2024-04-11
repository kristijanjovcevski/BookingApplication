using BookingApplication.Domain.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookingApplication.Domain.Identity
{
    public class BookingApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public BookList? BookingList { get; set; }

        public virtual ICollection<Reservation>? Reservations { get; set; }
    }
}
