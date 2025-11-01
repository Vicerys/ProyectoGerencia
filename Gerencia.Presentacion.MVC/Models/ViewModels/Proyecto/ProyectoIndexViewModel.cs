using Gerencia.Core.Dtos;
using Gerencia.ReglasdeNegocios.Repositorios.SQL.Infrastructure;

namespace Gerencia.Presentacion.MVC.Models.ViewModels.Proyecto
{
    public class ProyectoIndexViewModel
    {
        public List<ProyectoItemViewModel> Proyectos { get; set; } = new();
        public List<ProcesoViewModel> Procesos { get; set; } = new();
        /*public ProyectoFilterViewModel Filtro { get; set; } = new ProyectoFilterViewModel();
        public PaginatedList<ProyectoDto>? Resultados { get; set; }*/
    }
}