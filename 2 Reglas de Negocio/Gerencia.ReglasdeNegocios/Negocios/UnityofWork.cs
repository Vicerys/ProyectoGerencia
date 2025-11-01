using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerencia.ReglasdeNegocios.Negocios
{
    public class UnityofWork
    {
        public UnityofWork(EmpleadoNegocio empleadoNegocio, EstadoNegocio estadoNegocio, ImportanciaNegocio importanciaNegocio, TipoPuestoNegocio tipoPuestoNegocio, DisponibilidadNegocio disponibilidadNegocio)
        {
            EmpleadoNegocio = empleadoNegocio;
            EstadoNegocio = estadoNegocio;
            ImportanciaNegocio = importanciaNegocio;
            TipoPuestoNegocio = tipoPuestoNegocio;
            DisponibilidadNegocio = disponibilidadNegocio;
        }

        public EmpleadoNegocio EmpleadoNegocio { get; }
        public EstadoNegocio EstadoNegocio { get; }
        public ImportanciaNegocio ImportanciaNegocio { get; }
        public TipoPuestoNegocio TipoPuestoNegocio { get; }
        public DisponibilidadNegocio DisponibilidadNegocio { get; }
    }
}
