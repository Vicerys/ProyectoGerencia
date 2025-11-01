using Microsoft.AspNetCore.Mvc.Rendering;
using Gerencia.Presentacion.MVC.Models;
using System.ComponentModel.DataAnnotations;

namespace Gerencia.Presentacion.MVC.Models.ViewModels.Proyecto
{
    public enum DateTarget { None = 0, FechaLimite = 1 }

    public class ProyectoFilterViewModel
    {
        public int? ProyectoID { get; set; }
        [Display(Name = "Patrón de Nombre")] public string? NombreLike { get; set; }

        // Fecha (limite) + rango
        public DateTarget FechaTarget { get; set; } = DateTarget.None;
        [DataType(DataType.Date)] public DateTime? FechaDesde { get; set; }
        [DataType(DataType.Date)] public DateTime? FechaHasta { get; set; }

        public bool MostrarTodos { get; set; }

        // Paginación
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}