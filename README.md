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
- Se actualiza database.cargardatos en program y se agregam los casos 3,4 y 5 en cliente