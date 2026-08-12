using System;
using System.Collections.Generic;
using System.Text;
using RegistrodeEstudiantesFernandoCalderon.Generales;

namespace RegistrodeEstudiantesFernandoCalderon.RegistrodeEstudiantes
{

    public class Estudiante
    {
        // ATRIBUTOS
        private string nombre;
        private int edad;
        private string carrera;
        private int id; // Principal
        private List<Matricula> matriculas; // Relación con Matricula

        // PROPIEDADES
        public string Nombre
        {
            get => nombre;
            set
            {
                if (value == null || value.Length == 0)
                {
                    throw new Exception("El nombre del estudiante no puede estar vacío.");
                }
                nombre = value;
            }
        }

        public int Edad
        {
            get => edad;
            set
            {
                if (value <= 0)
                {
                    throw new Exception("La edad debe ser mayor que cero.");
                }
                edad = value;
            }
        }

        public string Carrera
        {
            get => carrera;
            set
            {
                if (value == null || value.Length == 0)
                {
                    throw new Exception("La carrera no puede estar vacía.");
                }
                carrera = value;
            }
        }

        public int Id { get => id; set => id = value; }

        // 🔑 Propiedades de navegación (necesarias para RegistroDBContext)
        public List<Matricula>? Matriculas { get => matriculas; set => matriculas = value; }
        public int CursoId { get; set; }              // Clave foránea
        public Curso? CursoActual { get; set; }       // Relación con Curso

        // CONSTRUCTOR PRINCIPAL
        public Estudiante(string nombre, int edad, string carrera)
        {
            if (nombre == null || nombre.Length == 0)
                throw new Exception("El nombre del estudiante no puede estar vacío.");

            if (edad <= 0)
                throw new Exception("La edad debe ser mayor que cero.");

            if (carrera == null || carrera.Length == 0)
                throw new Exception("La carrera no puede estar vacía.");

            this.Nombre = nombre;
            this.Edad = edad;
            this.Carrera = carrera;
            this.Matriculas = new List<Matricula>();
        }

        // CONSTRUCTOR VACÍO (para EF Core o inicialización manual)
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
            Console.WriteLine($"Curso asignado: {(this.CursoActual != null ? this.CursoActual.Nombre : "Sin curso")}");
            Console.WriteLine($"Matrículas registradas: {(this.Matriculas != null ? this.Matriculas.Count : 0)}");
            Console.WriteLine("------------------------------------");
        }
    

// CRUD

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

            Estudiante objEstudiante = new Estudiante(nombre, edad, carrera);
            Database.Estudiantes.Add(objEstudiante);
            Database.GuardarEstudiantes();
            Console.WriteLine("Estudiante creado exitosamente!!");
            Console.ReadLine();
        }

        public static void ListarEstudiantes()
        {
            Console.Clear();
            Console.WriteLine("**********Estudiantes Registrados**********");
            foreach (Estudiante estudiante in Database.Estudiantes)
            {
                estudiante.Imprimir();
                Console.WriteLine("_____________________________________");
            }
            Console.ReadLine();
        }

        public static void BuscarEstudiante()
        {
            Console.Clear();
            Console.WriteLine("**********Buscar Estudiante**********");
            Console.WriteLine("Ingrese el ID del estudiante: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            Estudiante objEstudiante = Database.Estudiantes.Find(e => e.Id == idIngresado);

            if (objEstudiante != null)
            {
                Console.WriteLine("Estudiante Encontrado!!");
                objEstudiante.Imprimir();
            }
            else
            {
                Console.WriteLine("Estudiante NO encontrado....");
            }
            Console.ReadLine();
        }

        public static void ActualizarEstudiante()
        {
            Console.Clear();
            Console.WriteLine("**********Actualizar Estudiante**********");
            Console.WriteLine("Ingrese el ID del estudiante a actualizar: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            Estudiante objEstudiante = Database.Estudiantes.Find(e => e.Id == idIngresado);

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
                Database.GuardarEstudiantes();

                Console.WriteLine("Estudiante actualizado exitosamente!!");
            }
            else
            {
                Console.WriteLine("Estudiante NO encontrado...");
            }
            Console.ReadLine();
        }

        public static void EliminarEstudiante()
        {
            Console.Clear();
            Console.WriteLine("**********Eliminar Estudiante**********");
            Console.WriteLine("Ingrese el ID del estudiante a eliminar: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            Estudiante objEstudiante = Database.Estudiantes.Find(e => e.Id == idIngresado);

            if (objEstudiante != null)
            {
                objEstudiante.Imprimir();
                Console.WriteLine("¿Estás seguro que quieres eliminar este estudiante? S/N:");
                if (Console.ReadLine().ToUpper() == "S")
                {
                    Database.Estudiantes.Remove(objEstudiante);
                    Database.GuardarEstudiantes();
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
            Console.ReadLine();
        }
    }
}
