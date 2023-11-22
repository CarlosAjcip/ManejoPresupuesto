using AutoMapper;
using ManejoPresupuesto.Models;
using ManejoPresupuesto.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
using System.Transactions;

namespace ManejoPresupuesto.Controllers
{
    public class TransaccionesController : Controller
    {
        private readonly IServicioUsuarios servicioUsuarios;
        private readonly IRepositorioCuentas repositorioCuentas;
        private readonly IRepositorioCategorias repositorioCategorias;
        private readonly IRepositorioTransaccines repositorioTransaccines;
        private readonly IMapper mapper;

        public TransaccionesController(IServicioUsuarios servicioUsuarios, IRepositorioCuentas repositorioCuentas, IRepositorioCategorias repositorioCategorias,
            IRepositorioTransaccines repositorioTransaccines, IMapper mapper)
        {
            this.servicioUsuarios = servicioUsuarios;
            this.repositorioCuentas = repositorioCuentas;
            this.repositorioCategorias = repositorioCategorias;
            this.repositorioTransaccines = repositorioTransaccines;
            this.mapper = mapper;
        }
        // GET: TransaccionesController
        public async Task<IActionResult> Index(int mes,int anio)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();

            DateTime fechaInicio;
            DateTime fechaFin;

            if (mes <= 0 || mes > 12 || anio <= 1900)
            {
                var hoy = DateTime.Today;
                fechaInicio = new DateTime(hoy.Year, hoy.Month, 1);
            }
            else
            {
                fechaInicio = new DateTime(anio, mes, 1);
            }

            fechaFin = fechaInicio.AddMonths(1).AddDays(-1);

            var parametro = new ParametroObtenerTransaccionesPorUsuario()
            {
                id_usuarios = usuarioId,
                FechaInicio = fechaInicio,
                FechaFin = fechaFin
            };

            var trasacciones = await repositorioTransaccines.ObtenerPorUsuarioId(parametro);


            var modelo = new ReporteTransaccionesDetalladas();

            var transaccionesPorFehca = trasacciones.OrderByDescending(x => x.fechaTransaccion)
                .GroupBy(x => x.fechaTransaccion)
                .Select(grupo => new ReporteTransaccionesDetalladas.TransaccionesPorFecha()
                {
                    FechaTransaccione = grupo.Key,
                    Transacciones = grupo.AsEnumerable()
                });

            modelo.TransaccionesAgrupadas = transaccionesPorFehca;
            modelo.FechaInicio = fechaInicio;
            modelo.FechaFin = fechaFin;

            ViewBag.mesAnterior = fechaInicio.AddMonths(-1).Month;
            ViewBag.anioAnterior = fechaInicio.AddMonths(-1).Year;

            ViewBag.mesPosterior = fechaInicio.AddMonths(1).Month;
            ViewBag.anioPosterior = fechaInicio.AddMonths(1).Year;
            ViewBag.urlRetorno = HttpContext.Request.Path + HttpContext.Request.QueryString;


            return View(modelo);
        }

