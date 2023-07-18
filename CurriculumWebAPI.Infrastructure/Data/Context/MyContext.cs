using CurriculumWebAPI.Domain.Models;
using CurriculumWebAPI.Domain.Models.ComplexTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CurriculumWebAPI.Infrastructure.IdentityConfiguration;
using CurriculumWebAPI.Domain.Models.CurriculumBody;

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

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(u => u.NormalizedEmail)
                .HasDatabaseName("EmailIndex")
                .IsUnique();

            modelBuilder.Entity<Curriculum>()
                .HasMany(f => f.Formacao)
                .WithOne(c => c.Curriculum)
                .HasForeignKey(f => f.CurriculumId);

            //Adiciona o curriculo meu curriculo (Paulo) no banco
            modelBuilder.Entity<Curriculum>().HasData(new Curriculum()
            {
                Id = "1",
                Nome = "Paulo Sérgio Pinto Manrique",
                PerfilProgramador = "Desenvolvedor Back-End Web .NET",
                Contato = new Contato() { Email = "ps616131@gmail.com", Endereco = new Adress() 
                {Rua = "Rua Vila Gran Cabrita", Bairro="Dom Pedro I", NumeroCasa="500", Cidade="Tabatinga", Estado="AM" }, 
                    Telefone = new PhoneNumber() {Codigo = "55", DDD="97", Numero="988034643" } },

            })
            
        }

        void InstanceCurriculumFields()

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Curriculum> Curriculum { get; set; }
        public DbSet<Formacao> Formacao { get; set; }
    }
}