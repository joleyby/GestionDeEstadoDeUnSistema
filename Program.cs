namespace SistemaGestionSolicitudes
{
       
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            GestorSolicitudes gestor = new GestorSolicitudes();
            bool continuar = true;

            MostrarEncabezado();

            while (continuar)
            {
                MostrarMenu();
                string opcion = Console.ReadLine().ToUpper();

                switch (opcion)
                {
                    case "1":
                        RegistrarSolicitud(gestor);
                        break;
                    case "2":
                        gestor.MostrarTodasLasSolicitudes();
                        break;
                    case "3":
                        BuscarSolicitud(gestor);
                        break;
                    case "4":
                        CambiarEstadoSolicitud(gestor);
                        break;
                    case "5":
                        gestor.MostrarEstadisticas();
                        break;
                    case "6":
                        continuar = false;
                        MostrarDespedida();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n❌ Opción no válida. Por favor, intente de nuevo.");
                        Console.ResetColor();
                        break;
                }

                if (continuar && opcion != "2" && opcion != "5")
                {
                    PausarPantalla();
                }
            }
        }

        static void MostrarEncabezado()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(new string('═', 70));
            Console.WriteLine("╔" + new string('═', 68) + "╗");
            Console.WriteLine("║" + " SISTEMA DE GESTIÓN DE SOLICITUDES ".PadBoth(68) + "║");
            Console.WriteLine("║" + " Versión 1.0 ".PadBoth(68) + "║");
            Console.WriteLine("╚" + new string('═', 68) + "╝");
            Console.WriteLine(new string('═', 70));
            Console.ResetColor();
        }

        // Método para mostrar el menú principal
        static void MostrarMenu()
        {
            Console.WriteLine();
            Console.WriteLine("┌─────────────────────────────────────┐");
            Console.WriteLine("│      MENÚ PRINCIPAL DEL SISTEMA     │");
            Console.WriteLine("├─────────────────────────────────────┤");
            Console.WriteLine("│ 1. Registrar nueva solicitud        │");
            Console.WriteLine("│ 2. Ver todas las solicitudes        │");
            Console.WriteLine("│ 3. Buscar solicitud por ID          │");
            Console.WriteLine("│ 4. Cambiar estado de solicitud      │");
            Console.WriteLine("│ 5. Ver estadísticas del sistema     │");
            Console.WriteLine("│ 6. Salir del programa               │");
            Console.WriteLine("└─────────────────────────────────────┘");
            Console.Write("\nSeleccione una opción (1-6): ");
        }

        // Método para registrar una solicitud
        static void RegistrarSolicitud(GestorSolicitudes gestor)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║    REGISTRAR NUEVA SOLICITUD           ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.ResetColor();

            Console.Write("\nIngrese nombre del cliente: ");
            string nombreCliente = Console.ReadLine();

            Console.Write("Ingrese descripción de la solicitud: ");
            string descripcion = Console.ReadLine();

            gestor.RegistrarSolicitud(nombreCliente, descripcion);
        }

        // Método para buscar una solicitud por ID
        static void BuscarSolicitud(GestorSolicitudes gestor)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║    BUSCAR SOLICITUD POR ID            ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.ResetColor();

            Console.Write("\nIngrese el ID de la solicitud a buscar: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Solicitud solicitud = gestor.BuscarPorId(id);

                if (solicitud != null)
                {
                    solicitud.MostrarInformacion();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n❌ No se encontró solicitud con ID {id}");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n❌ ID inválido. Debe ingresar un número.");
                Console.ResetColor();
            }
        }

        // Método para cambiar el estado de una solicitud
        static void CambiarEstadoSolicitud(GestorSolicitudes gestor)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║    CAMBIAR ESTADO DE SOLICITUD        ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.ResetColor();

            Console.Write("\nIngrese el ID de la solicitud: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n❌ ID inválido.");
                Console.ResetColor();
                return;
            }

            Solicitud solicitud = gestor.BuscarPorId(id);
            if (solicitud == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n❌ No se encontró solicitud con ID {id}");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("\n┌─────────────────────────────────────┐");
            Console.WriteLine("│    SELECCIONE NUEVO ESTADO          │");
            Console.WriteLine("├─────────────────────────────────────┤");
            Console.WriteLine("│ 1. Pendiente                        │");
            Console.WriteLine("│ 2. En Proceso                       │");
            Console.WriteLine("│ 3. Completada                       │");
            Console.WriteLine("│ 4. Cancelada                        │");
            Console.WriteLine("└─────────────────────────────────────┘");
            Console.Write("\nSeleccione el nuevo estado (1-4): ");

            string opcion = Console.ReadLine();
            EstadoSolicitud nuevoEstado;

            switch (opcion)
            {
                case "1":
                    nuevoEstado = EstadoSolicitud.Pendiente;
                    break;
                case "2":
                    nuevoEstado = EstadoSolicitud.EnProceso;
                    break;
                case "3":
                    nuevoEstado = EstadoSolicitud.Completada;
                    break;
                case "4":
                    nuevoEstado = EstadoSolicitud.Cancelada;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n❌ Opción no válida.");
                    Console.ResetColor();
                    return;
            }

            gestor.CambiarEstadoSolicitud(id, nuevoEstado);
        }

        // Método para pausar la pantalla
        static void PausarPantalla()
        {
            Console.Write("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            MostrarEncabezado();
        }

        // Método para mostrar mensaje de despedida
        static void MostrarDespedida()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║    ¡GRACIAS POR USAR NUESTRO SISTEMA! ║");
            Console.WriteLine("║                                        ║");
            Console.WriteLine("║    Servicios Tecnológicos - 2026      ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");
            Console.ResetColor();
        }
    }

    public static class StringExtensions
    {
        public static string PadBoth(this string str, int length)
        {
            int spaces = length - str.Length;
            int padLeft = spaces / 2 + str.Length;
            return str.PadLeft(padLeft).PadRight(length);
        }
    }
}