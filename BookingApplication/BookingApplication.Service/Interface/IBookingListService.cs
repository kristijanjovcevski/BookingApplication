using BookingApplication.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApplication.Service.Interface
{
    public interface IBookingListService
    {
        public ViewDTO ViewReservationsInBookList(string userId);

        public bool DeleteReservationFromBookList(string userId, Guid reservationId);

        public bool OrderReservations(string userId);
    }
}
