using Microsoft.EntityFrameworkCore;
using Rac.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Rac.Data
{
    public class RentACarContext : DbContext
    {
        public RentACarContext(DbContextOptions<RentACarContext> options) : base(options)

        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Customer> Customers { get; set; }

      
    }
}