        // GET: TransaccionesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //// GET: TransaccionesController/Create
        public async Task<ActionResult> Create()
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var modelo = new TransaccionCreacionVewModel();
            modelo.Cuentas = await ObtenerCuentas(usuarioId);
            modelo.Categorias = await ObtenerCategorias(usuarioId, modelo.TipoOperacionId);
            return View(modelo);
        }

        // POST: TransaccionesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransaccionCreacionVewModel transaccionCreacionVewModel)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();

            if (!ModelState.IsValid)
            {
                transaccionCreacionVewModel.Cuentas = await ObtenerCuentas(usuarioId);
                transaccionCreacionVewModel.Categorias = await ObtenerCategorias(usuarioId, transaccionCreacionVewModel.TipoOperacionId);
                return View(transaccionCreacionVewModel);
            }

            var cuenta = await repositorioCuentas.ObtenerPorId(transaccionCreacionVewModel.id_cuenta, usuarioId);

            if (cuenta is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            var categoria = await repositorioCategorias.ObtenerPorId(transaccionCreacionVewModel.id_categorias, usuarioId);

            if (categoria is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            transaccionCreacionVewModel.id_usuarios = usuarioId;

            if (transaccionCreacionVewModel.TipoOperacionId == TipoOperacion.Gatos)
            {
                transaccionCreacionVewModel.monto *= -1;
            }

            await repositorioTransaccines.Crear(transaccionCreacionVewModel);

            return RedirectToAction("Index");
        }
        //obteniendo el listado de las cuentas
        private async Task<IEnumerable<SelectListItem>> ObtenerCuentas(int id_usuario)
        {
            var cuentas = await repositorioCuentas.Buscar(id_usuario);
            return cuentas.Select(x => new SelectListItem(x.Nombre, x.id_cuenta.ToString()));
        }
        //
        private async Task<IEnumerable<SelectListItem>> ObtenerCategorias(int usuarioId, TipoOperacion id_tiposOp)
        {
            var categoria = await repositorioCategorias.Obtener(usuarioId, id_tiposOp);
            return categoria.Select(x => new SelectListItem(x.Nombre, x.id_categorias.ToString()));
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerCategorias([FromBody] TipoOperacion tipoOperacion)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var categorias = await ObtenerCategorias(usuarioId, tipoOperacion);
            return Ok(categorias);
        }

        // GET: TransaccionesController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id,string urlRetorno = null)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var transaccion = await repositorioTransaccines.ObtenerPorId(id, usuarioId);

            if (transaccion is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            var modelo = mapper.Map<TransaccionActualizarViewModel>(transaccion);
            modelo.MontoAnterior = modelo.monto;

            if (modelo.TipoOperacionId == TipoOperacion.Gatos)
            {
                modelo.MontoAnterior = modelo.monto * -1;
            }

            modelo.cuentaAnteriorId = transaccion.id_cuenta;
            modelo.Categorias = await ObtenerCategorias(usuarioId, transaccion.TipoOperacionId);
            modelo.Cuentas = await ObtenerCuentas(usuarioId);
            modelo.UrlRetorno = urlRetorno; 
            return View(modelo);
        }

        // POST: TransaccionesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TransaccionActualizarViewModel modelo)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            if (!ModelState.IsValid)
            {
                modelo.Cuentas = await ObtenerCuentas(usuarioId);
                modelo.Categorias = await ObtenerCategorias(usuarioId, modelo.TipoOperacionId);
                return View(modelo);
            }

            var cuenta = await repositorioCuentas.ObtenerPorId(modelo.id_cuenta, usuarioId);
            if (cuenta is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            var categoria = await repositorioCategorias.ObtenerPorId(modelo.id_categorias, usuarioId);
            if (categoria is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            var trasaccion = mapper.Map<Transaccion>(modelo);

            if (modelo.TipoOperacionId == TipoOperacion.Gatos)
            {
                trasaccion.monto *= -1;
            }
            await repositorioTransaccines.Actualizar(trasaccion, modelo.MontoAnterior, modelo.cuentaAnteriorId);

            if (string.IsNullOrEmpty(modelo.UrlRetorno))
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            else
            {
                return LocalRedirect(modelo.UrlRetorno);   
            }


        }

        // GET: TransaccionesController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: TransaccionesController/Delete/5
        [HttpPost]
        public async Task<IActionResult> Borrar(int id_transacciones, string urlRetorno = null)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var transacciones = await repositorioTransaccines.ObtenerPorId(id_transacciones, usuarioId);

            if (transacciones is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioTransaccines.Borrar(id_transacciones);
            if (string.IsNullOrEmpty(urlRetorno))
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            else
            {
                return LocalRedirect(urlRetorno);
            }
           
        }
    }
}
