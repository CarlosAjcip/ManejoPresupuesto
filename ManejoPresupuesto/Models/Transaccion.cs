using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuesto.Models
{
    public class Transaccion
    {
        public int id_transacciones { get; set; }
        [Display(Name = "Fecha Transacción")]
        [DataType(DataType.Date)]
        public DateTime fechaTransaccion { get; set; } = DateTime.Today;
        public decimal monto { get; set; }
        [StringLength(maximumLength: 1000, ErrorMessage = "La nota no puede pasar de {1} caracteres")]
        public string nota { get; set; }
        [Display(Name = "Tipo Usuario")]
        public int id_usuarios { get; set; }
        [Range(1, maximum: int.MaxValue, ErrorMessage = "Debe seleccionar una cuenta")]
        [Display(Name = "Tipo Cuenta")]
        public int id_cuenta { get; set; }
        [Display(Name = "Tipo Categoria")]
        [Range(1, maximum: int.MaxValue, ErrorMessage = "Debe seleccionar una categoría")]
        public int id_categorias { get; set; }
        [Display(Name = "Tipo Operacion")]
        public TipoOperacion TipoOperacionId { get; set; } = TipoOperacion.Gatos;
        public string Cuenta { get; set; }
        public string Categoria { get; set; }

    }
}
