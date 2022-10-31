using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApexDataApi.Models;

namespace ApexDataApi.Data
{
    public class ApexDataApiContext : DbContext
    {
        public ApexDataApiContext (DbContextOptions<ApexDataApiContext> options)
            : base(options)
        {
        }

        public DbSet<ApexDataApi.Models.Player> Player { get; set; } = default!;

        public DbSet<ApexDataApi.Models.Character> Character { get; set; }
    }
}
