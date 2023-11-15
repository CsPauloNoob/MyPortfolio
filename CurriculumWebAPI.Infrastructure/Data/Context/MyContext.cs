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
        //private const string _pauloId = "5c998a7b-b9c5-45fb-90f8-1326b5c2";

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

            /*modelBuilder.Entity<User>(u =>
            {
                u.HasData(new
                {
                    Id = "asfsadsfccfawfaxvwtfaq-dasd",
                    UserName = "Paulo",
                    Email = "ps616131@gmail.com",
                    Password = "SSWX3988",
                    CurriculumId = _pauloId,
                    Role = "admin"
                });
            });

            modelBuilder.Entity<Curriculum>(c => {
                c.HasData(
                    new
                    {
                        Id = _pauloId,
                        Nome = "Paulo Sérgio Pinto Manrique",
                        PerfilProgramador = "Desenvolvedor Back-End Web .NET",
                        Contato = new Contato()
                        {
                            Email = "ps616131@gmail.com",
                            Endereco = new Address()
                            { Rua = "Rua Vila Gran Cabrita", Bairro = "Dom Pedro I", NumeroCasa = "500", Cidade = "Tabatinga", Estado = "AM" },
                            Telefone = new PhoneNumber() { Codigo = "55", DDD = "97", Numero = "988034643" }
                        },
                        Cursos = new List<Cursos_Extras> { new Cursos_Extras()
                        { Nome_Curso = "ASP.NET Avançado", Organizacao = "Udemy" },
                            new Cursos_Extras() { Nome_Curso = "Microsserviços com .NET 6", Organizacao = "Udemy" } },
                        Formacao = new List<Formacao> { new Formacao { Curso = "Análise e desenvolvimento de sistemas",
                            Instituicao = "UNIFAEL", AnoConclusao = 2022 } },
                        Experiencia_Profissional = new List<Experiencia_Profissional>()
                            { new Experiencia_Profissional() { Descricao = "01/2023 a 05/2023. " +
                        "Desenvolvi por compelto aplicação desktop com C# e " +
                        "Windows Forms, a aplicação tinha como objetivo " +
                        "automatizar vários processos para criação de vídeos," +
                        " além disso o programa consegue postar automaticamente" +
                        " os vídeos criados por ela nas plataformas sociais de destino",
                        Funcao = "PROGRAMADOR DESKTOP - C#/WINDOWS FORMS", Nome_Organizacao = "FREELANCER" } },
                        Habilidade = new List<Habilidades> {
                            new Habilidades() { Nome_Habilidade = "Idiomas", Descricao = "Inglês básico(conversação, leitura e escrita) e Espanhol intermediário" },
                            new Habilidades() { Nome_Habilidade = "Sistemas Operacionais", Descricao = "Windows e Linux(Debian e Ubuntu)" },
                            new Habilidades() { Nome_Habilidade = "Linguagensde Programação", Descricao = "C# e C/C++" },
                            new Habilidades() { Nome_Habilidade = "FrontEnd", Descricao = "JavaScript, HTML e CSS" },
                            new Habilidades() { Nome_Habilidade = "WebServices", Descricao = "API's REST e Swagger" },
                            new Habilidades() { Nome_Habilidade = "Cloud Computing", Descricao = "Azure(provisionamento de VMs, " +
                            "conteinerização com Docker, deploy e provisionamento de recursos e orçamentos em geral.)" },
                            new Habilidades() { Nome_Habilidade = "Tecnologias e Frameworks",
                                Descricao = ".Netcore, .NET, ASP.NETCore, Entity Framework Core, Dapper, Selleniume e Identity" },
                            new Habilidades() { Nome_Habilidade = "CI/CD", Descricao = "Github Actions" },
                            new Habilidades() { Nome_Habilidade = "Banco de Dados", Descricao = "MySQL, SqlServer e Sqlite" },
                            new Habilidades() { Nome_Habilidade = "Outros", Descricao = "Orientação à objetos, MVC, SOLID, Clean Architeture e DDD;" }
                        },
                        SobreMim = "Desenvolvedor Back-End .NET",
                        DataCriacao = DateTime.Now

                    });

                /*c.OwnsOne(e => e.Contato).HasData(new
                {
                    CurriculumId = _pauloId,
                    Email = "ps616131@gmail.com",
                    Endereco = new Address()
                    { Rua = "Rua Vila Gran Cabrita", Bairro = "Dom Pedro I", NumeroCasa = "500", Cidade = "Tabatinga", Estado = "AM" },
                    Telefone = new PhoneNumber() { Codigo = "55", DDD = "97", Numero = "988034643" }
                });

                
            });

            modelBuilder.Entity<Contato>(c =>
            {
                c.HasData(new
                {
                    CurriculumId = _pauloId,
                    Email = "ps616131@gmail.com",
                    Endereco = new Address()
                    { Rua = "Rua Vila Gran Cabrita", Bairro = "Dom Pedro I", NumeroCasa = "500", Cidade = "Tabatinga", Estado = "AM" },
                    Telefone = new PhoneNumber() { Codigo = "55", DDD = "97", Numero = "988034643" }
                });
            });*/

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