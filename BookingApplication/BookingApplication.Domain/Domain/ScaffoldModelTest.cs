using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApplication.Domain.Domain
{
    public class ScaffoldModelTest : BaseEntity
    {
        [Required]
        public string Country { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
