using Microsoft.EntityFrameworkCore;
using RegistrodeEstudiantesFernandoCalderon.Datos;
using RegistrodeEstudiantesFernandoCalderon.Generales;
using System;
using System.Collections.Generic;
using System.Text;
namespace RegistrodeEstudiantesFernandoCalderon.RegistrodeEstudiantes
{
    public class Matricula
    {
        // Campos privados
        private int idEstudiante;
        private int idCurso;
        private DateTime fechaMatricula;
        private int id; // Principal

        // Propiedades con validaciones
        public int IdEstudiante
        {
            get => idEstudiante;
            set
            {
                if (value <= 0)
                    throw new Exception("El ID del estudiante debe ser mayor que cero.");
                idEstudiante = value;
            }
        }

        public int IdCurso
        {
            get => idCurso;
            set
            {
                if (value <= 0)
                    throw new Exception("El ID del curso debe ser mayor que cero.");
                idCurso = value;
            }
        }

        public DateTime FechaMatricula
        {
            get => fechaMatricula;
            set => fechaMatricula = value;
        }

        public int Id { get => id; set => id = value; }

        // 🔑 Propiedades de navegación (EF Core)
        public Estudiante? Estudiante { get; set; }   // ✅ antes era EstudianteActual
        public Curso? Curso { get; set; }             // ✅ antes era CursoActual

        // Constructor principal
        public Matricula(int idEstudiante, int idCurso, DateTime fechaMatricula)
        {
            if (idEstudiante <= 0)
                throw new Exception("El ID del estudiante debe ser mayor que cero");

            if (idCurso <= 0)
                throw new Exception("El ID del curso debe ser mayor que cero");

            this.IdEstudiante = idEstudiante;
            this.IdCurso = idCurso;
            this.FechaMatricula = fechaMatricula;
        }

        // Constructor vacío (para EF Core)
        public Matricula() { }

        // Método Imprimir
        public void Imprimir()
        {
            Console.WriteLine($"ID Matrícula: {this.Id}");

            string estudianteInfo = Estudiante != null
                ? $"{Estudiante.Nombre} (ID {this.IdEstudiante})"
                : $"ID {this.IdEstudiante}";

            string cursoInfo = Curso != null
                ? $"{Curso.Nombre} (ID {this.IdCurso})"
                : $"ID {this.IdCurso}";

            Console.WriteLine($"Estudiante: {estudianteInfo}");
            Console.WriteLine($"Curso: {cursoInfo}");
            Console.WriteLine($"Fecha de matrícula: {this.FechaMatricula:dd/MM/yyyy}");
            Console.WriteLine("------------------------------------");
        }




        // CRUD

        public static void CrearMatricula()
        {
            Console.Clear();
            Console.WriteLine("**********Crear Matrícula**********");

            Console.WriteLine("Ingrese el ID del estudiante: ");
            int idEstudiante = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Ingrese el ID del curso: ");
            int idCurso = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Ingrese la fecha de matrícula (dd/mm/yyyy): ");
            DateTime fecha = Convert.ToDateTime(Console.ReadLine());

            // Validar que el estudiante y curso existan
            using (var context = new RegistroDBContext())
            {
                var estudiante = context.Estudiantes.FirstOrDefault(e => e.Id == idEstudiante);
                var curso = context.Cursos.FirstOrDefault(c => c.Id == idCurso);

                if (estudiante == null)
                {
                    Console.WriteLine("Error: El estudiante no existe.");
                    Console.ReadLine();
                    return;
                }

                if (curso == null)
                {
                    Console.WriteLine("Error: El curso no existe.");
                    Console.ReadLine();
                    return;
                }

                // Crear matrícula
                Matricula objMatricula = new Matricula(idEstudiante, idCurso, fecha);
                context.Matriculas.Add(objMatricula);

                // 🔑 Actualizar el curso del estudiante
                estudiante.CursoId = idCurso;
                context.Estudiantes.Update(estudiante);

                // Guardar cambios en la BD
                context.SaveChanges();

                Console.WriteLine("Matrícula creada exitosamente y curso asignado al estudiante!!");
            }
            Console.ReadLine();
        }

