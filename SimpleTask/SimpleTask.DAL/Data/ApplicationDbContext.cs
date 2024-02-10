using Microsoft.EntityFrameworkCore;
using SimpleTask.DAL.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTask.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Priority> priorities { get; set; }
        public DbSet<Document> documents { get; set; }
        public DbSet<DocumentFile> documentFiles { get; set; }
    }
}