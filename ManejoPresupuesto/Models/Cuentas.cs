using ManejoPresupuesto.Validaciones;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuesto.Models
{
    public class Cuentas
    {
        public int id_cuenta { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Nombre del la Cuenta")]
        [PrimerLetraM]
        public string Nombre { get; set; }
        public decimal Balance { get; set; }
        [StringLength(maximumLength: 1000)]
        public string Descripcion { get; set; }
        [Display(Name = "Tipo Cuenta")]
        public int id_TiposCuen { get; set; }
        public string TipoCuenta { get; set; }
    }
}
