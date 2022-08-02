using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OrientalMedical.Damin.Models.Entities;

namespace OrientalMedical.Damin.Models.Context
{
    public partial class OrientalMedicalSystemDBContext : DbContext
    {
        public OrientalMedicalSystemDBContext()
        {
        }

        public OrientalMedicalSystemDBContext(DbContextOptions<OrientalMedicalSystemDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Asistente> Asistente { get; set; }
        public virtual DbSet<Ausencia> Ausencia { get; set; }
        public virtual DbSet<Citas> Citas { get; set; }
        public virtual DbSet<Especialidad> Especialidad { get; set; }
        public virtual DbSet<Paciente> Paciente { get; set; }
        public virtual DbSet<Personal> Personal { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-NGPA4BO\\SQLEXPRESS;Database=OrientalMedicalSystemDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asistente>(entity =>
            {
                entity.HasIndex(e => new { e.DoctorId, e.AsistenteId })
                    .HasName("UQ_Doctor_AsistenteFijo")
                    .IsUnique();

                entity.Property(e => e.AsistenteId).HasColumnName("AsistenteID");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cedula)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Asistente)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("Fk_Doctor_AsistenteFijo");
            });

            modelBuilder.Entity<Ausencia>(entity =>
            {
                entity.Property(e => e.AusenciaId).HasColumnName("AusenciaID");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.FechaInicio)
                    .HasColumnName("fechaInicio")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaReintegro).HasColumnType("datetime");

                entity.Property(e => e.MotivoAusencia)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Ausencia)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Ausencia_Doctor");
            });

            modelBuilder.Entity<Citas>(entity =>
            {
                entity.HasKey(e => e.CitaId)
                    .HasName("PK__Citas__F0E2D9F2C121F211");

                entity.HasIndex(e => new { e.EspecialidadId, e.DoctorId, e.PacienteId })
                    .HasName("UQ_Citas")
                    .IsUnique();

                entity.Property(e => e.CitaId).HasColumnName("CitaID");

                entity.Property(e => e.Comentario)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.FechaCita).HasColumnType("datetime");

                entity.Property(e => e.PacienteId).HasColumnName("PacienteID");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Citas)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Cita_Doctor");

                entity.HasOne(d => d.Especialidad)
                    .WithMany(p => p.Citas)
                    .HasForeignKey(d => d.EspecialidadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Cita_Especialidad");

                entity.HasOne(d => d.Paciente)
                    .WithMany(p => p.Citas)
                    .HasForeignKey(d => d.PacienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Cita_Paciente");
            });

            modelBuilder.Entity<Especialidad>(entity =>
            {
                entity.HasIndex(e => new { e.DoctorId, e.AsitenteId })
                    .HasName("UQ_Especialidad")
                    .IsUnique();

                entity.Property(e => e.AsitenteId).HasColumnName("AsitenteID");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.Especialidad1)
                    .IsRequired()
                    .HasColumnName("Especialidad")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HoraFin)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.HoraInicio)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.HasOne(d => d.Asitente)
                    .WithMany(p => p.EspecialidadAsitente)
                    .HasForeignKey(d => d.AsitenteId)
                    .HasConstraintName("Fk_Especialidad_Asistente");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.EspecialidadDoctor)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Especialidad_Doctor");
            });

            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.Property(e => e.PacienteId).HasColumnName("PacienteID");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Asistente)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Cedula)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Personal>(entity =>
            {
                entity.HasIndex(e => new { e.DoctorId, e.PersonalId })
                    .HasName("UQ_Doctor_Asistente")
                    .IsUnique();

                entity.Property(e => e.PersonalId).HasColumnName("PersonalID");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cedula)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ocupacion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.InverseDoctor)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("Fk_Doctor_Asistente");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.UsuarioId)
                    .HasName("PK__Usuarios__2B3DE798010E7CBE");

                entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

                entity.Property(e => e.AsistenteId).HasColumnName("AsistenteID");

                entity.Property(e => e.Clave)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PersonalId).HasColumnName("PersonalID");

                entity.Property(e => e.Usuario)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Asistente)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.AsistenteId)
                    .HasConstraintName("Fk_Usuarios_Asistente");

                entity.HasOne(d => d.Personal)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.PersonalId)
                    .HasConstraintName("Fk_Usuarios_Personal");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
