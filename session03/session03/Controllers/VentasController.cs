using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using session03.Data;
using session03.Models;

namespace session03.Controllers
{
    public class VentasController : Controller
    {
        private readonly session03Context _context;

        public VentasController(session03Context context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index(string buscarDes, string ordenar)
        {
            ViewData["DescripSort"] = String.IsNullOrEmpty(ordenar) ? "descrip_desc" : "";
            ViewData["CantidadSort"] = ordenar == "cant_asc" ? "cant_dsc" : "cant_asc";

            var ventas = from m in _context.Ventas
                         select m;

            switch (ordenar)
            {
                case "descrip_desc":
                    ventas = ventas.OrderByDescending(s => s.descripcion);
                    break;
                case "cant_dsc":
                    ventas = ventas.OrderByDescending(s => s.cantidad);
                    break;
                case "cant_asc":
                    ventas = ventas.OrderBy(s => s.cantidad);
                    break;
                default:
                    ventas= ventas.OrderBy(ventas => ventas.descripcion); 
                    break;
            }   

            if (!String.IsNullOrEmpty(buscarDes))
            {
               ventas = ventas.Where(s => s.descripcion!.Contains(buscarDes));
            }

            return View(await ventas.ToListAsync());
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventas = await _context.Ventas
                .FirstOrDefaultAsync(m => m.id == id);
            if (ventas == null)
            {
                return NotFound();
            }

            return View(ventas);
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ventas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,descripcion,cantidad,precio_uni")] Ventas ventas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ventas);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Index));
            }
            return View(ventas);
        }

        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventas = await _context.Ventas.FindAsync(id);
            if (ventas == null)
            {
                return NotFound();
            }
            return View(ventas);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,descripcion,cantidad,precio_uni")] Ventas ventas)
        {
            if (id != ventas.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ventas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentasExists(ventas.id))
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
            return View(ventas);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventas = await _context.Ventas
                .FirstOrDefaultAsync(m => m.id == id);
            if (ventas == null)
            {
                return NotFound();
            }

            return View(ventas);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ventas = await _context.Ventas.FindAsync(id);
            if (ventas != null)
            {
                _context.Ventas.Remove(ventas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentasExists(int id)
        {
            return _context.Ventas.Any(e => e.id == id);
        }
    }
}
