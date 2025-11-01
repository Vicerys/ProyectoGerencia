using Gerencia.Core.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Gerencia.Presentacion.MVC.Models.ViewModels.EmpleadoProyecto
{
    public class EmpleadoProyectoFormViewModel
    {
        [Required] public EmpleadoProyectoDto EmpleadoProyecto { get; set; } = new EmpleadoProyectoDto();

        // Para cabecera
        public string? EmpleadoNombre { get; set; }

        // Para edición de detalles en empleado (UI)
        public List<EmpleadoProyectoDetalleDto> Detalles { get; set; } = new();

    }
}
