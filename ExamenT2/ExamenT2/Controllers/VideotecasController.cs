using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamenT2.Data;
using ExamenT2.Models;

namespace ExamenT2.Controllers
{
    public class VideotecasController : Controller
    {
        private readonly ExamenT2Context _context;

        public VideotecasController(ExamenT2Context context)
        {
            _context = context;
        }

        // GET: Videotecas
        public async Task<IActionResult> Index(string buscarNom, decimal? buscarRat, string ordenar)
        {

            ViewData["NombreSort"] = String.IsNullOrEmpty(ordenar) ? "nombre_desc" : "";
            ViewData["TipoSort"] = ordenar == "tipo_asc" ? "tipo_dsc" : "tipo_asc";
            ViewData["VecesSort"] = ordenar == "veces_asc" ? "veces_dsc" : "veces_asc";
            ViewData["RatingSort"] = ordenar == "rating_asc" ? "rating_dsc" : "rating_asc";

            

            var videoteca = from m in _context.Videoteca
                            select m;

            switch (ordenar)
            {
                case "nombre_desc":
                    videoteca = videoteca.OrderByDescending(s => s.nombre);
                    break;
                case "tipo_dsc":
                    videoteca = videoteca.OrderByDescending(s => s.tipo);
                    break;
                case "tipo_asc":
                    videoteca = videoteca.OrderBy(s => s.tipo);
                    break;
                case "veces_dsc":
                    videoteca = videoteca.OrderByDescending(s => s.veces_vista);
                    break;
                case "veces_asc":
                    videoteca = videoteca.OrderBy(s => s.veces_vista);
                    break;
                case "rating_dsc":
                    videoteca = videoteca.OrderByDescending(s => s.rating);
                    break;
                case "rating_asc":
                    videoteca = videoteca.OrderBy(s => s.rating);
                    break;
                default:
                    videoteca = videoteca.OrderBy(ventas => ventas.nombre);
                    break;
            }

            // buscar
            if (!String.IsNullOrEmpty(buscarNom))
            {
                videoteca = videoteca.Where(s => s.nombre!.Contains(buscarNom));
            }

            if (buscarRat.HasValue)
            {
                videoteca = videoteca.Where(s => s.rating >= buscarRat.Value);
            }



            return View(await videoteca.ToListAsync());
        }

        // GET: Videotecas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoteca = await _context.Videoteca
                .FirstOrDefaultAsync(m => m.id == id);
            if (videoteca == null)
            {
                return NotFound();
            }

            return View(videoteca);
        }

        // GET: Videotecas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Videotecas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nombre,tipo,veces_vista,rating,fecha")] Videoteca videoteca)
        {
            if (ModelState.IsValid)
            {
                _context.Add(videoteca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(videoteca);
        }

        // GET: Videotecas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoteca = await _context.Videoteca.FindAsync(id);
            if (videoteca == null)
            {
                return NotFound();
            }
            return View(videoteca);
        }

        // POST: Videotecas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nombre,tipo,veces_vista,rating,fecha")] Videoteca videoteca)
        {
            if (id != videoteca.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(videoteca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideotecaExists(videoteca.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(videoteca);
        }

        // GET: Videotecas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoteca = await _context.Videoteca
                .FirstOrDefaultAsync(m => m.id == id);
            if (videoteca == null)
            {
                return NotFound();
            }

            return View(videoteca);
        }

        // POST: Videotecas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var videoteca = await _context.Videoteca.FindAsync(id);
            if (videoteca != null)
            {
                _context.Videoteca.Remove(videoteca);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideotecaExists(int id)
        {
            return _context.Videoteca.Any(e => e.id == id);
        }
    }
}
