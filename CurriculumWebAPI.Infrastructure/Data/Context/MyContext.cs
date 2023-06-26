using CurriculumWebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CurriculumWebAPI.Infrastructure.Data.Context
{
    public class MyContext: DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {   }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Curriculum>()
                .HasMany(f => f.Formacao)
                .WithOne(c => c.Curriculum)
                .HasForeignKey(f => f.CurriculumId);
        }


        public DbSet<Curriculum> Curriculum { get; set; }
        public DbSet<Formacao> Formacao { get; set; }
    }
}