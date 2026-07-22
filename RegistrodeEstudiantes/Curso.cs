using System;
using System.Collections.Generic;
using System.Text;
using RegistrodeEstudiantesFernandoCalderon.Generales;
namespace RegistrodeEstudiantesFernandoCalderon.RegistrodeEstudiantes
{
    public class Curso
    {
        // Campos privados
        private string nombre;
        private string descripcion;
        private string duracion; // ahora texto libre
        private int id; // Principal

        // Propiedades
        public string Nombre
        {
            get => nombre;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("El nombre del curso no puede estar vacío.");
                    return;
                }
                nombre = value;
            }
        }

        public string Descripcion
        {
            get => descripcion;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("La descripción no puede estar vacía.");
                    return;
                }
                descripcion = value;
            }
        }

        public string Duracion
        {
            get => duracion;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("La duración no puede estar vacía.");
                    return;
                }
                duracion = value;
            }
        }

        public int Id { get => id; set => id = value; }

        // Constructor
        public Curso(string nombre, string descripcion, string duracion)
        {
            if (string.IsNullOrEmpty(nombre))
                throw new Exception("El nombre del curso no puede estar vacío");

            if (string.IsNullOrEmpty(descripcion))
                throw new Exception("La descripción no puede estar vacía");

            if (string.IsNullOrEmpty(duracion))
                throw new Exception("La duración no puede estar vacía");

            this.Nombre = nombre;
            this.Descripcion = descripcion;
            this.Duracion = duracion;

            if (Database.Cursos.Count == 0)
            {
                this.id = 1;
            }
            else
            {
                this.id = Database.Cursos.Max(x => x.Id) + 1;
            }
        }

        // Método Imprimir
        public void Imprimir()
        {
            Console.WriteLine($"ID: {this.Id}");
            Console.WriteLine($"Nombre del curso: {this.Nombre}");
            Console.WriteLine($"Descripción: {this.Descripcion}");
            Console.WriteLine($"Duración: {this.Duracion}");
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
            Database.Cursos.Add(objCurso);

            Console.WriteLine("Curso creado exitosamente!!");
            Console.ReadLine();
        }

        public static void ListarCursos()
        {
            Console.Clear();
            Console.WriteLine("**********Cursos Registrados**********");
            foreach (Curso curso in Database.Cursos)
            {
                curso.Imprimir();
                Console.WriteLine("_____________________________________");
            }
            Console.ReadLine();
        }

        public static void BuscarCurso()
        {
            Console.Clear();
            Console.WriteLine("**********Buscar Curso**********");
            Console.WriteLine("Ingrese el ID del curso: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            Curso objCurso = Database.Cursos.Find(c => c.Id == idIngresado);

            if (objCurso != null)
            {
                Console.WriteLine("Curso Encontrado!!");
                objCurso.Imprimir();
            }
            else
            {
                Console.WriteLine("Curso NO encontrado....");
            }
            Console.ReadLine();
        }

        public static void ActualizarCurso()
        {
            Console.Clear();
            Console.WriteLine("**********Actualizar Curso**********");
            Console.WriteLine("Ingrese el ID del curso a actualizar: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            Curso objCurso = Database.Cursos.Find(c => c.Id == idIngresado);

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

                Console.WriteLine("Curso actualizado exitosamente!!");
            }
            else
            {
                Console.WriteLine("Curso NO encontrado...");
            }
            Console.ReadLine();
        }

        public static void EliminarCurso()
        {
            Console.Clear();
            Console.WriteLine("**********Eliminar Curso**********");
            Console.WriteLine("Ingrese el ID del curso a eliminar: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            Curso objCurso = Database.Cursos.Find(c => c.Id == idIngresado);

            if (objCurso != null)
            {
                objCurso.Imprimir();
                Console.WriteLine("¿Estás seguro que quieres eliminar este curso? S/N:");
                if (Console.ReadLine().ToUpper() == "S")
                {
                    Database.Cursos.Remove(objCurso);
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
            Console.ReadLine();
        }
    }
}