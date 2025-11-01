using Microsoft.AspNetCore.Mvc.Rendering;
using Gerencia.Presentacion.MVC.Models;
using System.ComponentModel.DataAnnotations;

namespace Gerencia.Presentacion.MVC.Models.ViewModels.Proceso
{
    public enum DateTarget { None = 0, FechaEstimada = 1, FechaReal = 2 }

    public class ProcesoFilterViewModel
    {
        public int? ProcesoID { get; set; }
        [Display(Name = "Patrón de Nombre")] public string? NombreLike { get; set; }

        public int? ImportanciaId { get; set; }
        public IEnumerable<SelectListItem> Importancia { get; set; } = Enumerable.Empty<SelectListItem>();

        // Perecedero (tri-estado): null=No aplica, true/false filtra
        public bool? Finalizado { get; set; }

        // IVA seleccionable múltiple (0,10,16)
        public List<byte> Dispositivos { get; set; } = new();

        // Empaque (si true, filtra por true)
        public bool? Proyecto1 { get; set; }
        public bool? Proyecto2 { get; set; }
        public bool? Proyecto3 { get; set; }


        // Fecha (alta/modif/ninguna) + rango
        public DateTarget FechaTarget { get; set; } = DateTarget.None;
        [DataType(DataType.Date)] public DateTime? FechaDesde { get; set; }
        [DataType(DataType.Date)] public DateTime? FechaHasta { get; set; }

        // Status (FK catálogo) multi-select (A/I/S)
        public List<string> ProcesoStatus { get; set; } = new();
        public IEnumerable<SelectListItem> Statuses { get; set; } = Enumerable.Empty<SelectListItem>();

        public bool MostrarTodos { get; set; }

        // Paginación
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}