        public static void ListarMatriculas()
        {
            Console.Clear();
            Console.WriteLine("**********Matrículas Registradas**********");

            using (var context = new RegistroDBContext())
            {
                var matriculas = context.Matriculas
                    .Include(m => m.Estudiante) // 🔑 carga estudiante
                    .Include(m => m.Curso)      // 🔑 carga curso
                    .ToList();

                foreach (Matricula matricula in matriculas)
                {
                    matricula.Imprimir();
                    Console.WriteLine("_____________________________________");
                }
            }
            Console.ReadLine();
        }

        public static void BuscarMatricula()
        {
            Console.Clear();
            Console.WriteLine("**********Buscar Matrícula**********");
            Console.WriteLine("Ingrese el ID de la matrícula: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            using (var context = new RegistroDBContext())
            {
                Matricula objMatricula = context.Matriculas
                    .FirstOrDefault(m => m.Id == idIngresado);

                if (objMatricula != null)
                {
                    Console.WriteLine("Matrícula Encontrada!!");
                    objMatricula.Imprimir();
                }
                else
                {
                    Console.WriteLine("Matrícula NO encontrada....");
                }
            }
            Console.ReadLine();
        }

        public static void ActualizarMatricula()
        {
            Console.Clear();
            Console.WriteLine("**********Actualizar Matrícula**********");
            Console.WriteLine("Ingrese el ID de la matrícula a actualizar: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            using (var context = new RegistroDBContext())
            {
                Matricula objMatricula = context.Matriculas
                    .FirstOrDefault(m => m.Id == idIngresado);

                if (objMatricula != null)
                {
                    Console.WriteLine("Matrícula Encontrada!!!");
                    Console.WriteLine("_____________________________________");
                    objMatricula.Imprimir();
                    Console.WriteLine("_____________________________________");

                    Console.WriteLine("Ingrese el nuevo ID del estudiante: ");
                    int nuevoEstudiante = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Ingrese el nuevo ID del curso: ");
                    int nuevoCurso = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Ingrese la nueva fecha de matrícula (dd/mm/yyyy): ");
                    DateTime nuevaFecha = Convert.ToDateTime(Console.ReadLine());

                    // Validar existencia
                    var estudiante = context.Estudiantes.FirstOrDefault(e => e.Id == nuevoEstudiante);
                    var curso = context.Cursos.FirstOrDefault(c => c.Id == nuevoCurso);

                    if (estudiante == null || curso == null)
                    {
                        Console.WriteLine("Error: Estudiante o curso no existen.");
                    }
                    else
                    {
                        objMatricula.IdEstudiante = nuevoEstudiante;
                        objMatricula.IdCurso = nuevoCurso;
                        objMatricula.FechaMatricula = nuevaFecha;

                        context.SaveChanges(); // ✅ Persistencia en SQL
                        Console.WriteLine("Matrícula actualizada exitosamente!!");
                    }
                }
                else
                {
                    Console.WriteLine("Matrícula NO encontrada...");
                }
            }
            Console.ReadLine();
        }

        public static void EliminarMatricula()
        {
            Console.Clear();
            Console.WriteLine("**********Eliminar Matrícula**********");
            Console.WriteLine("Ingrese el ID de la matrícula a eliminar: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            using (var context = new RegistroDBContext())
            {
                Matricula objMatricula = context.Matriculas
                    .FirstOrDefault(m => m.Id == idIngresado);

                if (objMatricula != null)
                {
                    objMatricula.Imprimir();
                    Console.WriteLine("¿Estás seguro que quieres eliminar esta matrícula? S/N:");
                    if (Console.ReadLine().ToUpper() == "S")
                    {
                        context.Matriculas.Remove(objMatricula);
                        context.SaveChanges(); // ✅ Persistencia en SQL
                        Console.WriteLine("Matrícula eliminada exitosamente!!");
                    }
                    else
                    {
                        Console.WriteLine("Operación cancelada!!");
                    }
                }
                else
                {
                    Console.WriteLine("Matrícula NO encontrada!!");
                }
            }
            Console.ReadLine();
        }
    }
}