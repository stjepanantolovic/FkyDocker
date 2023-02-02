using DocuSignPOC2.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuSignPOC2
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Party> Parties { get; set; }
        public DbSet<Envelope> Envleopes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Party>().HasKey(p => p.Id);
            modelBuilder.Entity<Envelope>().HasKey(p => p.Id);
            modelBuilder.Entity<ESignDocument>().HasKey(p => p.Id);

            modelBuilder.Entity<Envelope>()
                .HasMany(e => e.Parties)
                .WithMany(p => p.Envelopes);

            modelBuilder.Entity<Envelope>()
                .HasMany(e => e.ESignDocuments)
                .WithOne(p => p.Envelope);

        }



    }
}
