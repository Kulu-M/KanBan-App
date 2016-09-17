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
            optionsBuilder.UseSqlite(@"Datasource=C:\Users\Kulu-M\Documents\KanBan-App\KanBan-App\Backend\Database\File\KanBanDatabase.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoardUser>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("sqlite_autoindex_Board_User_1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.EMail)
                    .HasName("sqlite_autoindex_User_1");
            });
        }

        public virtual DbSet<Board> Board { get; set; }
        public virtual DbSet<BoardUser> BoardUser { get; set; }
        public virtual DbSet<Note> Note { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserNote> UserNote { get; set; }
    }
}