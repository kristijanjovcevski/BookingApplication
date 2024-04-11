using BookingApplication.Domain.Domain;
using BookingApplication.Domain.DTO;
using BookingApplication.Repository.Interface;
using BookingApplication.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApplication.Service.Implementation
{
    public class BookingListService : IBookingListService
    {

        //private readonly IRepository<Reservation> _reservationRepository;
        private readonly IUserRepository _userRepository;

        private readonly IRepository<BookList> _bookListRepository;

        private readonly IRepository<BookReservation> _bookReservationRepository;

        public BookingListService (IUserRepository userRepository, IRepository<BookList> bookListRepository,
            IRepository<BookReservation> bookReservationRepository)
        {
            _userRepository = userRepository;
            _bookListRepository = bookListRepository;
            _bookReservationRepository = bookReservationRepository;
        }

        public bool DeleteReservationFromBookList(string userId, Guid reservationId)
        {
            var loggedInUser = _userRepository.Get(userId);

            if (loggedInUser != null)
            {

                var bookList = loggedInUser?.BookingList;
                if (bookList != null)
                {
                    var bookReservation = _bookReservationRepository.Get(reservationId);

                    bookList?.Reservations?.Remove(bookReservation);

                    _bookListRepository.Update(bookList);
                    return true;
                }
                return false;
            }
            return false;
            
        }

        public ViewDTO ViewReservationsInBookList(string userId)
        {
            var loggedInUser = _userRepository.Get(userId);

            if (loggedInUser != null)
            {
                var bookList = loggedInUser?.BookingList;

                if ( bookList !=null)
                {
                    var reservationsList = bookList?.Reservations?.ToList();   

                    if ( reservationsList != null ) {

                        double totalPrice = 0.0;

                        foreach (var reservation in reservationsList ) {

                            totalPrice += reservation.Reservation.Apartment.Price_per_night * reservation.NumberOfNights;

                        }


                        ViewDTO model = new ViewDTO
                        {
                            BookedReservations = reservationsList,
                            TotalPrice = totalPrice
                        };
                        _bookListRepository.Update(bookList);
                        return model;

                    }

                  
                  
                }

                
            }

            return new ViewDTO();
        }

        public bool OrderReservations(string userId)
        {
            var loggedInUser = _userRepository.Get(userId);

            if (loggedInUser != null)
            {
                var bookList = loggedInUser.BookingList;

                if ( bookList != null ) {

                    bookList?.Reservations?.Clear();

                    _bookListRepository.Update(bookList);

                    return true;
                }
                return false;

            }
            return false;
        }
    }
}
