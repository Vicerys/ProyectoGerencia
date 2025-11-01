using Gerencia.ReglasdeNegocios.Repositorios.SQL.Infrastructure;

namespace Gerencia.Presentacion.MVC.Models.ViewModels.EmpleadoProyecto
{
    public class EmpleadoProyectoIndexViewModel
    {
        public EmpleadoProyectoFilterViewModel Filtro { get; set; } = new();
        public IPaginatedList? Resultados { get; set; }
        public IEnumerable<EmpleadoProyectoIndexRowViewModel>? Items { get; set; } // para render
    }
}
