using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bar_Rating.Data;
using Bar_Rating.Models;
using Microsoft.AspNetCore.Authorization;

namespace Bar_Rating.Controllers
{
    public class BarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bars
        public async Task<IActionResult> Index()
        {
              return _context.Bars != null ? 
                          View(await _context.Bars.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Bars'  is null.");
        }

        // GET: Bars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bars == null)
            {
                return NotFound();
            }

            var bar = await _context.Bars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bar == null)
            {
                return NotFound();
            }

            return View(bar);
        }

        // GET: Bars/Create
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bars/Create
        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public async Task<IActionResult> Create(BarViewModel _bar)
        {
            using var stream = new MemoryStream();
            _bar.BarImage.CopyTo(stream);
            byte[] bytes = stream.ToArray();

            Bar bar = new Bar(_bar.Name, _bar.Description, bytes);
            if (ModelState.IsValid)
            {
                _context.Add(bar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bar);
        }

        // GET: Bars/Edit/5
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bars == null)
            {
                return NotFound();
            }

            var bar = await _context.Bars.FindAsync(id);
            if (bar == null)
            {
                return NotFound();
            }
            var stream = new MemoryStream(bar.BarImage);
            var file = new FormFile(stream, 0, bar.BarImage.Length, bar.Name, bar.Name);

            var _bar = new BarViewModel
            {
                Id = bar.Id,
                Name = bar.Name,
                Description = bar.Description,
                BarImage = file,
            };
            return View(_bar);
        }

        // POST: Bars/Edit/5
        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public async Task<IActionResult> Edit(int id, BarViewModel bar)
        {
            using var stream = new MemoryStream();
            bar.BarImage.CopyTo(stream);
            byte[] bytes = stream.ToArray();

            Bar _bar = new Bar(bar.Name, bar.Description, bytes);
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(_bar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BarExists(id))
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
            return View(bar);
        }

        // GET: Bars/Delete/5
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bars == null)
            {
                return NotFound();
            }

            var bar = await _context.Bars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bar == null)
            {
                return NotFound();
            }

            return View(bar);
        }

        // POST: Bars/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bars == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Bars'  is null.");
            }
            var bar = await _context.Bars.FindAsync(id);
            if (bar != null)
            {
                _context.Bars.Remove(bar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BarExists(int id)
        {
          return (_context.Bars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
