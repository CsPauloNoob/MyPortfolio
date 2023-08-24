using CurriculumWebAPI.Domain.Models;
using CurriculumWebAPI.Domain.Models.ComplexTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CurriculumWebAPI.Domain.Models.CurriculumBody;
using System.Runtime.Loader;
using CurriculumWebAPI.Infrastructure.IdentityConfigs;

namespace CurriculumWebAPI.Infrastructure.Data.Context
{
    public class MyContext : IdentityDbContext<ApplicationUser>
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .ToTable("AspNetUsers").HasKey(k => k.Id);


            modelBuilder.Entity<Curriculum>()
            .HasOne(c => c.Contato)
            .WithOne()
            .HasForeignKey<Contato>("CurriculumId");

            modelBuilder.Entity<Curriculum>()
                .HasMany(f => f.Formacao)
                .WithOne(c => c.Curriculum)
                .HasForeignKey(f => f.CurriculumId);

            modelBuilder.Entity<Curriculum>()
                .HasMany(h => h.Experiencia_Profissional)
                .WithOne(c => c.Curriculum)
                .HasForeignKey(f => f.CurriculumId);

            modelBuilder.Entity<Curriculum>()
                .HasMany(h => h.Habilidade)
                .WithOne(c => c.Curriculum)
                .HasForeignKey(f => f.CurriculumId);

            modelBuilder.Entity<Curriculum>()
                .HasMany(h => h.Cursos)
                .WithOne(c => c.Curriculum)
                .HasForeignKey(f => f.CurriculumId);


            //Adiciona o curriculo meu curriculo (Paulo) no banco
            /*modelBuilder.Entity<Curriculum>().HasData(new Curriculum()
            {
                Id = "1",
                Nome = "Paulo Sérgio Pinto Manrique",
                PerfilProgramador = "Desenvolvedor Back-End Web .NET",
                Contato = new Contato() { Email = "ps616131@gmail.com", Endereco = new Address() 
                {Rua = "Rua Vila Gran Cabrita", Bairro="Dom Pedro I", NumeroCasa="500", Cidade="Tabatinga", Estado="AM" }, 
                    Telefone = new PhoneNumber() {Codigo = "55", DDD="97", Numero="988034643" } },

            })*/

        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Contato> Contato { get; set; }
        public DbSet<Curriculum> Curriculum { get; set; }
        public DbSet<Experiencia_Profissional> Experiencias { get; set; }
        public DbSet<Formacao> Formacao { get; set; }
        public DbSet<Cursos_Extras> Cursos_Extras { get; set; }
        public DbSet<Habilidades> Habilidades { get; set; }
    }
}