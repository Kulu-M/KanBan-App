using System;
using Backend.ConnectionStrings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Backend.Models
{
    public partial class APIAppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlite(DatabaseConnection.DatabaseConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.EMail)
                    .HasName("sqlite_autoindex_User_1");
            });
        }

        public virtual DbSet<Board> Board { get; set; }
        public virtual DbSet<BoardNote> BoardNote { get; set; }
        public virtual DbSet<BoardUser> BoardUser { get; set; }
        public virtual DbSet<Note> Note { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserNote> UserNote { get; set; }
    }
}