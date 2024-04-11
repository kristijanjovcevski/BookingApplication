using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authorization;
//using BookingApplication.Models.DTO;
using System.Security.Claims;
using BookingApplication.Service.Implementation;
using BookingApplication.Domain.Domain;
using BookingApplication.Repository.Interface;
using BookingApplication.Domain.Identity;
using BookingApplication.Service.Interface;
using BookingApplication.Domain.DTO;

namespace BookingApplication.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IReservationService _reservationService;

        private readonly IApartmentService _apartmentService;

        private readonly IUserRepository _userRepository;

        public ReservationsController(IReservationService reservationService, IApartmentService apartmentService,
             IUserRepository userRepository)
        {
            _reservationService = reservationService;
            _apartmentService = apartmentService;
            _userRepository = userRepository;
        }

        // GET: Reservations
        
        public IActionResult Index()
        {
           
            return View(_reservationService.GetReservations());
        }

        // GET: Reservations/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = _reservationService.GetReservationById(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["ApartmentId"] = new SelectList(_apartmentService.GetApartments(), "Id", "ApartmentName");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Check_in_date,ApartmentId")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
               
                reservation.Id = Guid.NewGuid();
                
                _reservationService.CreateNewReservation(userId,reservation);
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApartmentId"] = new SelectList(_apartmentService.GetApartments(), "Id", "ApartmentName", reservation.ApartmentId);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = _reservationService.GetReservationById(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["ApartmentId"] = new SelectList(_apartmentService.GetApartments(), "Id", "ApartmentName", reservation.ApartmentId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(Guid id, [Bind("Id,Check_in_date,ApartmentId")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _reservationService.UpdateReservation(reservation);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (reservation != null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApartmentId"] = new SelectList(_apartmentService.GetApartments(), "Id", "ApartmentName", reservation.ApartmentId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var reservation = _reservationService.GetReservationById(id);
            //var reservation = await _context.Reservations
            //    .Include(r => r.Apartment)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var reservation = _reservationService.GetReservationById(id);
            if (reservation != null)
            {
                _reservationService.DeleteReservation(id);
            }

            
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public IActionResult AddReservationToBookList(Guid id)
        {

            if (id != null)
            {
                var model = _reservationService.AddReservationToBookingList(id);

                return View(model);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddReservationToBookList(AddReservationToBookingListDto model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _reservationService.GetBookListDetails(userId, model);
            if (result)
            {
                return RedirectToAction("Index", "Reservations");
            }
            return RedirectToAction("Index","Reservations");

            //var applicationDbContext = _context.Reservations.Include(r => r.Apartment);
            //return View(await applicationDbContext.ToListAsync());
        }

        //private bool ReservationExists(Guid id)
        //{
        //    return _context.Reservations.Any(e => e.Id == id);
        //}
    }
}
