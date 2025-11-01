namespace Gerencia.Presentacion.MVC.Models.ViewModels.Proyecto
{
    public class ProcesoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<TareaViewModel> Tareas { get; set; } = new();
    }
}