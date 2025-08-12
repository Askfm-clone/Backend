using System.Data;
using AskFm.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Thread = AskFm.DAL.Models.Thread;

namespace AskFm.DAL;

public class AppDbContext : IdentityDbContext<ApplicationUser,IdentityRole<int>, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Thread> Threads { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Follow> Follows { get; set; }
    public DbSet<ThreadLike> ThreadLikes { get; set; }
    public DbSet<CommentLike> CommentLikes { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<SavedThreads> SavedThreads { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        
        foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                     .Where(t => typeof(ITrackable).IsAssignableFrom(t.ClrType)))
        {

            modelBuilder.Entity(entityType.ClrType).Property<DateTime>("DeletedAt")
                .HasColumnType("DATETIME");

            modelBuilder.Entity(entityType.ClrType).Property<DateTime>("UpdatedAt")
                .HasColumnType("DATETIME");

            modelBuilder.Entity(entityType.ClrType).Property<DateTime>("CreatedAt")
                .HasColumnType("DATETIME");

            modelBuilder.Entity(entityType.ClrType).Property<bool>("IsDeleted")
                .HasColumnType("BIT")
                .HasDefaultValue(false);
        }
        
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        _fillTrackableInfromation();
        return base.SaveChangesAsync(cancellationToken);
        
    }
    public override int SaveChanges()
    {
        _fillTrackableInfromation();
        return base.SaveChanges();
    }
    private void _fillTrackableInfromation()
    {
        foreach (var entry in  ChangeTracker.Entries<ITrackable>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.DeletedAt = DateTime.UtcNow;;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.IsDeleted = false;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Deleted:
                    entry.Entity.IsDeleted = true;
                    entry.Entity.DeletedAt = DateTime.UtcNow;
                    entry.State = EntityState.Modified;
                    break;
            }
        }
    }
}