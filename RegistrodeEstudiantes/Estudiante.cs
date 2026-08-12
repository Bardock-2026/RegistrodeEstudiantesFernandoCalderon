using Microsoft.EntityFrameworkCore;
using RegistrodeEstudiantesFernandoCalderon.Datos;
using RegistrodeEstudiantesFernandoCalderon.Generales;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegistrodeEstudiantesFernandoCalderon.RegistrodeEstudiantes
{

    public class Estudiante
    {
        // ATRIBUTOS PRIVADOS
        private string nombre;
        private int edad;
        private string carrera;
        private int id; // Principal

        // PROPIEDADES CON VALIDACIONES
        public string Nombre
        {
            get => nombre;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("El nombre del estudiante no puede estar vacío.");
                nombre = value;
            }
        }

        public int Edad
        {
            get => edad;
            set
            {
                if (value <= 0)
                    throw new Exception("La edad debe ser mayor que cero.");
                edad = value;
            }
        }

        public string Carrera
        {
            get => carrera;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("La carrera no puede estar vacía.");
                carrera = value;
            }
        }

        // 🔑 ID principal (SQL lo maneja con IDENTITY)
        public int Id { get => id; set => id = value; }

        // 🔑 Propiedades de navegación (EF Core)
        public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
        public int? CursoId { get; set; }        // Clave foránea opcional
        public Curso? Curso { get; set; }        // Propiedad de navegación

        // CONSTRUCTOR PRINCIPAL
        public Estudiante(string nombre, int edad, string carrera)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception("El nombre del estudiante no puede estar vacío.");

            if (edad <= 0)
                throw new Exception("La edad debe ser mayor que cero.");

            if (string.IsNullOrWhiteSpace(carrera))
                throw new Exception("La carrera no puede estar vacía.");

            this.Nombre = nombre;
            this.Edad = edad;
            this.Carrera = carrera;
            this.Matriculas = new List<Matricula>();
        }

        // CONSTRUCTOR VACÍO (para EF Core)
        public Estudiante()
        {
            this.Matriculas = new List<Matricula>();
        }

        // MÉTODOS
        public void Presentar()
        {
            Console.WriteLine($"Hola, soy {this.Nombre}, tengo {this.Edad} años y estudio {this.Carrera}.");
        }

        public void Imprimir()
        {
            Console.WriteLine($"ID: {this.Id}");
            Console.WriteLine($"Nombre: {this.Nombre}");
            Console.WriteLine($"Edad: {this.Edad}");
            Console.WriteLine($"Carrera: {this.Carrera}");
            Console.WriteLine($"Curso asignado: {(this.Curso != null ? this.Curso.Nombre : "Sin curso")}");
            Console.WriteLine($"Matrículas registradas: {this.Matriculas.Count}");
            Console.WriteLine("------------------------------------");
        }



        //CRUD
        public static void CrearEstudiante()
        {
            Console.Clear();
            Console.WriteLine("**********Crear Estudiante**********");

            Console.WriteLine("Ingrese el nombre del estudiante: ");
            string nombre = Console.ReadLine();

            Console.WriteLine("Ingrese la edad del estudiante: ");
            int edad = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Ingrese la carrera del estudiante: ");
            string carrera = Console.ReadLine();

            Console.WriteLine("Ingrese el ID del curso asignado: ");
            int cursoId = Convert.ToInt32(Console.ReadLine());

            Estudiante objEstudiante = new Estudiante(nombre, edad, carrera);
            objEstudiante.CursoId = cursoId;   // 🔑 asignar curso

            using (var context = new RegistroDBContext())
            {
                context.Estudiantes.Add(objEstudiante);
                context.SaveChanges(); // ✅ SQL genera automáticamente el ID del estudiante
            }

            Console.WriteLine("Estudiante creado exitosamente con curso asignado!!");
            Console.ReadLine();
        }

        public static void ListarEstudiantes()
        {
            Console.Clear();
            Console.WriteLine("**********Estudiantes Registrados**********");

            using (var context = new RegistroDBContext())
            {
                var estudiantes = context.Estudiantes
                    .Include(e => e.Curso)                // curso asignado al estudiante
                    .Include(e => e.Matriculas)           // matrículas del estudiante
                        .ThenInclude(m => m.Curso)        // curso de cada matrícula
                    .ToList();

                foreach (var estudiante in estudiantes)
                {
                    estudiante.Imprimir();

                    foreach (var matricula in estudiante.Matriculas)
                    {
                        Console.WriteLine($" - Matrícula en curso: {matricula.Curso?.Nombre ?? "Sin curso"}");
                    }
                }
            
        }
            Console.ReadLine();
        }

        public static void BuscarEstudiante()
        {
            Console.Clear();
            Console.WriteLine("**********Buscar Estudiante**********");
            Console.WriteLine("Ingrese el ID del estudiante: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            using (var context = new RegistroDBContext())
            {
                Estudiante objEstudiante = context.Estudiantes
                    .FirstOrDefault(e => e.Id == idIngresado);

                if (objEstudiante != null)
                {
                    Console.WriteLine("Estudiante Encontrado!!");
                    objEstudiante.Imprimir();
                }
                else
                {
                    Console.WriteLine("Estudiante NO encontrado....");
                }
            }
            Console.ReadLine();
        }

        public static void ActualizarEstudiante()
        {
            Console.Clear();
            Console.WriteLine("**********Actualizar Estudiante**********");
            Console.WriteLine("Ingrese el ID del estudiante a actualizar: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            using (var context = new RegistroDBContext())
            {
                Estudiante objEstudiante = context.Estudiantes
                    .FirstOrDefault(e => e.Id == idIngresado);

                if (objEstudiante != null)
                {
                    Console.WriteLine("Estudiante Encontrado!!!");
                    Console.WriteLine("_____________________________________");
                    objEstudiante.Imprimir();
                    Console.WriteLine("_____________________________________");

                    Console.WriteLine("Ingrese el nuevo nombre del estudiante: ");
                    objEstudiante.Nombre = Console.ReadLine();

                    Console.WriteLine("Ingrese la nueva edad del estudiante: ");
                    objEstudiante.Edad = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Ingrese la nueva carrera del estudiante: ");
                    objEstudiante.Carrera = Console.ReadLine();

                    context.SaveChanges(); // ✅ Persistencia en SQL
                    Console.WriteLine("Estudiante actualizado exitosamente!!");
                }
                else
                {
                    Console.WriteLine("Estudiante NO encontrado...");
                }
            }
            Console.ReadLine();
        }

        public static void EliminarEstudiante()
        {
            Console.Clear();
            Console.WriteLine("**********Eliminar Estudiante**********");
            Console.WriteLine("Ingrese el ID del estudiante a eliminar: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            using (var context = new RegistroDBContext())
            {
                Estudiante objEstudiante = context.Estudiantes
                    .FirstOrDefault(e => e.Id == idIngresado);

                if (objEstudiante != null)
                {
                    objEstudiante.Imprimir();
                    Console.WriteLine("¿Estás seguro que quieres eliminar este estudiante? S/N:");
                    if (Console.ReadLine().ToUpper() == "S")
                    {
                        context.Estudiantes.Remove(objEstudiante);
                        context.SaveChanges(); // ✅ Persistencia en SQL
                        Console.WriteLine("Estudiante eliminado exitosamente!!");
                    }
                    else
                    {
                        Console.WriteLine("Operación cancelada!!");
                    }
                }
                else
                {
                    Console.WriteLine("Estudiante NO encontrado!!");
                }
            }
            Console.ReadLine();
        }
    }
}