using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApplicationCentreFormation.Models
{
    public partial class centreFormationDbContext : DbContext
    {
        public centreFormationDbContext()
        {
        }

        public centreFormationDbContext(DbContextOptions<centreFormationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Candidat> Candidat { get; set; }
        public virtual DbSet<Formateur> Formateur { get; set; }
        public virtual DbSet<FormateurSpecialite> FormateurSpecialite { get; set; }
        public virtual DbSet<Formation> Formation { get; set; }
        public virtual DbSet<Niveau> Niveau { get; set; }
        public virtual DbSet<Session> Session { get; set; }
        public virtual DbSet<SessionCandidat> SessionCandidat { get; set; }
        public virtual DbSet<SessionFormateur> SessionFormateur { get; set; }
        public virtual DbSet<Specialite> Specialite { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=centreFormationDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidat>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Cin)
                    .IsRequired()
                    .HasColumnName("cin");

                entity.Property(e => e.Cv)
                    .IsRequired()
                    .HasColumnName("cv");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email");

                entity.Property(e => e.MotPass)
                    .IsRequired()
                    .HasColumnName("mot_pass");

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasColumnName("nom");

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasColumnName("photo");

                entity.Property(e => e.Prenom)
                    .IsRequired()
                    .HasColumnName("prenom");
            });

            modelBuilder.Entity<Formateur>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Cin)
                    .IsRequired()
                    .HasColumnName("cin");

                entity.Property(e => e.Cv)
                    .IsRequired()
                    .HasColumnName("cv");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email");

                entity.Property(e => e.MotPass)
                    .IsRequired()
                    .HasColumnName("mot_pass");

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasColumnName("nom");

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasColumnName("photo");

                entity.Property(e => e.Prenom)
                    .IsRequired()
                    .HasColumnName("prenom");

                entity.Property(e => e.TarifHoraire)
                    .IsRequired()
                    .HasColumnName("tarif_horaire");

                entity.Property(e => e.Tel)
                    .IsRequired()
                    .HasColumnName("tel");
            });

            modelBuilder.Entity<FormateurSpecialite>(entity =>
            {
                entity.HasKey(e => new { e.FormateurId, e.SpecialiteId });

                entity.HasIndex(e => e.SpecialiteId)
                    .HasName("IX_FK_FormateurSpecialite_Specialite");

                entity.Property(e => e.FormateurId).HasColumnName("Formateur_Id");

                entity.Property(e => e.SpecialiteId).HasColumnName("Specialite_Id");

                entity.HasOne(d => d.Formateur)
                    .WithMany(p => p.FormateurSpecialite)
                    .HasForeignKey(d => d.FormateurId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FormateurSpecialite_Formateur");

                entity.HasOne(d => d.Specialite)
                    .WithMany(p => p.FormateurSpecialite)
                    .HasForeignKey(d => d.SpecialiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FormateurSpecialite_Specialite");
            });

            modelBuilder.Entity<Formation>(entity =>
            {
                entity.HasIndex(e => e.NiveauId)
                    .HasName("IX_FK_NiveauFormation");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ChargeHoraire)
                    .IsRequired()
                    .HasColumnName("charge_horaire");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Programme)
                    .IsRequired()
                    .HasColumnName("programme");

                entity.Property(e => e.Titre)
                    .IsRequired()
                    .HasColumnName("titre");

                entity.HasOne(d => d.Niveau)
                    .WithMany(p => p.Formation)
                    .HasForeignKey(d => d.NiveauId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NiveauFormation");
            });

            modelBuilder.Entity<Niveau>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasColumnName("nom");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.HasIndex(e => e.FormationId)
                    .HasName("IX_FK_FormationSession");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateDeb)
                    .HasColumnName("date_deb")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateFin)
                    .HasColumnName("date_fin")
                    .HasColumnType("datetime");

                entity.Property(e => e.Planning)
                    .IsRequired()
                    .HasColumnName("planning");

                entity.HasOne(d => d.Formation)
                    .WithMany(p => p.Session)
                    .HasForeignKey(d => d.FormationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FormationSession");
            });

            modelBuilder.Entity<SessionCandidat>(entity =>
            {
                entity.HasKey(e => new { e.SessionId, e.CandidatId });

                entity.HasIndex(e => e.CandidatId)
                    .HasName("IX_FK_SessionCandidat_Candidat");

                entity.Property(e => e.SessionId).HasColumnName("Session_Id");

                entity.Property(e => e.CandidatId).HasColumnName("Candidat_Id");

                entity.HasOne(d => d.Candidat)
                    .WithMany(p => p.SessionCandidat)
                    .HasForeignKey(d => d.CandidatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SessionCandidat_Candidat");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.SessionCandidat)
                    .HasForeignKey(d => d.SessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SessionCandidat_Session");
            });

            modelBuilder.Entity<SessionFormateur>(entity =>
            {
                entity.HasKey(e => new { e.SessionId, e.FormateurId });

                entity.HasIndex(e => e.FormateurId)
                    .HasName("IX_FK_SessionFormateur_Formateur");

                entity.Property(e => e.SessionId).HasColumnName("Session_Id");

                entity.Property(e => e.FormateurId).HasColumnName("Formateur_Id");

                entity.HasOne(d => d.Formateur)
                    .WithMany(p => p.SessionFormateur)
                    .HasForeignKey(d => d.FormateurId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SessionFormateur_Formateur");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.SessionFormateur)
                    .HasForeignKey(d => d.SessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SessionFormateur_Session");
            });

            modelBuilder.Entity<Specialite>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasColumnName("nom");
            });

          
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
