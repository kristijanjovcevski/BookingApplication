using BookingApplication.Domain.Domain;
using BookingApplication.Repository.Interface;
using BookingApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApplication.Service.Implementation
{

    public class ApartmentService : IApartmentService
    {

        private readonly IRepository<Apartment> _apartmentRepository;

        public ApartmentService(IRepository<Apartment> apartmentRepository)
        {
            _apartmentRepository = apartmentRepository;
        }

        public Apartment CreateNewApartment(Apartment apartment)
        {
            return _apartmentRepository.Insert(apartment);
        }

        public Apartment DeleteApartment(Guid id)
        {
            Apartment apartment = this.GetApartmentById(id);

            return _apartmentRepository.Delete(apartment);
        }

        public List<Apartment> GetApartments()
        {
            return _apartmentRepository.GetAll().ToList();
        }

        public Apartment GetApartmentById(Guid? id)
        {
            return _apartmentRepository.Get(id);

        }

        public Apartment UpdateApartment(Apartment apartment)
        {
           
           return _apartmentRepository.Update(apartment);
        }

        
    }
}
