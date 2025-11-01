using Gerencia.Presentacion.MVC.Models;
using System;
using System.Collections.Generic;

namespace Gerencia.Presentacion.MVC.Models.ViewModels.EmpleadoProyecto
{
    public class EmpleadoProyectoIndexViewModel
    {
        // Filtros (binding por QueryString)
        public int? EmpleadoProyectoID { get; set; }
        public int? EmpleadoID { get; set; }
        public string? EmpleadoNombre { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }

        // Resultados
        public List<Models.EmpleadoProyecto> Resultados { get; set; } = new();
    }
}
