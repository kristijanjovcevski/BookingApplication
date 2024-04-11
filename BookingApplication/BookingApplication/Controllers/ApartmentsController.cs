using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using BookingApplication.Domain.Domain;
using BookingApplication.Repository;
using BookingApplication.Service.Implementation;
using BookingApplication.Service.Interface;
using Microsoft.AspNetCore.Authorization;

namespace BookingApplication.Controllers
{
    public class ApartmentsController : Controller
    {
       

        private readonly IApartmentService _apartmentService;

        public ApartmentsController(IApartmentService apartmentService)
        {
            _apartmentService = apartmentService;
        }

        // GET: Apartments
        public IActionResult Index()
        {
            return View(_apartmentService.GetApartments());
        }

        // GET: Apartments/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = _apartmentService.GetApartmentById(id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }
        [Authorize]
        // GET: Apartments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Apartments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,ApartmentName,City,Description,Price_per_night,Rating")] Apartment apartment)
        {
            if (ModelState.IsValid)
            {
                apartment.Id = Guid.NewGuid();

                _apartmentService.CreateNewApartment(apartment);

                return RedirectToAction(nameof(Index));
            }
            return View(apartment);
        }

        // GET: Apartments/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = _apartmentService.GetApartmentById(id);
            if (apartment == null)
            {
                return NotFound();
            }
            return View(apartment);
        }

        // POST: Apartments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,ApartmentName,City,Description,Price_per_night,Rating")] Apartment apartment)
        {
            if (id != apartment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _apartmentService.UpdateApartment(apartment);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (apartment.Id == null)
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
            return View(apartment);
        }

        // GET: Apartments/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = _apartmentService.GetApartmentById(id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // POST: Apartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var apartment = _apartmentService.GetApartmentById(id);
            if (apartment != null)
            {
                _apartmentService.DeleteApartment(id);
            }

           
            return RedirectToAction(nameof(Index));
        }

        
    }
}
