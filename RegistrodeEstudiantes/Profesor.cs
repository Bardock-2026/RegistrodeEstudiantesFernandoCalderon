using Microsoft.EntityFrameworkCore;
using RegistrodeEstudiantesFernandoCalderon.Datos;
using RegistrodeEstudiantesFernandoCalderon.Generales;
using System;
using System.Collections.Generic;
using System.Text;
namespace RegistrodeEstudiantesFernandoCalderon.RegistrodeEstudiantes
{
    public class Profesor
    {
        // Campos privados
        private string nombre;
        private string materia;
        private int experiencia; // años de experiencia
        private int id; // Principal

        // Propiedades con validaciones
        public string Nombre
        {
            get => nombre;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("El nombre del profesor no puede estar vacío.");
                nombre = value;
            }
        }

        public string Materia
        {
            get => materia;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("La materia no puede estar vacía.");
                materia = value;
            }
        }

        public int Experiencia
        {
            get => experiencia;
            set
            {
                if (value < 0)
                    throw new Exception("La experiencia no puede ser negativa.");
                experiencia = value;
            }
        }

        // 🔑 ID principal (SQL lo maneja con IDENTITY)
        public int Id { get => id; set => id = value; }

        // 🔑 Propiedades de navegación (EF Core)
        public int? CursoId { get; set; }       // Clave foránea opcional
        public Curso? Curso { get; set; }       // Propiedad de navegación

        // Constructor principal
        public Profesor(string nombre, string materia, int experiencia)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception("El nombre del profesor no puede estar vacío");

            if (string.IsNullOrWhiteSpace(materia))
                throw new Exception("La materia no puede estar vacía");

            if (experiencia < 0)
                throw new Exception("La experiencia no puede ser negativa");

            this.Nombre = nombre;
            this.Materia = materia;
            this.Experiencia = experiencia;
        }

        // Constructor vacío (para EF Core)
        public Profesor() { }

        // Método Imprimir
        public void Imprimir()
        {
            Console.WriteLine($"ID: {this.Id}");
            Console.WriteLine($"Nombre del profesor: {this.Nombre}");
            Console.WriteLine($"Materia: {this.Materia}");
            Console.WriteLine($"Años de experiencia: {this.Experiencia}");

            string cursoNombre = Curso != null ? Curso.Nombre : "Sin curso";
            Console.WriteLine($"Curso asignado: {cursoNombre}");

            Console.WriteLine("------------------------------------");
        }




        // CRUD

        public static void CrearProfesor()
        {
            Console.Clear();
            Console.WriteLine("**********Crear Profesor**********");

            Console.WriteLine("Ingrese el nombre del profesor: ");
            string nombre = Console.ReadLine();

            Console.WriteLine("Ingrese la materia que dicta: ");
            string materia = Console.ReadLine();

            Console.WriteLine("Ingrese los años de experiencia: ");
            int experiencia = Convert.ToInt32(Console.ReadLine());

            // 🔑 Preguntar si quiere asignar curso
            Console.WriteLine("¿Desea asignar un curso al profesor? (s/n): ");
            string respuesta = Console.ReadLine()?.ToLower();

            int? cursoId = null;
            if (respuesta == "s")
            {
                Console.WriteLine("Ingrese el ID del curso asignado: ");
                cursoId = Convert.ToInt32(Console.ReadLine());
            }

            Profesor objProfesor = new Profesor(nombre, materia, experiencia);
            objProfesor.CursoId = cursoId;   // Puede ser null si no asigna curso

            using (var context = new RegistroDBContext())
            {
                context.Profesores.Add(objProfesor);
                context.SaveChanges(); // ✅ Persistencia en SQL
            }

            if (cursoId.HasValue)
                Console.WriteLine("Profesor creado exitosamente con curso asignado!!");
            else
                Console.WriteLine("Profesor creado exitosamente sin curso asignado!!");

            Console.ReadLine();
        }

        public static void ListarProfesores()
        {
            Console.Clear();
            Console.WriteLine("**********Profesores Registrados**********");

            using (var context = new RegistroDBContext())
            {
                var profesores = context.Profesores
                    .Include(p => p.Curso)   // 🔑 carga el curso asignado
                    .ToList();

                foreach (Profesor profesor in profesores)
                {
                    profesor.Imprimir();
                    Console.WriteLine("_____________________________________");
                }
            }
            Console.ReadLine();
        }

        public static void BuscarProfesor()
        {
            Console.Clear();
            Console.WriteLine("**********Buscar Profesor**********");
            Console.WriteLine("Ingrese el ID del profesor: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            using (var context = new RegistroDBContext())
            {
                Profesor objProfesor = context.Profesores
                    .FirstOrDefault(p => p.Id == idIngresado);

                if (objProfesor != null)
                {
                    Console.WriteLine("Profesor Encontrado!!");
                    objProfesor.Imprimir();
                }
                else
                {
                    Console.WriteLine("Profesor NO encontrado....");
                }
            }
            Console.ReadLine();
        }

        public static void ActualizarProfesor()
        {
            Console.Clear();
            Console.WriteLine("**********Actualizar Profesor**********");
            Console.WriteLine("Ingrese el ID del profesor a actualizar: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            using (var context = new RegistroDBContext())
            {
                Profesor objProfesor = context.Profesores
                    .Include(p => p.Curso) // para mostrar curso actual
                    .FirstOrDefault(p => p.Id == idIngresado);

                if (objProfesor != null)
                {
                    Console.WriteLine("Profesor Encontrado!!!");
                    Console.WriteLine("_____________________________________");
                    objProfesor.Imprimir();
                    Console.WriteLine("_____________________________________");

                    Console.WriteLine("Ingrese el nuevo nombre del profesor: ");
                    objProfesor.Nombre = Console.ReadLine();

                    Console.WriteLine("Ingrese la nueva materia: ");
                    objProfesor.Materia = Console.ReadLine();

                    Console.WriteLine("Ingrese los nuevos años de experiencia: ");
                    objProfesor.Experiencia = Convert.ToInt32(Console.ReadLine());

                    // 🔑 Preguntar si quiere asignar/cambiar/quitar curso
                    Console.WriteLine("¿Desea asignar o cambiar curso al profesor? (s/n): ");
                    string respuesta = Console.ReadLine()?.ToLower();

                    if (respuesta == "s")
                    {
                        Console.WriteLine("Ingrese el ID del curso: ");
                        int cursoId = Convert.ToInt32(Console.ReadLine());

                        var curso = context.Cursos.FirstOrDefault(c => c.Id == cursoId);
                        if (curso != null)
                        {
                            objProfesor.CursoId = cursoId;
                            Console.WriteLine($"Curso asignado: {curso.Nombre}");
                        }
                        else
                        {
                            Console.WriteLine("Curso no encontrado, no se asignó.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("¿Desea quitar el curso actual del profesor? (s/n): ");
                        string quitar = Console.ReadLine()?.ToLower();
                        if (quitar == "s")
                        {
                            objProfesor.CursoId = null; // 🔑 dejar sin curso
                            Console.WriteLine("Curso eliminado del profesor.");
                        }
                    }

                    context.SaveChanges(); // ✅ Persistencia en SQL
                    Console.WriteLine("Profesor actualizado exitosamente!!");
                }
                else
                {
                    Console.WriteLine("Profesor NO encontrado...");
                }
            }
            Console.ReadLine();
        }


        public static void EliminarProfesor()
        {
            Console.Clear();
            Console.WriteLine("**********Eliminar Profesor**********");
            Console.WriteLine("Ingrese el ID del profesor a eliminar: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            using (var context = new RegistroDBContext())
            {
                Profesor objProfesor = context.Profesores
                    .FirstOrDefault(p => p.Id == idIngresado);

                if (objProfesor != null)
                {
                    objProfesor.Imprimir();
                    Console.WriteLine("¿Estás seguro que quieres eliminar este profesor? S/N:");
                    if (Console.ReadLine().ToUpper() == "S")
                    {
                        context.Profesores.Remove(objProfesor);
                        context.SaveChanges(); // ✅ Persistencia en SQL
                        Console.WriteLine("Profesor eliminado exitosamente!!");
                    }
                    else
                    {
                        Console.WriteLine("Operación cancelada!!");
                    }
                }
                else
                {
                    Console.WriteLine("Profesor NO encontrado!!");
                }
            }
            Console.ReadLine();
        }
    }
}