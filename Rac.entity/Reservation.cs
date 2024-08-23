﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rac.entity
{
    public class Reservation:BaseEntity
    {
        
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }



        public Customer Customer { get; set; }
        public Car Car { get; set; }

    }
}
