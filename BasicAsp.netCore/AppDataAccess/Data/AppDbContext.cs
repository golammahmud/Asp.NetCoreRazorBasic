using AppDomain.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDataAccess.Data
{
    public class AppDbContext:DbContext
    {      
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Category { get; set; }

        public virtual DbSet<FileModel> FileModel { get; set; }

        public virtual DbSet<MultipleCheckbox> MultipleCheckbox { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
