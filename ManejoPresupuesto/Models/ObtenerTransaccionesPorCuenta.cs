namespace ManejoPresupuesto.Models
{
    public class ObtenerTransaccionesPorCuenta
    {
        public int id_usuarios { get; set; }
        public int id_cuenta { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
