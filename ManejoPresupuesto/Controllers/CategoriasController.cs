using ManejoPresupuesto.Models;
using ManejoPresupuesto.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace ManejoPresupuesto.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly IRepositorioCategorias repositorioCategorias;
        private readonly IServicioUsuarios servicioUsuarios;

        public CategoriasController(IRepositorioCategorias repositorioCategorias, IServicioUsuarios servicioUsuarios)
        {
            this.repositorioCategorias = repositorioCategorias;
            this.servicioUsuarios = servicioUsuarios;
        }

        // GET: CategoriasController
        public async Task<IActionResult> Index()
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var categorias = await repositorioCategorias.Obtener(usuarioId);
            return View(categorias);
        }

        // GET: CategoriasController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoriasController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: CategoriasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Categoria categoria)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();

            if (!ModelState.IsValid)
            {
                return View(categoria);
            }
            categoria.id_usuarios = usuarioId;
            await repositorioCategorias.Crear(categoria);
            return RedirectToAction("Index");
        }

        // GET: CategoriasController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var categoria = await repositorioCategorias.ObtenerPorId(id, usuarioId);

            if (categoria is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            return View(categoria);

        }

        // POST: CategoriasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Categoria categoriaEditar, int id)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var categoria = await repositorioCategorias.ObtenerPorId(id, usuarioId);
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(categoriaEditar);
                }

                if (categoria is null)
                {
                    return RedirectToAction("NoEncontrado", "Home");
                }

                categoriaEditar.id_usuarios = usuarioId;
                await repositorioCategorias.Actualizar(categoriaEditar);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se ha producido una excepción: " + ex.Message);
                throw;
            }
        }

        // GET: CategoriasController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var categoria = await repositorioCategorias.ObtenerPorId(id, usuarioId);

            if (categoria is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            return View(categoria);
        }

        // POST: CategoriasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategorias(int id_categorias)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var categoria = await repositorioCategorias.ObtenerPorId(id_categorias, usuarioId);
            try
            {
                if (categoria is null)
                {
                    return RedirectToAction("NoEncontrado", "Home");
                }

                await repositorioCategorias.Eliminar(id_categorias);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se ha producido una excepción: " + ex.Message);
                throw;
            }
        }
    }
}
