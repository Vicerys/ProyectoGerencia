using Gerencia.Core.Repositorios.Entidades;
using Gerencia.ReglasdeNegocios.Repositorios.SQL.Infrastructure;

namespace Gerencia.Presentacion.MVC.Models.ViewModels.Proyecto
{
    public class ProyectoIndexViewModel
    {
        public ProyectoFilterViewModel Filtro { get; set; } = new ProyectoFilterViewModel();
        public PaginatedList<ProyectoEntidad>? Resultados { get; set; }
    }
}