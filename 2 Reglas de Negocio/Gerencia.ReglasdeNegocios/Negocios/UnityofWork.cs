namespace Gerencia.ReglasdeNegocios.Negocios
{
    public class UnityofWork
    {
        public UnityofWork(EmpleadoNegocio empleadoNegocio, EstadoNegocio estadoNegocio, ImportanciaNegocio importanciaNegocio, TipoPuestoNegocio tipoPuestoNegocio, DisponibilidadNegocio disponibilidadNegocio, ProyectoNegocio proyectoNegocio, EmpleadoProyectoNegocio empleadoProyectoNegocio, TareaNegocio tareaNegocio, EquipoNegocio equipoNegocio, ProcesoNegocio procesoNegocio)
        {
            EmpleadoNegocio = empleadoNegocio;
            ProyectoNegocio = proyectoNegocio;
            EmpleadoProyectoNegocio = empleadoProyectoNegocio;
            EstadoNegocio = estadoNegocio;
            ImportanciaNegocio = importanciaNegocio;
            TipoPuestoNegocio = tipoPuestoNegocio;
            DisponibilidadNegocio = disponibilidadNegocio;
            TareaNegocio = tareaNegocio;
            EquipoNegocio = equipoNegocio;
            ProcesoNegocio = procesoNegocio;
        }


        public EmpleadoNegocio EmpleadoNegocio { get; }
        public ProyectoNegocio ProyectoNegocio { get; }
        public EmpleadoProyectoNegocio EmpleadoProyectoNegocio { get; }
        public EstadoNegocio EstadoNegocio { get; }
        public ImportanciaNegocio ImportanciaNegocio { get; }
        public TipoPuestoNegocio TipoPuestoNegocio { get; }
        public DisponibilidadNegocio DisponibilidadNegocio { get; }
        public TareaNegocio TareaNegocio { get; }
        public EquipoNegocio EquipoNegocio { get; }
        public ProcesoNegocio ProcesoNegocio { get; }
    }
}
