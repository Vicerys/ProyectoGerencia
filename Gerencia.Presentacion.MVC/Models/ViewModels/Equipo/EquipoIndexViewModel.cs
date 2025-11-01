using Gerencia.Core.Dtos;

namespace Gerencia.Presentacion.MVC.Models.ViewModels.Equipo
{
    public class EquipoIndexViewModel
    {
        public List<EquipoDto> Equipos { get; set; } = new();
        public List<TareaDto> ActividadesDisponibles { get; set; } = new();
    }
}
