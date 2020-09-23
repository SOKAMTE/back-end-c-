using offreService.Models;
using Microsoft.EntityFrameworkCore;

namespace offreService.Data
{
    public class OffreServiceContext: DbContext
    {

        public OffreServiceContext(DbContextOptions<OffreServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<ArticleType> ArticleTypes { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Commentaire> Commentaires { get; set; }
        public DbSet<Compte> Comptes { get; set; }
        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            /*modelBuilder.Entity<Discussion>()
                .HasMany(c => c.Clients).WithMany(i => i.Discussions)
                .Map(t => t.MapLeftKey("idDiscussion")
                    .MapRightKey("idClient")
                    .ToTable("DiscussionClient"));

            modelBuilder.Entity<Admin>()
                .HasMany(c => c.Clients).WithMany(i => i.Admins)
                .Map(t => t.MapLeftKey("idAdmin")
                    .MapRightKey("idClient")
                    .ToTable("AdminClient"));*/

            modelBuilder.Entity<DiscussionClient>()
            .HasKey(t => new { t.idDiscussion, t.idClient });

            modelBuilder.Entity<DiscussionClient>()
                .HasOne(pt => pt.Discussion)
                .WithMany(p => p.DiscussionClients)
                .HasForeignKey(pt => pt.idDiscussion);

            modelBuilder.Entity<DiscussionClient>()
                .HasOne(pt => pt.Client)
                .WithMany(t => t.DiscussionClients)
                .HasForeignKey(pt => pt.idClient);

            /*Configuration de Admin et Client*/
            modelBuilder.Entity<AdminClient>()
            .HasKey(t => new { t.idAdmin, t.idClient });

            modelBuilder.Entity<AdminClient>()
                .HasOne(pt => pt.Admin)
                .WithMany(p => p.AdminClients)
                .HasForeignKey(pt => pt.idAdmin);

            modelBuilder.Entity<AdminClient>()
                .HasOne(pt => pt.Client)
                .WithMany(t => t.AdminClients)
                .HasForeignKey(pt => pt.idClient);
        }
        
    }
}
