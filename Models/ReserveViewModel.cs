using Rac.entity.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Rac.Web.Models
{
    public class ReserveViewModel
    {
        public int CarId { get; set; }

        public int CustomerId { get; set; }


        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public decimal DailyPrice { get; set; }
    }

}

