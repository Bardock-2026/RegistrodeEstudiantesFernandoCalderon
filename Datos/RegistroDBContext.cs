using Microsoft.EntityFrameworkCore;
using RegistrodeEstudiantesFernandoCalderon.RegistrodeEstudiantes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegistrodeEstudiantesFernandoCalderon.Datos
{
    public class RegistroDBContext : DbContext
    {
        // 1er paso: DbSet para cada clase que se quiera mapear a la base de datos
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }

        // 2do paso: Configurar la cadena de conexión
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // CADENA CONEXIÓN USUARIO SQL SERVER
            optionsBuilder.UseSqlServer("Server=DESKTOP-DQDC13N\\SQLEXPRESS;Database=REGISTROESTUDIANTES_FCALDERON;User Id=sa;Password=1234;TrustServerCertificate=True;");
            // CADENA CONEXIÓN USUARIO WINDOWS (alternativa)
            // optionsBuilder.UseSqlServer("Server=DESKTOP-DQDC13N\\SQLEXPRESS;Database=REGISTROESTUDIANTES_FCALDERON;Trusted_Connection=True;");
        }

        // 3er paso: Configurar las relaciones entre las tablas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relación 1 a muchos entre Curso y Estudiante
            modelBuilder.Entity<Curso>()
                .HasMany(c => c.Estudiantes)
                .WithOne(e => e.CursoActual)
                .HasForeignKey(e => e.CursoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación 1 a muchos entre Curso y Profesor
            modelBuilder.Entity<Curso>()
                .HasMany(c => c.Profesores)
                .WithOne(p => p.CursoActual)
                .HasForeignKey(p => p.CursoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación 1 a muchos entre Curso y Matricula
            modelBuilder.Entity<Curso>()
                .HasMany(c => c.Matriculas)
                .WithOne(m => m.CursoActual)
                .HasForeignKey(m => m.IdCurso)
                .OnDelete(DeleteBehavior.Cascade); // ✅ Mantén cascada aquí

            // Relación 1 a muchos entre Estudiante y Matricula
            modelBuilder.Entity<Estudiante>()
                .HasMany(e => e.Matriculas)
                .WithOne(m => m.EstudianteActual)
                .HasForeignKey(m => m.IdEstudiante)
                .OnDelete(DeleteBehavior.Restrict); // 🔴 antes estaba Cascade
        }
    }
}