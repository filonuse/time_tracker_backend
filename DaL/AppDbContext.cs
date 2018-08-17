using Core.Models.Models;
using Core.Models.Models.Bind_Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DaL
{
    public class AppDbContext : IdentityDbContext<UserModel>
    {
        public DbSet<AppInfoModel> AppInfos { get; set; }
        public DbSet<FileModel> Files { get; set; }
        public DbSet<GroupModel> Groups { get; set; }
        public DbSet<LogModel> Logs { get; set; }
        public DbSet<UserWorkSessionDataModel> UserWorkSessionData { get; set; }

        #region DbSet of bind models
        public DbSet<GroupAppModel> GroupApps { get; set; }
        public DbSet<UserAppModel> UserApps { get; set; }
        public DbSet<UserGroupModel> UserGroups { get; set; }
        #endregion

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Enable many-to-many relations between UserModel and IdentityClaim model.
            modelBuilder.Entity<UserModel>(b => {
                b.HasMany(x => x.Claims)
                .WithOne()
                .HasForeignKey(ur => ur.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            });
            #endregion

            #region Enable many-to-many relation between GroupModel and UserModel.
            modelBuilder.Entity<UserGroupModel>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<UserGroupModel>()
                .HasAlternateKey(eg=> new { eg.GroupId, eg.UserId });

            modelBuilder.Entity<UserGroupModel>()
                .HasOne(eg => eg.User)
                .WithMany(e => e.UserGroups)
                .HasForeignKey(eg => eg.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserGroupModel>()
                .HasOne(eg => eg.Group)
                .WithMany(g => g.UserGroups)
                .HasForeignKey(eg => eg.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Enable many-to-many relation between UserModel and AppInfoModel.
            modelBuilder.Entity<UserAppModel>()
               .HasKey(x => x.Id);

            modelBuilder.Entity<UserAppModel>()
                .HasAlternateKey(eg => new { eg.UserId, eg.AppId });

            modelBuilder.Entity<UserAppModel>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserApps)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserAppModel>()
                .HasOne(ua => ua.App)
                .WithMany(a => a.UserApps)
                .HasForeignKey(ua => ua.AppId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
