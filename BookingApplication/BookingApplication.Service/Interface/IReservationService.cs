using BookingApplication.Domain.Domain;
using BookingApplication.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApplication.Service.Interface
{
    public interface IReservationService
    {
        public List<Reservation> GetReservations();
        public Reservation GetReservationById(Guid? id);
        public Reservation CreateNewReservation(string userId, Reservation reservation);
        public Reservation UpdateReservation(Reservation reservation);
        public Reservation DeleteReservation(Guid id);

        public AddReservationToBookingListDto AddReservationToBookingList(Guid id);

        public bool GetBookListDetails(string userId,AddReservationToBookingListDto model);
       

       

    }
}
