using System;
using RegistrodeEstudiantesFernandoCalderon.RegistrodeEstudiantes;
using RegistrodeEstudiantesFernandoCalderon.Generales;

class Program
{
    static void Main(string[] args)
    {
        
        int opcion = 0;
        do
        {
            Console.WriteLine("          _______________________");
            Console.WriteLine("         |                       |");
            Console.WriteLine("         | PROYECTO INDIVIDUAL   |");
            Console.WriteLine("         | FERNANDO CALDERON     |");
            Console.WriteLine("         |_______________________|");
            Console.WriteLine("         |   |   |   |   |   |   |");
            Console.WriteLine("         |   |   |   |   |   |   |");
            Console.WriteLine("         |   |   |   |   |   |   |");
            Console.WriteLine("         |___|___|___|___|___|___|");
            Console.WriteLine("          |   |   |   |   |   |  ");
            Console.WriteLine("          |   |   |   |   |   |  ");
            Console.WriteLine("          |___|___|___|___|___|  ");
            Console.WriteLine("             [ REGISTRO DE ]     ");
            Console.WriteLine("             [ ESTUDIANTES ]     ");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("1. Crear Estudiante");
            Console.WriteLine("2. Listar Estudiantes");
            Console.WriteLine("3. Buscar Estudiante");
            Console.WriteLine("4. Actualizar Estudiante");
            Console.WriteLine("5. Eliminar Estudiante");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("6. Crear Profesor");
            Console.WriteLine("7. Listar Profesores");
            Console.WriteLine("8. Buscar Profesor");
            Console.WriteLine("9. Actualizar Profesor");
            Console.WriteLine("10. Eliminar Profesor");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("11. Crear Curso");
            Console.WriteLine("12. Listar Cursos");
            Console.WriteLine("13. Buscar Curso");
            Console.WriteLine("14. Actualizar Curso");
            Console.WriteLine("15. Eliminar Curso");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("16. Crear Matrícula");
            Console.WriteLine("17. Listar Matrículas");
            Console.WriteLine("18. Buscar Matrícula");
            Console.WriteLine("19. Actualizar Matrícula");
            Console.WriteLine("20. Eliminar Matrícula");
            Console.WriteLine("=====================================");
            Console.WriteLine("0. Salir");
            Console.WriteLine("=====================================");
            Console.Write("Seleccione una opción: ");
            opcion = Convert.ToInt32(Console.ReadLine());

            switch (opcion)
            {
                // --- ESTUDIANTE ---
                case 1: Estudiante.CrearEstudiante(); break;
                case 2: Estudiante.ListarEstudiantes(); break;
                case 3: Estudiante.BuscarEstudiante(); break;
                case 4: Estudiante.ActualizarEstudiante(); break;
                case 5: Estudiante.EliminarEstudiante(); break;

                // --- PROFESOR ---
                case 6: Profesor.CrearProfesor(); break;
                case 7: Profesor.ListarProfesores(); break;
                case 8: Profesor.BuscarProfesor(); break;
                case 9: Profesor.ActualizarProfesor(); break;
                case 10: Profesor.EliminarProfesor(); break;

                // --- CURSO ---
                case 11: Curso.CrearCurso(); break;
                case 12: Curso.ListarCursos(); break;
                case 13: Curso.BuscarCurso(); break;
                case 14: Curso.ActualizarCurso(); break;
                case 15: Curso.EliminarCurso(); break;

                // --- MATRÍCULA ---
                case 16: Matricula.CrearMatricula(); break;
                case 17: Matricula.ListarMatriculas(); break;
                case 18: Matricula.BuscarMatricula(); break;
                case 19: Matricula.ActualizarMatricula(); break;
                case 20: Matricula.EliminarMatricula(); break;

                case 0: Console.WriteLine("👋 Saliendo del sistema..."); break;
                default: Console.WriteLine("⚠️ Opción inválida."); Console.ReadLine(); break;
            }
        } while (opcion != 0);
    }
}