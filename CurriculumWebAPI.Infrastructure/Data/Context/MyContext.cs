using CurriculumWebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Infrastructure.Data.Context
{
    public class MyContext: DbContext
    {
        public MyContext() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlite("Data Source=CurriculumDb.sqlite;");
        }

        public DbSet<Curriculum> Curriculum { get; set; }
    }
}