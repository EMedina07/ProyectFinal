using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OrientalMedical.Damin.Entities
{
    public partial class OrientalMedicalDBContext : DbContext
    {
        public OrientalMedicalDBContext()
        {
        }

        public OrientalMedicalDBContext(DbContextOptions<OrientalMedicalDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Citas> Citas { get; set; }
        public virtual DbSet<Especialidad> Especialidad { get; set; }
        public virtual DbSet<Paciente> Paciente { get; set; }
        public virtual DbSet<Personal> Personal { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-NGPA4BO\\SQLEXPRESS;Database=OrientalMedicalDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Citas>(entity =>
            {
                entity.HasKey(e => e.CitaId)
                    .HasName("PK__Citas__F0E2D9F2FB321D0E");

                entity.Property(e => e.CitaId).HasColumnName("CitaID");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.PacienteId).HasColumnName("PacienteID");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Citas)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Citas__DoctorID__45F365D3");

                entity.HasOne(d => d.Paciente)
                    .WithMany(p => p.Citas)
                    .HasForeignKey(d => d.PacienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Citas__PacienteI__46E78A0C");
            });

            modelBuilder.Entity<Especialidad>(entity =>
            {
                entity.HasIndex(e => new { e.Especialidad1, e.DoctorId, e.SecreteriaId })
                    .HasName("UQ_Especialidad_DoctorID_SecreteriaID")
                    .IsUnique();

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.Especialidad1)
                    .IsRequired()
                    .HasColumnName("Especialidad")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SecreteriaId).HasColumnName("SecreteriaID");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.EspecialidadDoctor)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Especiali__Docto__3F466844");

                entity.HasOne(d => d.Secreteria)
                    .WithMany(p => p.EspecialidadSecreteria)
                    .HasForeignKey(d => d.SecreteriaId)
                    .HasConstraintName("FK__Especiali__Secre__403A8C7D");
            });

            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.HasIndex(e => e.Cedula)
                    .HasName("UQ__Paciente__B4ADFE3872E18A44")
                    .IsUnique();

                entity.Property(e => e.PacienteId).HasColumnName("PacienteID");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cedula)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Direcion)
                    .IsRequired()
                    .HasMaxLength(50)
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
                entity.HasIndex(e => e.Cedula)
                    .HasName("UQ__Personal__B4ADFE380A81E98D")
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
                    .HasConstraintName("FK__Personal__Doctor__37A5467C");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.UsuarioId)
                    .HasName("PK__Usuarios__2B3DE7989F35960F");

                entity.HasIndex(e => e.PersonalId)
                    .HasName("UQ__Usuarios__28343712138BA075")
                    .IsUnique();

                entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

                entity.Property(e => e.Clave)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PersonalId).HasColumnName("PersonalID");

                entity.Property(e => e.Usuario)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Personal)
                    .WithOne(p => p.Usuarios)
                    .HasForeignKey<Usuarios>(d => d.PersonalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Usuarios__Person__3B75D760");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
