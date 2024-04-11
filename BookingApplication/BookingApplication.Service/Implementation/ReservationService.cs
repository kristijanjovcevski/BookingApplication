using BookingApplication.Domain.Domain;
using BookingApplication.Domain.DTO;
using BookingApplication.Repository.Interface;
using BookingApplication.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApplication.Service.Implementation
{
    
    public class ReservationService : IReservationService
    {

        private readonly IRepository<Reservation> _reservationRepository;
        private readonly IUserRepository _userRepository;

        private readonly IRepository<BookList> _bookListRepository;
      

        public ReservationService (IRepository<Reservation> reservationRepository, IUserRepository userRepository,
            IRepository<BookList> bookListRepository)
        {
            _reservationRepository = reservationRepository;
            _userRepository = userRepository;
            _bookListRepository = bookListRepository;
           
        }

        public AddReservationToBookingListDto AddReservationToBookingList(Guid id)
        {
            var SelectedReservation = _reservationRepository.Get(id);

            AddReservationToBookingListDto model = new AddReservationToBookingListDto
            {
                SelectedReservationId = id,
                NumberOfNights = 1
            };
            return model;
        }

        public Reservation CreateNewReservation(string userId, Reservation reservation)
        {

            if (userId == null && reservation == null)
            {
                throw new NullReferenceException();
            }
            var createdBy = _userRepository.Get(userId);

            reservation.User = createdBy;

            return _reservationRepository.Insert(reservation);

           

        }

        public Reservation DeleteReservation(Guid id)
        {
            if (id == null)
            {
                throw new NullReferenceException();

            }
           Reservation reservation = this.GetReservationById(id);

            _reservationRepository.Delete(reservation);


           return reservation;
        }

        public bool GetBookListDetails(string userId, AddReservationToBookingListDto model)
        {
            var loggedInUser = _userRepository.Get(userId);

            if (loggedInUser != null)
            {
                var bookList = loggedInUser?.BookingList;

                var reservation = _reservationRepository.Get(model.SelectedReservationId);
                if (bookList != null)
                {
                    bookList?.Reservations?.Add(new BookReservation
                    {
                        BookListId = bookList.Id,
                        BookingList = bookList,
                        ReservationId = model.SelectedReservationId,
                        Reservation = reservation,
                        NumberOfNights = model.NumberOfNights
                    });
                    _bookListRepository.Update(bookList);
                    return true;
                }
                return false;
            }
            return false;
        }

        public Reservation GetReservationById(Guid? id)
        {
            if (id == null)
            {
                throw new NullReferenceException();

            }

            Reservation reservation = _reservationRepository.Get(id);

            return reservation;
        }

        public List<Reservation> GetReservations()
        {
            return _reservationRepository.GetAll().ToList();
        }

        public Reservation UpdateReservation(Reservation reservation)
        {
            return _reservationRepository.Update(reservation);
        }
    }
}
