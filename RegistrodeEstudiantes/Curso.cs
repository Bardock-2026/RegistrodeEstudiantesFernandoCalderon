using RegistrodeEstudiantesFernandoCalderon.Datos;
using RegistrodeEstudiantesFernandoCalderon.Generales;
using System;
using System.Collections.Generic;
using System.Text;
namespace RegistrodeEstudiantesFernandoCalderon.RegistrodeEstudiantes
{
    public class Curso
    {
        // Campos privados
        private string nombre;
        private string descripcion;
        private string duracion; // texto libre
        private int id; // Principal

        // Propiedades
        public string Nombre
        {
            get => nombre;
            set
            {
                if (value == null || value.Length == 0)
                {
                    throw new Exception("El nombre del curso no puede estar vacío.");
                }
                nombre = value;
            }
        }

        public string Descripcion
        {
            get => descripcion;
            set
            {
                if (value == null || value.Length == 0)
                {
                    throw new Exception("La descripción no puede estar vacía.");
                }
                descripcion = value;
            }
        }

        public string Duracion
        {
            get => duracion;
            set
            {
                if (value == null || value.Length == 0)
                {
                    throw new Exception("La duración no puede estar vacía.");
                }
                duracion = value;
            }
        }

        public int Id { get => id; set => id = value; }

        // 🔑 Propiedades de navegación (necesarias para EF Core y RegistroDBContext)
        public List<Estudiante>? Estudiantes { get; set; }
        public List<Profesor>? Profesores { get; set; }
        public List<Matricula>? Matriculas { get; set; }

        // Constructor principal
        public Curso(string nombre, string descripcion, string duracion)
        {
            if (nombre == null || nombre.Length == 0)
                throw new Exception("El nombre del curso no puede estar vacío");

            if (descripcion == null || descripcion.Length == 0)
                throw new Exception("La descripción no puede estar vacía");

            if (duracion == null || duracion.Length == 0)
                throw new Exception("La duración no puede estar vacía");

            this.Nombre = nombre;
            this.Descripcion = descripcion;
            this.Duracion = duracion;

            // Inicializamos las listas de navegación
            this.Estudiantes = new List<Estudiante>();
            this.Profesores = new List<Profesor>();
            this.Matriculas = new List<Matricula>();
        }

        // Constructor vacío (para EF Core o inicialización manual)
        public Curso()
        {
            this.Estudiantes = new List<Estudiante>();
            this.Profesores = new List<Profesor>();
            this.Matriculas = new List<Matricula>();
        }

        // Método Imprimir
        public void Imprimir()
        {
            Console.WriteLine($"ID: {this.Id}");
            Console.WriteLine($"Nombre del curso: {this.Nombre}");
            Console.WriteLine($"Descripción: {this.Descripcion}");
            Console.WriteLine($"Duración: {this.Duracion}");
            Console.WriteLine($"Estudiantes registrados: {(this.Estudiantes != null ? this.Estudiantes.Count : 0)}");
            Console.WriteLine($"Profesores asignados: {(this.Profesores != null ? this.Profesores.Count : 0)}");
            Console.WriteLine($"Matrículas registradas: {(this.Matriculas != null ? this.Matriculas.Count : 0)}");
            Console.WriteLine("------------------------------------");
        }




        // CRUD

        public static void CrearCurso()
        {
            Console.Clear();
            Console.WriteLine("**********Crear Curso**********");

            Console.WriteLine("Ingrese el nombre del curso: ");
            string nombre = Console.ReadLine();

            Console.WriteLine("Ingrese la descripción del curso: ");
            string descripcion = Console.ReadLine();

            Console.WriteLine("Ingrese la duración del curso (ej: 12 meses, 1 semestre, 40 horas): ");
            string duracion = Console.ReadLine();

            Curso objCurso = new Curso(nombre, descripcion, duracion);

            using (var context = new RegistroDBContext())
            {
                context.Cursos.Add(objCurso);
                context.SaveChanges(); // ✅ SQL genera automáticamente el ID
            }

            Console.WriteLine("Curso creado exitosamente!!");
            Console.ReadLine();
        }

        public static void ListarCursos()
        {
            Console.Clear();
            Console.WriteLine("**********Cursos Registrados**********");

            using (var context = new RegistroDBContext())
            {
                foreach (Curso curso in context.Cursos.ToList())
                {
                    curso.Imprimir();
                    Console.WriteLine("_____________________________________");
                }
            }
            Console.ReadLine();
        }

        public static void BuscarCurso()
        {
            Console.Clear();
            Console.WriteLine("**********Buscar Curso**********");
            Console.WriteLine("Ingrese el ID del curso: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            using (var context = new RegistroDBContext())
            {
                Curso objCurso = context.Cursos
                    .FirstOrDefault(c => c.Id == idIngresado);

                if (objCurso != null)
                {
                    Console.WriteLine("Curso Encontrado!!");
                    objCurso.Imprimir();
                }
                else
                {
                    Console.WriteLine("Curso NO encontrado....");
                }
            }
            Console.ReadLine();
        }

        public static void ActualizarCurso()
        {
            Console.Clear();
            Console.WriteLine("**********Actualizar Curso**********");
            Console.WriteLine("Ingrese el ID del curso a actualizar: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            using (var context = new RegistroDBContext())
            {
                Curso objCurso = context.Cursos
                    .FirstOrDefault(c => c.Id == idIngresado);

                if (objCurso != null)
                {
                    Console.WriteLine("Curso Encontrado!!!");
                    Console.WriteLine("_____________________________________");
                    objCurso.Imprimir();
                    Console.WriteLine("_____________________________________");

                    Console.WriteLine("Ingrese el nuevo nombre del curso: ");
                    objCurso.Nombre = Console.ReadLine();

                    Console.WriteLine("Ingrese la nueva descripción: ");
                    objCurso.Descripcion = Console.ReadLine();

                    Console.WriteLine("Ingrese la nueva duración (ej: 12 meses, 1 semestre, 40 horas): ");
                    objCurso.Duracion = Console.ReadLine();

                    context.SaveChanges(); // ✅ Persistencia en SQL
                    Console.WriteLine("Curso actualizado exitosamente!!");
                }
                else
                {
                    Console.WriteLine("Curso NO encontrado...");
                }
            }
            Console.ReadLine();
        }

        public static void EliminarCurso()
        {
            Console.Clear();
            Console.WriteLine("**********Eliminar Curso**********");
            Console.WriteLine("Ingrese el ID del curso a eliminar: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            using (var context = new RegistroDBContext())
            {
                Curso objCurso = context.Cursos
                    .FirstOrDefault(c => c.Id == idIngresado);

                if (objCurso != null)
                {
                    objCurso.Imprimir();
                    Console.WriteLine("¿Estás seguro que quieres eliminar este curso? S/N:");
                    if (Console.ReadLine().ToUpper() == "S")
                    {
                        context.Cursos.Remove(objCurso);
                        context.SaveChanges(); // ✅ Persistencia en SQL
                        Console.WriteLine("Curso eliminado exitosamente!!");
                    }
                    else
                    {
                        Console.WriteLine("Operación cancelada!!");
                    }
                }
                else
                {
                    Console.WriteLine("Curso NO encontrado!!");
                }
            }
            Console.ReadLine();
        }
    }
}