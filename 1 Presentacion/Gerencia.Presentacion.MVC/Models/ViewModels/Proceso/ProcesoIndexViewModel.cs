using Gerencia.ReglasdeNegocios.Repositorios.SQL.Infrastructure;

namespace Gerencia.Presentacion.MVC.Models.ViewModels.Proceso
{
    public class ProcesoIndexViewModel
    {
        public ProcesoFilterViewModel Filtro { get; set; } = new ProcesoFilterViewModel();
        public PaginatedList<Gerencia.Presentacion.MVC.Models.Proceso>? Resultados { get; set; }
    }
}