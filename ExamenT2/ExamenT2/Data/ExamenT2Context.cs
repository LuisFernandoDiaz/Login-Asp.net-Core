using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExamenT2.Models;

namespace ExamenT2.Data
{
    public class ExamenT2Context : DbContext
    {
        public ExamenT2Context (DbContextOptions<ExamenT2Context> options)
            : base(options)
        {
        }

        public DbSet<ExamenT2.Models.Videoteca> Videoteca { get; set; } = default!;
    }
}
