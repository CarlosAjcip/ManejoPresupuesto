using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuesto.Models
{
    public class TransaccionCreacionVewModel : Transaccion
    {
        public IEnumerable<SelectListItem> Cuentas { get; set; }
        public IEnumerable<SelectListItem> Categorias { get; set; }
        //[Display(Name = "Tipo Operacion")]
        //public TipoOperacion TipoOperacionId { get; set; } = TipoOperacion.Gatos;

    }
}
