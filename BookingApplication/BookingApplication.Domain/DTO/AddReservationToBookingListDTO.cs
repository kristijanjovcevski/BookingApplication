using BookingApplication.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApplication.Domain.DTO
{
    public class AddReservationToBookingListDto
    {
        public Guid SelectedReservationId { get; set; }

        public int NumberOfNights { get; set; }
    }
}
