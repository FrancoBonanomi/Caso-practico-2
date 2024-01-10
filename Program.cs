/* implementa un sistema de gestion de proyectos en c#. Crea clases para representar proyectos con propiedades como nombre, lista de tareas y estado de finalizacion del proyecto.                                                                Implementa una clase "GestorProyectos" que permita agregar proyectos, agregar tareas a un proyecto, marcar tareas como completadas y listar todos los proyectos junto con su estado.                        Utiliza  excepciones para manejar situaciones como intentar marcar como completada una tarea ya finalizada o intentar agregar una tarea a un proyecto que ya esta finalizado.                                                          En el programa principal, crea instancias de la clase "GestorProyectos", agrega algunos proyectos de ejemplo, realiza operaciones como agrega tareas y marcarlas como completadas y finalmente muestra el estado actual de todos los proyectos.                                                                                                                    NOTA: Este desafio se centra en la gestion de objetos, manejo de excepciones y operaciones avanzadas, con un enfoque especifico en proyectos y tareas. No es necesario implementar una persistencia de datos (por ejemplo en una base de datos).  */





using System;
using System.Collections.Generic;

//class Tarea: Define una nueva clase llamada Tarea. En la programación orientada a objetos, una clase es una plantilla para crear objetos.

class Tarea
{
// Propiedad 'Nombre': Representa el nombre de la tarea.
    public string Nombre { get; set; }

// Propiedad 'Completada': Representa el estado de completado de la tarea.

    public bool Completada { get; set; }
}


//class Proyecto: Define una nueva clase llamada Proyecto.


class Proyecto
{

  // Propiedad 'Nombre': Representa el nombre del proyecto.
    public string Nombre { get; set; }

     // Propiedad 'Tareas': Representa una lista de tareas asociadas al proyecto.
    public List<Tarea> Tareas { get; set; }

    // Propiedad 'Finalizado': Indica si el proyecto ha sido finalizado o no.
    public bool Finalizado { get; set; }

     // Constructor: Se ejecuta al crear una instancia de la clase 'Proyecto'.

    public Proyecto()
    {
        Tareas = new List<Tarea>();
    }
}

class GestorProyectos
{
   // Lista privada para almacenar proyectos gestionados por el GestorProyectos.
    private List<Proyecto> proyectos;

     // Constructor: Se ejecuta al crear una instancia de la clase 'GestorProyectos'.

    public GestorProyectos()
    {
        proyectos = new List<Proyecto>();
    }

    // Método para agregar un proyecto a la lista de proyectos.

    public void AgregarProyecto(Proyecto proyecto)
    {
        proyectos.Add(proyecto);
    }

     // Método para agregar una tarea a un proyecto específico.

    public void AgregarTareaAProyecto(string nombreProyecto, Tarea tarea)
    {
        var proyecto = ObtenerProyecto(nombreProyecto);

        if (proyecto.Finalizado)
        {
            throw new InvalidOperationException("No se pueden agregar tareas a un proyecto finalizado.");
        }

        proyecto.Tareas.Add(tarea);
    }

    // Método para marcar una tarea como completada en un proyecto específico.

    public void MarcarTareaComoCompletada(string nombreProyecto, string nombreTarea)
    {
        var proyecto = ObtenerProyecto(nombreProyecto);
        var tarea = ObtenerTarea(proyecto, nombreTarea);

        if (tarea.Completada)
        {
            throw new InvalidOperationException("La tarea ya está marcada como completada.");
        }

        tarea.Completada = true;
    }
    // Método para listar todos los proyectos y sus tareas.

    public void ListarProyectos()
    {
        foreach (var proyecto in proyectos)
        {
            Console.WriteLine($"Proyecto: {proyecto.Nombre}, Estado: {(proyecto.Finalizado ? "Finalizado" : "En Progreso")}");

            foreach (var tarea in proyecto.Tareas)
            {
                Console.WriteLine($"  Tarea: {tarea.Nombre}, Completada: {(tarea.Completada ? "Sí" : "No")}");
            }

            Console.WriteLine();
        }
    }

    // Método privado para obtener un proyecto por su nombre.

    private Proyecto ObtenerProyecto(string nombreProyecto)
    {
        var proyecto = proyectos.Find(p => p.Nombre == nombreProyecto);

        if (proyecto == null)
        {
            throw new InvalidOperationException("Proyecto no encontrado.");
        }

        return proyecto;
    }

     // Método privado para obtener una tarea por su nombre en un proyecto específico.

    private Tarea ObtenerTarea(Proyecto proyecto, string nombreTarea)
    {
        var tarea = proyecto.Tareas.Find(t => t.Nombre == nombreTarea);

        if (tarea == null)
        {
            throw new InvalidOperationException("Tarea no encontrada.");
        }

        return tarea;
    }
}

class Program
{
    static void Main()
    {
        // Ejemplo de uso

       /*   Se crea una instancia del objeto GestorProyectos llamado gestor. Esta instancia representa el gestor que se utilizará para administrar los proyectos. */
        GestorProyectos gestor = new GestorProyectos();

      /*   Se crea una instancia del objeto Proyecto llamado proyecto1 y se le asigna el nombre "Proyecto A". Este proyecto se agrega al gestor en el siguiente paso. */

        Proyecto proyecto1 = new Proyecto { Nombre = "Proyecto A" };

        //Se agrega el proyecto1 al gestor mediante el método AgregarProyecto de la clase GestorProyectos.
        gestor.AgregarProyecto(proyecto1);

        //Se agrega una tarea llamada "Tarea 1" al proyecto "Proyecto A" utilizando el método AgregarTareaAProyecto del gestor.

        gestor.AgregarTareaAProyecto("Proyecto A", new Tarea { Nombre = "Tarea 1" });

         //Se agrega otra tarea llamada "Tarea 2" al mismo proyecto.
        gestor.AgregarTareaAProyecto("Proyecto A", new Tarea { Nombre = "Tarea 2" });
        gestor.MarcarTareaComoCompletada("Proyecto A", "Tarea 2" );
        //gestor.MarcarTareaComoCompletada("Proyecto A", "Tarea 9" );
        gestor.AgregarTareaAProyecto("proyecto z",new Tarea {Nombre="Tarea 6"});

        
       
        
        

        //Se marca el proyecto1 como finalizado, lo que simula un proyecto que ha sido completado.

        proyecto1.Finalizado = true;

        // Se intenta agregar otra tarea al proyecto1 después de que ha sido marcado como finalizado. Esto genera una excepción InvalidOperationException porque no se pueden agregar tareas a un proyecto finalizado. La excepción es capturada y se imprime un mensaje de error.
        try
        {
            gestor.AgregarTareaAProyecto("Proyecto A", new Tarea { Nombre = "Tarea 3" });
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

       /*  se llama al método ListarProyectos del gestor para mostrar en la consola el estado actual de todos los proyectos y sus tareas asociadas. Este paso ayuda a verificar que las operaciones anteriores se hayan realizado correctamente. */

        gestor.ListarProyectos();
       
    }
}