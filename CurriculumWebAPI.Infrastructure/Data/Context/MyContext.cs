using CurriculumWebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CurriculumWebAPI.Infrastructure.IdentityConfiguration;

namespace CurriculumWebAPI.Infrastructure.Data.Context
{
    public class MyContext: IdentityDbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {   }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Curriculum>()
                .HasMany(f => f.Formacao)
                .WithOne(c => c.Curriculum)
                .HasForeignKey(f => f.CurriculumId);
        }


        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Curriculum> Curriculum { get; set; }
        public DbSet<Formacao> Formacao { get; set; }
    }
}