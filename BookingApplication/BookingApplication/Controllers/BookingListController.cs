//using BookingApplication.Data;
//using BookingApplication.Models;
//using BookingApplication.Models.DTO;

using BookingApplication.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookingApplication.Controllers
{
    public class BookingListController : Controller
    {


        private readonly IBookingListService _bookingListService;

        public BookingListController(IBookingListService bookingListService)
        {
            _bookingListService = bookingListService;
        }
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = _bookingListService.ViewReservationsInBookList(userId);

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteReservation(Guid reservationId)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _bookingListService.DeleteReservationFromBookList(userId, reservationId);

            if (result)
            {
                return RedirectToAction("Index", "BookingList");
            }

            return RedirectToAction("Index", "BookingList");
        }



        public IActionResult Order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _bookingListService.OrderReservations(userId);

            if (result)

            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }

}
