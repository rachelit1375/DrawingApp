using Microsoft.EntityFrameworkCore;

namespace DrawingApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Drawing> Drawings { get; set; }
        public DbSet<DrawingCommand> DrawingCommands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ÷ùø áéï User ìÎDrawing
            modelBuilder.Entity<Drawing>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ÷ùø áéï Drawing ìÎDrawingCommand
            modelBuilder.Entity<DrawingCommand>()
                .HasOne<Drawing>()
                .WithMany()
                .HasForeignKey(dc => dc.DrawingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}