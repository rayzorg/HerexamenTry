using HerexamenTry.Shared;
using HerexamenTry.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HerexamenTry.Server.Data
{
    public class HerexamenContext : DbContext
    {
        public HerexamenContext(DbContextOptions<HerexamenContext> options) : base(options)
        {

        }

        public DbSet<Jongere> Jongeren { get; set; }
        public DbSet<Begeleider> Begeleiders { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Reactie> Reacties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jongere>().ToTable("Jongere");
            modelBuilder.Entity<Begeleider>().ToTable("Begeleider");
            modelBuilder.Entity<Post>().ToTable("Post");
            modelBuilder.Entity<Reactie>().ToTable("Reactie");
        }
    }
}
