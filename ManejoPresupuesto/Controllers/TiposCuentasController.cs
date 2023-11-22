using ManejoPresupuesto.Models;
using ManejoPresupuesto.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace ManejoPresupuesto.Controllers
{
    public class TiposCuentasController : Controller
    {
        private readonly IRepositorioTiposCuentas repositorioTiposCuentas;
        private readonly IServicioUsuarios servicioUsuarios;

        public TiposCuentasController(IRepositorioTiposCuentas repositorioTiposCuentas, IServicioUsuarios servicioUsuarios)
        {
            this.repositorioTiposCuentas = repositorioTiposCuentas;
            this.servicioUsuarios = servicioUsuarios;
        }


        // GET: TiposCuentasController
        public async Task<ActionResult> Index()
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var tiposCuentas = await repositorioTiposCuentas.Obtener(usuarioId);

            return View(tiposCuentas);
        }

        // GET: TiposCuentasController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TiposCuentasController/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: TiposCuentasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TipoCuenta tipoCuenta)
        {

            if (!ModelState.IsValid)
            {
                return View(tipoCuenta);
            }

            tipoCuenta.id_usuarios = servicioUsuarios.ObtenerUsuarioId();

            //validacion si existe
            var yaExiste = await repositorioTiposCuentas.Existe(
            tipoCuenta.Nombre, tipoCuenta.id_usuarios);


            if (yaExiste)
            {
                ModelState.AddModelError(
                nameof(tipoCuenta.Nombre),
                $"El nombre {tipoCuenta.Nombre} ya existe.");

                return View(tipoCuenta);
            }


            await repositorioTiposCuentas.Crear(tipoCuenta);
            return RedirectToAction("Index");
        }

        //verifiacion si existe
        [HttpGet]
        public async Task<IActionResult> VerificarExisteTipoCuenta(string nombre)
        {
            var id_usuarios = servicioUsuarios.ObtenerUsuarioId();
            var yaexistetipo = await repositorioTiposCuentas.Existe(nombre, id_usuarios);

            if (yaexistetipo)
            {
                return Json($"El nombre {nombre} ya existe");
            }

            return Json(true);
        }

        //editar el campo
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var tipoCuenta = await repositorioTiposCuentas.ObtenerPorId(id, usuarioId);

            if (tipoCuenta is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(tipoCuenta);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(TipoCuenta tipoCuenta)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var tipoCuentaExiste = await repositorioTiposCuentas.ObtenerPorId(tipoCuenta.id_tiposCuen,
                usuarioId);

            if (tipoCuentaExiste is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioTiposCuentas.Actualizar(tipoCuenta);
            return RedirectToAction("Index");
        }

        // GET: TiposCuentasController/Delete/5 se obtiene el id del campo
        public async Task<IActionResult> Eliminar(int id)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var tipoCuenta = await repositorioTiposCuentas.ObtenerPorId(id, usuarioId);

            if (tipoCuenta is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(tipoCuenta);
        }
        //Eliminar el campo seleccionado
        public async Task<IActionResult> EliminarCuenta(int id_tiposCuen)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var tipoCuenta = await repositorioTiposCuentas.ObtenerPorId(id_tiposCuen, usuarioId);

            if (tipoCuenta is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioTiposCuentas.Borrar(id_tiposCuen);
            return RedirectToAction("Index");

        }
        //moveer filas de las tablas
        [HttpPost]
        public async Task<IActionResult> Ordenar([FromBody] int[] ids)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var tipoCuenta = await repositorioTiposCuentas.Obtener(usuarioId);
            var idsTiposCuentas = tipoCuenta.Select(x => x.id_tiposCuen);

            var idsTipoNoPerteneceAlUsuario = ids.Except(idsTiposCuentas).ToList();

            if (idsTipoNoPerteneceAlUsuario.Count > 0)
            {
                return Forbid();
            }

            var tiposCuentasOrdenados = ids.Select((valor, indice) =>
            new TipoCuenta() { id_tiposCuen = valor, Orden = indice + 1 }).AsEnumerable();

            await repositorioTiposCuentas.Ordenar(tiposCuentasOrdenados);
            return Ok();
        }
    }
}
