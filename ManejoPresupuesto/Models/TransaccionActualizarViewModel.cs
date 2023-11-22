namespace ManejoPresupuesto.Models
{
    public class TransaccionActualizarViewModel : TransaccionCreacionVewModel
    {
        public int cuentaAnteriorId { get; set; }
        public decimal MontoAnterior { get; set; }
        public string UrlRetorno { get; set; }
    }
}
