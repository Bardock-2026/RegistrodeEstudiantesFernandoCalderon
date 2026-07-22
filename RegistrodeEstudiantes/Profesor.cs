using System;
using System.Collections.Generic;
using System.Text;
using RegistrodeEstudiantesFernandoCalderon.Generales;
namespace RegistrodeEstudiantesFernandoCalderon.RegistrodeEstudiantes
{
    public class Profesor
    {
        // Campos privados
        private string nombre;
        private string materia;
        private int experiencia; // años de experiencia
        private int id; // Principal

        // Propiedades
        public string Nombre
        {
            get => nombre;
            set
            {
                if (value == null || value == "")
                {
                    Console.WriteLine("El nombre del profesor no puede estar vacío.");
                    return;
                }
                nombre = value;
            }
        }

        public string Materia
        {
            get => materia;
            set
            {
                if (value == null || value == "")
                {
                    Console.WriteLine("La materia no puede estar vacía.");
                    return;
                }
                materia = value;
            }
        }

        public int Experiencia
        {
            get => experiencia;
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("La experiencia no puede ser negativa.");
                    return;
                }
                experiencia = value;
            }
        }

        public int Id { get => id; set => id = value; }

        // Constructor
        public Profesor(string nombre, string materia, int experiencia)
        {
            if (nombre == null || nombre.Length == 0)
            {
                throw new Exception("El nombre del profesor no puede estar vacío");
            }

            if (materia == null || materia.Length == 0)
            {
                throw new Exception("La materia no puede estar vacía");
            }

            if (experiencia < 0)
            {
                throw new Exception("La experiencia no puede ser negativa");
            }

            this.Nombre = nombre;
            this.Materia = materia;
            this.Experiencia = experiencia;

            if (Database.Profesores.Count == 0)
            {
                this.id = 1;
            }
            else
            {
                this.id = Database.Profesores.Max(x => x.Id) + 1;
            }
        }

        // Método Imprimir
        public void Imprimir()
        {
            Console.WriteLine($"ID: {this.Id}");
            Console.WriteLine($"Nombre del profesor: {this.Nombre}");
            Console.WriteLine($"Materia: {this.Materia}");
            Console.WriteLine($"Años de experiencia: {this.Experiencia}");
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

            Profesor objProfesor = new Profesor(nombre, materia, experiencia);
            Database.Profesores.Add(objProfesor);

            Console.WriteLine("Profesor creado exitosamente!!");
            Console.ReadLine();
        }

        public static void ListarProfesores()
        {
            Console.Clear();
            Console.WriteLine("**********Profesores Registrados**********");
            foreach (Profesor profesor in Database.Profesores)
            {
                profesor.Imprimir();
                Console.WriteLine("_____________________________________");
            }
            Console.ReadLine();
        }

        public static void BuscarProfesor()
        {
            Console.Clear();
            Console.WriteLine("**********Buscar Profesor**********");
            Console.WriteLine("Ingrese el ID del profesor: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            Profesor objProfesor = Database.Profesores.Find(p => p.Id == idIngresado);

            if (objProfesor != null)
            {
                Console.WriteLine("Profesor Encontrado!!");
                objProfesor.Imprimir();
            }
            else
            {
                Console.WriteLine("Profesor NO encontrado....");
            }
            Console.ReadLine();
        }

        public static void ActualizarProfesor()
        {
            Console.Clear();
            Console.WriteLine("**********Actualizar Profesor**********");
            Console.WriteLine("Ingrese el ID del profesor a actualizar: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            Profesor objProfesor = Database.Profesores.Find(p => p.Id == idIngresado);

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

                Console.WriteLine("Profesor actualizado exitosamente!!");
            }
            else
            {
                Console.WriteLine("Profesor NO encontrado...");
            }
            Console.ReadLine();
        }

        public static void EliminarProfesor()
        {
            Console.Clear();
            Console.WriteLine("**********Eliminar Profesor**********");
            Console.WriteLine("Ingrese el ID del profesor a eliminar: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            Profesor objProfesor = Database.Profesores.Find(p => p.Id == idIngresado);

            if (objProfesor != null)
            {
                objProfesor.Imprimir();
                Console.WriteLine("¿Estás seguro que quieres eliminar este profesor? S/N:");
                if (Console.ReadLine().ToUpper() == "S")
                {
                    Database.Profesores.Remove(objProfesor);
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
            Console.ReadLine();
        }
    }
}
