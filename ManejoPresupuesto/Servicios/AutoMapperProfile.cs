using AutoMapper;
using ManejoPresupuesto.Models;

namespace ManejoPresupuesto.Servicios
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Cuentas, CuentasCreacionViewModel>();
            CreateMap<TransaccionActualizarViewModel, Transaccion>().ReverseMap();
        }
    }
}
