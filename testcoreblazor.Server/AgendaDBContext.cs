using BlazorAgenda.Server;
using Microsoft.EntityFrameworkCore;

namespace BlazorAgenda.Shared.Models
{
    public partial class AgendaDBContext : DbContext
    {
        public AgendaDBContext()
        {
        }

        public AgendaDBContext(DbContextOptions<AgendaDBContext> options) : base(options)
        {
        }

        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<EventOption> EventOption { get; set; }
        public virtual DbSet<Option> Option { get; set; }
        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<UserJob> UserJob { get; set; }

        public virtual DbSet<Workhours> Workhours { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Resources.ServerSettings);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IsPrivate).HasColumnName("IS_PRIVATE");

                entity.Property(e => e.End)
                    .HasColumnName("END")
                    .HasColumnType("datetime");

                entity.Property(e => e.Location)
                    .HasColumnName("LOCATION")
                    .HasMaxLength(30);

                entity.Property(e => e.Start)
                    .HasColumnName("START")
                    .HasColumnType("datetime");

                entity.Property(e => e.Summary)
                    .HasColumnName("SUMMARY")
                    .HasMaxLength(30);

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.Property(e => e.JobId).HasColumnName("JOB_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.UserId);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.JobId);
            });

            modelBuilder.Entity<EventOption>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EventId).HasColumnName("EVENT_ID");

                entity.Property(e => e.OptionId).HasColumnName("OPTION_ID");

                entity.Property(e => e.Value)
                    .HasColumnName("VALUE")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventOption)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_Event_EventOption");

                entity.HasOne(d => d.Option)
                    .WithMany(p => p.EventOption)
                    .HasForeignKey(d => d.OptionId)
                    .HasConstraintName("FK_Option_EventOption");
            });

            modelBuilder.Entity<Option>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(30);

                entity.Property(e => e.ElementType).HasColumnName("ELEMENT_TYPE");

                entity.Property(e => e.OptionId).HasColumnName("OPTION_ID");

                entity.Property(e => e.IsMandatory).HasColumnName("IS_MANDATORY");

                entity.Property(e => e.OrganizationId).HasColumnName("ORGANIZATION_ID");

                entity.Property(e => e.PositionOrder).HasColumnName("POSITION_ORDER");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnName("TEXT")
                    .HasMaxLength(30);

                entity.Property(e => e.TimeModifier).HasColumnName("TIME_MODIFIER");

                entity.HasOne(d => d.OptionNavigation)
                    .WithMany(p => p.InverseOptionNavigation)
                    .HasForeignKey(d => d.OptionId)
                    .HasConstraintName("FK_Option_Option");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Option)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Organization_Option");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IsPrivate).HasColumnName("IS_PRIVATE");

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Emailadress)
                    .HasName("UQ__User__3A372016376838CC")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Emailadress)
                    .IsRequired()
                    .HasColumnName("EMAILADRESS")
                    .HasMaxLength(40);

                entity.Property(e => e.Firstname)
                    .HasColumnName("FIRSTNAME")
                    .HasMaxLength(40);

                entity.Property(e => e.IsAdmin).HasColumnName("IS_ADMIN");

                entity.Property(e => e.Lastname)
                    .HasColumnName("LASTNAME")
                    .HasMaxLength(40);

                entity.Property(e => e.OrganizationId).HasColumnName("ORGANIZATION_ID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("PASSWORD")
                    .HasMaxLength(65);

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK_Organization_User");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(30);

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(30);

                entity.Property(e => e.OrganizationId).HasColumnName("ORGANIZATION_ID");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Job)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK_Organization_Job");
            });

            modelBuilder.Entity<UserJob>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.Property(e => e.JobId).HasColumnName("JOB_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserJob)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_User_UserJob");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.UserJob)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_Job_UserJob");
            });

            modelBuilder.Entity<Workhours>(entity =>
            {
                entity.ToTable("WORKHOURS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Start)
                    .HasColumnName("START")
                    .HasColumnType("datetime");

                entity.Property(e => e.End)
                    .HasColumnName("END")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Workhours)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_User_Workhours");
            });
        }
    }
}
