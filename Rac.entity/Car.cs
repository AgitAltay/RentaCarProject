using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rac.entity
{
    public class Car:BaseEntity
    {
        
        public string Description { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }   
        public int DailyPrice { get; set; }
        public string Image { get; set; }
        public bool IsRented { get; set; }





    }
}
