using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Backend.Models
{
    public partial class APIAppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlite(@"Datasource=F:\GITHUB REPOS\KanBan-App\KanBan-App\Backend\Database\File\KanBanDatabase.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.EMail)
                    .HasName("sqlite_autoindex_User_2")
                    .IsUnique();
            });
        }

        public virtual DbSet<Note> Note { get; set; }
        public virtual DbSet<User> User { get; set; }

        // Unable to generate entity type for table 'Board_User'. Please see the warning messages.
    }
}