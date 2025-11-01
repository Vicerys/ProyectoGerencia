using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gerencia.Presentacion.MVC.Models.ViewModels
{
    public class ProcesoFormViewModel
    {
        [Required]
        public Gerencia.Presentacion.MVC.Models.Proceso Proceso { get; set; } = new Gerencia.Presentacion.MVC.Models.Proceso();

        // Listas para los SelectList en la vista
        //public IEnumerable<SelectListItem> TipoPuestos { get; set; } = new List<SelectListItem>();

        public IEnumerable<SelectListItem> Estados { get; set; } = [];

        //public IEnumerable<SelectListItem> Disponibilidades { get; set; } = new List<SelectListItem>();

        public IEnumerable<SelectListItem> Importancias { get; set; } = [];
    }
    //ViewModel -> DTO Data Transfer Object
    //Patrones de Arquitectura: MVVM (modelo de vista), MVC, MVP (presentador)
}
