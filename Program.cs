using System;
using RegistrodeEstudiantesFernandoCalderon.RegistrodeEstudiantes;
using RegistrodeEstudiantesFernandoCalderon.Generales;

class Program
{
    static void Main(string[] args)
    {
        Database.CargarDatos();
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
                case 1: Estudiante.CrearEstudiante(); Database.GuardarEstudiantes(); break;
                case 2: Estudiante.ListarEstudiantes(); break;
                case 3: Estudiante.BuscarEstudiante(); break;
                case 4: Estudiante.ActualizarEstudiante(); Database.GuardarEstudiantes(); break;
                case 5: Estudiante.EliminarEstudiante(); Database.GuardarEstudiantes(); break;

                // --- PROFESOR ---
                case 6: Profesor.CrearProfesor(); Database.GuardarProfesores(); break;
                case 7: Profesor.ListarProfesores(); break;
                case 8: Profesor.BuscarProfesor(); break;
                case 9: Profesor.ActualizarProfesor(); Database.GuardarProfesores(); break;
                case 10: Profesor.EliminarProfesor(); Database.GuardarProfesores(); break;

                // --- CURSO ---
                case 11: Curso.CrearCurso(); Database.GuardarCursos(); break;
                case 12: Curso.ListarCursos(); break;
                case 13: Curso.BuscarCurso(); break;
                case 14: Curso.ActualizarCurso(); Database.GuardarCursos(); break;
                case 15: Curso.EliminarCurso(); Database.GuardarCursos(); break;

                // --- MATRÍCULA ---
                case 16: Matricula.CrearMatricula(); Database.GuardarMatriculas(); break;
                case 17: Matricula.ListarMatriculas(); break;
                case 18: Matricula.BuscarMatricula(); break;
                case 19: Matricula.ActualizarMatricula(); Database.GuardarMatriculas(); break;
                case 20: Matricula.EliminarMatricula(); Database.GuardarMatriculas(); break;

                case 0: Console.WriteLine("👋 Saliendo del sistema..."); break;
                default: Console.WriteLine("⚠️ Opción inválida."); Console.ReadLine(); break;
            }
        } while (opcion != 0);
    }
}