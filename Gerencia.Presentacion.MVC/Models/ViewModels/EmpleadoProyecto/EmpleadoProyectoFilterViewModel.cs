using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Gerencia.Presentacion.MVC.Models.ViewModels.EmpleadoProyecto
{
    public class EmpleadoProyectoFilterViewModel
    {
        public int? EmpleadoProyectoId { get; set; }

        [Display(Name = "ID de Empleado")]
        public int? EmpleadoId { get; set; }

        [Display(Name = "Nombre contiene")]
        public string? EmpleadoNombreLike { get; set; }

        [Display(Name = "Desde"), DataType(DataType.Date)]
        public DateTime? FechaDesde { get; set; }
        [Display(Name = "Hasta"), DataType(DataType.Date)]
        public DateTime? FechaHasta { get; set; }

        // Sort/Paging
        public string? SortBy { get; set; } // PedidoID, Cliente, Fecha, Total, Status
        public string? SortDir { get; set; } // asc/desc
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // Flag para que Index no muestre nada hasta aplicar filtro
        public bool MostrarResultados { get; set; } = false;
    }
}
