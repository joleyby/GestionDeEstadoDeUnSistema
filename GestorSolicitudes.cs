namespace SistemaGestionSolicitudes
{
    internal class GestorSolicitudes
    {
        private List<Solicitud> solicitudes;
        private int proximoId;

        public GestorSolicitudes()
        {
            solicitudes = new List<Solicitud>();
            proximoId = 1;
        }
        public void RegistrarSolicitud(string nombreCliente, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(nombreCliente) || string.IsNullOrWhiteSpace(descripcion))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n❌ Error: El nombre del cliente y la descripción no pueden estar vacíos.");
                Console.ResetColor();
                return;
            }

            Solicitud nuevaSolicitud = new Solicitud(proximoId, nombreCliente, descripcion, EstadoSolicitud.Pendiente);
            solicitudes.Add(nuevaSolicitud);
            proximoId++;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✓ Solicitud registrada exitosamente con ID: {nuevaSolicitud.Id}");
            Console.ResetColor();
        }

        public void MostrarTodasLasSolicitudes()
        {
            if (solicitudes.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n⚠ No hay solicitudes registradas en el sistema.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("\n" + new string('=', 80));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("LISTADO DE TODAS LAS SOLICITUDES");
            Console.ResetColor();
            Console.WriteLine(new string('=', 80));

            foreach (var solicitud in solicitudes)
            {
                Console.Write($"ID: {solicitud.Id,-4} | Cliente: {solicitud.Nombre,-20} | Estado: ");

                switch (solicitud.Estado)
                {
                    case EstadoSolicitud.Pendiente:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Pendiente");
                        break;
                    case EstadoSolicitud.EnProceso:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("En Proceso");
                        break;
                    case EstadoSolicitud.Completada:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Completada");
                        break;
                    case EstadoSolicitud.Cancelada:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Cancelada");
                        break;
                }
                Console.ResetColor();
                Console.WriteLine();
            }
            Console.WriteLine(new string('=', 80) + "\n");
        }

        public Solicitud BuscarPorId(int id)
        {
            return solicitudes.FirstOrDefault(s => s.Id == id);
        }

        public bool CambiarEstadoSolicitud(int id, EstadoSolicitud nuevoEstado)
        {
            Solicitud solicitud = BuscarPorId(id);

            if (solicitud == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n❌ Error: No se encontró solicitud con ID {id}");
                Console.ResetColor();
                return false;
            }

            string estadoAnterior = solicitud.ObtenerEstadoTexto();
            solicitud.CambiarEstado(nuevoEstado);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✓ Estado actualizado: {estadoAnterior} → {solicitud.ObtenerEstadoTexto()}");
            Console.ResetColor();
            return true;
        }

        public void MostrarEstadisticas()
        {
            if (solicitudes.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n⚠ No hay solicitudes para mostrar estadísticas.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("\n" + new string('=', 50));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("ESTADÍSTICAS DEL SISTEMA");
            Console.ResetColor();
            Console.WriteLine(new string('=', 50));
            Console.WriteLine($"Total de solicitudes:    {solicitudes.Count}");
            Console.WriteLine($"Pendientes:              {solicitudes.Count(s => s.Estado == EstadoSolicitud.Pendiente)}");
            Console.WriteLine($"En Proceso:              {solicitudes.Count(s => s.Estado == EstadoSolicitud.EnProceso)}");
            Console.WriteLine($"Completadas:             {solicitudes.Count(s => s.Estado == EstadoSolicitud.Completada)}");
            Console.WriteLine($"Canceladas:              {solicitudes.Count(s => s.Estado == EstadoSolicitud.Cancelada)}");
            Console.WriteLine(new string('=', 50) + "\n");
        }

        public int ObtenerTotalSolicitudes()
        {
            return solicitudes.Count;
        }
    }

}
