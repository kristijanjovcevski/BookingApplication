using BookingApplication.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApplication.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<BookingApplicationUser> GetAll();
        BookingApplicationUser Get(string id);
        void Insert(BookingApplicationUser entity);
        void Update(BookingApplicationUser entity);
        void Delete(BookingApplicationUser entity);
    }

}
