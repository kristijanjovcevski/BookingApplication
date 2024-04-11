using BookingApplication.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApplication.Domain.DTO
{
    public class ViewDTO
    {
        public List<BookReservation>? BookedReservations { get; set; }

        public double TotalPrice { get; set; }


    }
}
