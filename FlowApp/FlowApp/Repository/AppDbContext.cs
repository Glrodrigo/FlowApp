using FlowApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace FlowApp.Repository
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserBase> Users { get; set; }
        public DbSet<GoalBase> Goals { get; set; }
        public DbSet<HabitBase> Habits { get; set; }
        public DbSet<HabitCheckBase> HabitChecks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserBase>()
            .HasKey(a => a.Id);

            modelBuilder.Entity<UserBase>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            modelBuilder.Entity<UserBase>()
                .HasIndex(p => p.Hash)
                .IsUnique();

            modelBuilder.Entity<UserBase>()
                .Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<UserBase>()
                .Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(300);

            modelBuilder.Entity<UserBase>()
                .Property(a => a.PasswordHash)
                .IsRequired();

            modelBuilder.Entity<UserBase>()
                .Property(a => a.CreateDate)
                .IsRequired();


            modelBuilder.Entity<GoalBase>()
                .HasKey(g => g.Id);

            modelBuilder.Entity<GoalBase>()
                .Property(g => g.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<GoalBase>()
                .HasOne(g => g.User)
                .WithMany() // se tiver a coleção
                .HasForeignKey(g => g.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GoalBase>()
                .Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<GoalBase>()
                .Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(300);

            modelBuilder.Entity<GoalBase>()
                .Property(a => a.TargetDate)
                .IsRequired();


            modelBuilder.Entity<HabitBase>()
                .HasKey(g => g.Id);

            modelBuilder.Entity<HabitBase>()
                .Property(g => g.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<HabitBase>()
                .HasOne(g => g.Goal)
                .WithMany() // se tiver a coleção
                .HasForeignKey(g => g.GoalId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HabitBase>()
                .Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<HabitBase>()
                .Property(a => a.FrequencyCode)
                .IsRequired();

            modelBuilder.Entity<HabitBase>()
                .Property(a => a.FrequencyName)
                .IsRequired();


            modelBuilder.Entity<HabitCheckBase>()
                .HasKey(g => g.Id);

            modelBuilder.Entity<HabitCheckBase>()
                .Property(g => g.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<HabitCheckBase>()
                .HasOne(g => g.Habit)
                .WithMany() // se tiver a coleção
                .HasForeignKey(g => g.HabitId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HabitCheckBase>()
                .Property(a => a.Completed)
                .IsRequired();

            modelBuilder.Entity<HabitCheckBase>()
                .Property(a => a.Hash)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
