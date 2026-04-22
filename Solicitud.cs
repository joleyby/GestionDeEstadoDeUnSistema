namespace SistemaGestionSolicitudes
{
    internal class Solicitud
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public EstadoSolicitud Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        // Constructor
        public Solicitud(int id, string nombre, string descripcion, EstadoSolicitud estado)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            Estado = estado;
            FechaCreacion = DateTime.Now;
            FechaActualizacion = null;
        }

        public void MostrarInformacion()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("INFORMACIÓN DE SOLICITUD");
            Console.ResetColor();
            Console.WriteLine(new string('=', 60));
            Console.WriteLine($"ID Solicitud:        {Id}");
            Console.WriteLine($"Cliente:             {Nombre}");
            Console.WriteLine($"Descripción:         {Descripcion}");

            Console.Write($"Estado:              ");
            MostrarEstadoConColor();

            Console.WriteLine($"Fecha Creación:      {FechaCreacion:dd/MM/yyyy HH:mm:ss}");
            if (FechaActualizacion.HasValue)
            {
                Console.WriteLine($"Última Actualización: {FechaActualizacion:dd/MM/yyyy HH:mm:ss}");
            }
            Console.WriteLine(new string('=', 60) + "\n");
        }

        private void MostrarEstadoConColor()
        {
            switch (Estado)
            {
                case EstadoSolicitud.Pendiente:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Pendiente");
                    break;
                case EstadoSolicitud.EnProceso:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("En Proceso");
                    break;
                case EstadoSolicitud.Completada:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Completada");
                    break;
                case EstadoSolicitud.Cancelada:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Cancelada");
                    break;
            }
            Console.ResetColor();
        }

        public void CambiarEstado(EstadoSolicitud nuevoEstado)
        {
            Estado = nuevoEstado;
            FechaActualizacion = DateTime.Now;
        }

        public string ObtenerEstadoTexto()
        {
            return Estado switch
            {
                EstadoSolicitud.Pendiente => "Pendiente",
                EstadoSolicitud.EnProceso => "En Proceso",
                EstadoSolicitud.Completada => "Completada",
                EstadoSolicitud.Cancelada => "Cancelada",
                _ => "Desconocido"
            };
        }
    }

}
