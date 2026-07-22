using RegistrodeEstudiantesFernandoCalderon.RegistrodeEstudiantes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegistrodeEstudiantesFernandoCalderon.Generales
{
    public static class Database
    {
        private static readonly string rutaCarpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Datos");
        private static readonly string rutaArchivoEstudiantes = Path.Combine(rutaCarpeta, "Estudiantes.json");
        private static readonly string rutaArchivoProfesores = Path.Combine(rutaCarpeta, "Profesores.json");
        private static readonly string rutaArchivoCursos = Path.Combine(rutaCarpeta, "Cursos.json");
        private static readonly string rutaArchivoMatriculas = Path.Combine(rutaCarpeta, "Matriculas.json");

        public static List<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
        public static List<Profesor> Profesores { get; set; } = new List<Profesor>();
        public static List<Curso> Cursos { get; set; } = new List<Curso>();
        public static List<Matricula> Matriculas { get; set; } = new List<Matricula>();

        public static void CargarDatos()
        {
            if (!Directory.Exists(rutaCarpeta))
            {
                Directory.CreateDirectory(rutaCarpeta);
            }

            Estudiantes = ArchivoJson.Cargar<Estudiante>(rutaArchivoEstudiantes);
            Profesores = ArchivoJson.Cargar<Profesor>(rutaArchivoProfesores);
            Cursos = ArchivoJson.Cargar<Curso>(rutaArchivoCursos);
            Matriculas = ArchivoJson.Cargar<Matricula>(rutaArchivoMatriculas);
        }

        public static void GuardarDatos()
        {
            ArchivoJson.Guardar(rutaArchivoEstudiantes, Estudiantes);
            ArchivoJson.Guardar(rutaArchivoProfesores, Profesores);
            ArchivoJson.Guardar(rutaArchivoCursos, Cursos);
            ArchivoJson.Guardar(rutaArchivoMatriculas, Matriculas);
        }

        public static void GuardarEstudiantes()
        {
            ArchivoJson.Guardar(rutaArchivoEstudiantes, Estudiantes);
        }

        public static void GuardarProfesores()
        {
            ArchivoJson.Guardar(rutaArchivoProfesores, Profesores);
        }

        public static void GuardarCursos()
        {
            ArchivoJson.Guardar(rutaArchivoCursos, Cursos);
        }

        public static void GuardarMatriculas()
        {
            ArchivoJson.Guardar(rutaArchivoMatriculas, Matriculas);
        }
    }
}