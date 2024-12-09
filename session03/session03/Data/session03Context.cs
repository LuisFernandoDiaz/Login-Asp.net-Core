using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using session03.Models;

namespace session03.Data
{
    public class session03Context : DbContext
    {
        public session03Context (DbContextOptions<session03Context> options)
            : base(options)
        {
        }

        public DbSet<session03.Models.Ventas> Ventas { get; set; } = default!;
        public DbSet<session03.Models.Usuarios> Usuarios { get; set; }
    }
}
