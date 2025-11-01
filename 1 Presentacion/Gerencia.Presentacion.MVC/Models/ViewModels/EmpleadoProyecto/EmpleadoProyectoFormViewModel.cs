using Microsoft.AspNetCore.Mvc.Rendering;
using Gerencia.Presentacion.MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Gerencia.Core.Repositorios.Entidades;

namespace Gerencia.Presentacion.MVC.Models.ViewModels.EmpleadoProyecto
{
    public class EmpleadoProyectoDetalleViewModel
    {
        public int Linea { get; set; }

        [Display(Name = "ID de asignación")]
        [Required]
        public int ProyectoId { get; set; }
        public int? EmpleadoProyectoId { get; set; }
        public int? EmpleadoId { get; set; }



    }

    public class EmpleadoLookupRow
    {
        public int EmpleadoId { get; set; }
        public string Nombre { get; set; } = "";

    }

    public class ProductoLookupRow
    {
        public int ProyectoID { get; set; }
        public string Nombre { get; set; } = "";
        public DateTime FechaLimite { get; set; }
    }

    public class EmpleadoProyectoFormViewModel
    {
        public EmpleadoProyectoEntidad EmpleadoProyecto { get; set; } = new();

        public List<EmpleadoProyectoDetalleViewModel> Detalles { get; set; } = new();

        public string? EmpleadoNombre { get; set; }


        // ----- Modal CLIENTE (filtros + resultados) -----
        public int? FiltroEmpleadoId { get; set; }
        public string? FiltroEmpleadoNombre { get; set; }
        public List<EmpleadoLookupRow> EmpleadoResultados { get; set; } = new();
        public bool ShowEmpleadoModal { get; set; }

        // ----- Modal PRODUCTO (filtros + catálogos + resultados) -----
        public int? FiltroProyectoId { get; set; }
        public int? FiltroProyectoNombre{ get; set; }
        public int? FiltroProyectoFechaLimite { get; set; }

        public List<ProductoLookupRow> ProyectoResultados { get; set; } = new();
        public bool ShowProyectoModal { get; set; }
    }
}
