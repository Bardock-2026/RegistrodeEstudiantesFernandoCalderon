using System;
using System.Collections.Generic;
using System.Text;
using RegistrodeEstudiantesFernandoCalderon.Generales;
namespace RegistrodeEstudiantesFernandoCalderon.RegistrodeEstudiantes
{
    public class Matricula
    {
        // Campos privados
        private int idEstudiante;
        private int idCurso;
        private DateTime fechaMatricula;
        private int id; // Principal

        // Propiedades
        public int IdEstudiante
        {
            get => idEstudiante;
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("El ID del estudiante debe ser mayor que cero.");
                    return;
                }
                idEstudiante = value;
            }
        }

        public int IdCurso
        {
            get => idCurso;
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("El ID del curso debe ser mayor que cero.");
                    return;
                }
                idCurso = value;
            }
        }

        public DateTime FechaMatricula
        {
            get => fechaMatricula;
            set => fechaMatricula = value;
        }

        public int Id { get => id; set => id = value; }

        // Constructor
        public Matricula(int idEstudiante, int idCurso, DateTime fechaMatricula)
        {
            if (idEstudiante <= 0)
            {
                throw new Exception("El ID del estudiante debe ser mayor que cero");
            }

            if (idCurso <= 0)
            {
                throw new Exception("El ID del curso debe ser mayor que cero");
            }

            this.IdEstudiante = idEstudiante;
            this.IdCurso = idCurso;
            this.FechaMatricula = fechaMatricula;

            if (Database.Matriculas.Count == 0)
            {
                this.id = 1;
            }
            else
            {
                this.id = Database.Matriculas.Max(x => x.Id) + 1;
            }
        }

        // Método Imprimir
        public void Imprimir()
        {
            // Buscar estudiante por ID
            Estudiante estudiante = Database.Estudiantes.Find(e => e.Id == this.IdEstudiante);
            string nombreEstudiante = estudiante != null ? estudiante.Nombre : "Estudiante no encontrado";

            // Buscar curso por ID
            Curso curso = Database.Cursos.Find(c => c.Id == this.IdCurso);
            string nombreCurso = curso != null ? curso.Nombre : "Curso no encontrado";

            Console.WriteLine($"ID Matrícula: {this.Id}");
            Console.WriteLine($"Estudiante: {nombreEstudiante} (ID {this.IdEstudiante})");
            Console.WriteLine($"Curso: {nombreCurso} (ID {this.IdCurso})");
            Console.WriteLine($"Fecha de matrícula: {this.FechaMatricula.ToShortDateString()}");
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

            Matricula objMatricula = new Matricula(idEstudiante, idCurso, fecha);
            Database.Matriculas.Add(objMatricula);

            Console.WriteLine("Matrícula creada exitosamente!!");
            Console.ReadLine();
        }

        public static void ListarMatriculas()
        {
            Console.Clear();
            Console.WriteLine("**********Matrículas Registradas**********");
            foreach (Matricula matricula in Database.Matriculas)
            {
                matricula.Imprimir();
                Console.WriteLine("_____________________________________");
            }
            Console.ReadLine();
        }

        public static void BuscarMatricula()
        {
            Console.Clear();
            Console.WriteLine("**********Buscar Matrícula**********");
            Console.WriteLine("Ingrese el ID de la matrícula: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            Matricula objMatricula = Database.Matriculas.Find(m => m.Id == idIngresado);

            if (objMatricula != null)
            {
                Console.WriteLine("Matrícula Encontrada!!");
                objMatricula.Imprimir();
            }
            else
            {
                Console.WriteLine("Matrícula NO encontrada....");
            }
            Console.ReadLine();
        }

        public static void ActualizarMatricula()
        {
            Console.Clear();
            Console.WriteLine("**********Actualizar Matrícula**********");
            Console.WriteLine("Ingrese el ID de la matrícula a actualizar: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            Matricula objMatricula = Database.Matriculas.Find(m => m.Id == idIngresado);

            if (objMatricula != null)
            {
                Console.WriteLine("Matrícula Encontrada!!!");
                Console.WriteLine("_____________________________________");
                objMatricula.Imprimir();
                Console.WriteLine("_____________________________________");

                Console.WriteLine("Ingrese el nuevo ID del estudiante: ");
                objMatricula.IdEstudiante = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Ingrese el nuevo ID del curso: ");
                objMatricula.IdCurso = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Ingrese la nueva fecha de matrícula (dd/mm/yyyy): ");
                objMatricula.FechaMatricula = Convert.ToDateTime(Console.ReadLine());

                Console.WriteLine("Matrícula actualizada exitosamente!!");
            }
            else
            {
                Console.WriteLine("Matrícula NO encontrada...");
            }
            Console.ReadLine();
        }

        public static void EliminarMatricula()
        {
            Console.Clear();
            Console.WriteLine("**********Eliminar Matrícula**********");
            Console.WriteLine("Ingrese el ID de la matrícula a eliminar: ");
            int idIngresado = Convert.ToInt32(Console.ReadLine());

            Matricula objMatricula = Database.Matriculas.Find(m => m.Id == idIngresado);

            if (objMatricula != null)
            {
                objMatricula.Imprimir();
                Console.WriteLine("¿Estás seguro que quieres eliminar esta matrícula? S/N:");
                if (Console.ReadLine().ToUpper() == "S")
                {
                    Database.Matriculas.Remove(objMatricula);
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
            Console.ReadLine();
        }
    }
}
