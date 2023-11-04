using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManejoPresupuesto.Models
{
    public class CuentasCreacionViewModel : Cuentas
    {
        public IEnumerable<SelectListItem> TiposCuentas { get; set; }
    }
}
