using FCManager.DataAccess.Interfaces;
using FCManager.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace FCManager.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IAuthenticatedUserService _authenticatedUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IAuthenticatedUserService authenticatedUser) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _authenticatedUser = authenticatedUser;
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<TeamLogo> TeamLogo { get; set; }
        public DbSet<TeamMemberImage> TeamMemberImage { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<MemberCategory> MemberCategories { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditLog>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = DateTime.Now;
                        entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
