namespace Gerencia.Presentacion.MVC.Models.ViewModels.EmpleadoProyecto
{
    public class EmpleadoProyectoIndexRowViewModel
    {
        public int EmpleadoProyectoId { get; set; }
        public int EmpleadoId { get; set; }
        public string EmpleadoNombre { get; set; } = "";
        public DateTime FechaAsignacion { get; set; }
        
    }
}
