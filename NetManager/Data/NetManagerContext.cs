using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetManager.Models;

namespace NetManager.Data
{
    public class NetManagerContext : DbContext
    {
        public NetManagerContext (DbContextOptions<NetManagerContext> options)
            : base(options)
        {
        }

        public DbSet<NetManager.Models.Users> Users { get; set; } = default!;

        public DbSet<NetManager.Models.Parcs> Parcs { get; set; } = default!;

        public DbSet<NetManager.Models.Workstation> Workstation { get; set; } = default!;

        public DbSet<NetManager.Models.Logs> Logs { get; set; } = default!;

        public DbSet<NetManager.Models.Room> Room { get; set; } = default!;
    }
}
