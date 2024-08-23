using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rac.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<RentACarContext>
    {
        public RentACarContext CreateDbContext(string[] args)
        {
            // AppSettings.json dosyasını okuyarak yapılandırmayı oluşturun
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // DbContextOptionsBuilder nesnesini oluşturun ve SQL Server bağlantı dizesini ayarlayın
            var optionsBuilder = new DbContextOptionsBuilder<RentACarContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            // DbContext nesnesini oluşturun ve yapılandırmayı geçirin
            return new RentACarContext(optionsBuilder.Options);
        }

    }
}

