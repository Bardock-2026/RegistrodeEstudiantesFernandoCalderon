# RegistrodeEstudiantesFernandoCalderon

21/07/2026
Proyecto Individual RegistroEstudiantes:
- Se crean las clases principales Estudiante, Profesor, Curso y Matricula con ID único como buscador.
- Se implementa la clase Database en carpeta Generales para manejar listas globales y persistencia en JSON.
- Se agrega la clase ArchivoJson con métodos genéricos Cargar<T>() y Guardar<T>(), usando JsonSerializer con opciones WriteIndented y PropertyNameCaseInsensitive.
- Se configuran validaciones simples dentro de propiedades get/set, sin IsNullOrWhiteSpace ni excepciones, con mensajes claros en consola.
- Se desarrollan constructores con parámetros en todas las clases para inicializar objetos de manera ordenada.
- Se implementan métodos CRUD completos (Crear, Leer, Actualizar, Eliminar) para Estudiante, Profesor, Curso y Matricula, integrados con Database.
- Se añaden métodos Imprimir() en cada clase para mostrar datos en consola de forma clara y legible.
- Se asegura la persistencia automática en JSON al realizar operaciones CRUD, manteniendo la información guardada entre ejecuciones.
- Se organiza la estructura del proyecto con carpetas y clases separadas, siguiendo buenas prácticas de POO.
- Se incluye un banner visual en Program.cs con Console.WriteLine simulando una portada del proyecto, mostrando 'Proyecto Individual Fernando Calderon' y 'Registro de Estudiantes'."

24/07/2026
Fernando Calderon
Añadidas llamadas a persistencia en Database para todas las entidades del Registro de Estudiantes
- Clase Estudiante: se agregó Database.GuardarEstudiantes() al crear/modificar registros
- Clase Profesor: se agregó Database.GuardarProfesores() al crear/modificar registros
- Clase Curso: se agregó Database.GuardarCursos() al crear/modificar registros
- Clase Matricula: se agregó Database.GuardarMatriculas() al crear/modificar registros

Con este cambio, cada operación CRUD asegura la persistencia inmediata en el archivo JSON global.

Fernando Calderon 
12/08/2026
Validaciones con throw new Exception en Estudiante, Profesor, Curso y Matricula. 
- Eliminado uso de Console.WriteLine en setters.
- Eliminado uso de IsNullOrEmpty e IsNullOrWhiteSpace.
- Validaciones consistentes con value == null || value.Length == 0 para cadenas.
- Validaciones numéricas con comparaciones directas (<= 0).
- Constructor refuerza reglas de las propiedades.
- CRUD mantiene IDs autoincrementales desde Database.

Agrego clases Estudiante, Profesor, Curso y Matricula con validaciones y propiedades de navegación

Configuro RegistroDBContext con DbSet y relaciones entre entidades

Corrijo relación Estudiante-Matricula cambiando DeleteBehavior de Cascade a Restrict para evitar múltiples cascade paths

Elimino migraciones antiguas y base de datos para regenerar esquema limpio

Genero migración InicialRestrict con relaciones correctas y aplico Update-Database

Corrijo CRUD de Estudiante para trabajar con SQL Server usando ID automático y relación CursoId nullable

Implemento CRUD de Profesor con SQL Server usando ID automático y operaciones por ID

Creo migración ProfesorCursoOptional para permitir CursoId nullable y corregir relación con Cursos

Implemento CRUD de Curso con SQL Server usando ID automático y operaciones por ID

Verifico que CRUD de Curso funciona correctamente sin errores de integridad referencial

