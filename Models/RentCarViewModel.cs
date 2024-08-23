using Rac.entity.DTOs;

namespace Rac.Web.Models
{
    public class RentCarViewModel
    {
        public CarDto Car { get; set; } 
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; } 

        public int TotalPrice { get; set; }
        public int CarId { get; set; }
    }
}
