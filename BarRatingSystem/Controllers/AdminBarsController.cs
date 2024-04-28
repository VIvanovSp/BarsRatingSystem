using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BarRatingSystem.Data;
using BarRatingSystem.Models;

namespace BarRatingSystem.Controllers
{
    public class AdminBarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminBarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminBars
        public async Task<IActionResult> Index(string searchString = "")
        {
            ViewData["CurrentFilter"] = searchString;

            IQueryable<Bar> bars = _context.Bars;

            if (!String.IsNullOrEmpty(searchString))
            {
                bars = bars.Where(s => s.Name.Contains(searchString));
            }

            return bars != null ? 
                          View(await bars.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Bar'  is null.");
        }

        // GET: AdminBars/Details/5
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

        // GET: AdminBars/Create
        public IActionResult Create()
        {
            //base bar = new Bar()

            Bar bar = new Bar();

            bar.Image = new byte[100];

            return View(bar);
        }

        // POST: AdminBars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,UploadedImage,Image")] Bar bar)
        {

            if (ModelState.IsValid || (ModelState.ErrorCount == 1 && bar.Image == null))
            {
                if (bar.UploadedImage != null && bar.UploadedImage.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await bar.UploadedImage.CopyToAsync(memoryStream);
                        // Check the size as needed (for example < 2 MB)
                        if (memoryStream.Length <= 2097152)
                        {   
                            bar.Image = memoryStream.ToArray();
                        }
                        else
                        {
                            ModelState.AddModelError("UploadedImage", "The file is too large. Maximum allowed size is 2MB.");
                            return View(bar);
                        }
                    }
                }

                _context.Add(bar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bar);
        }

        // GET: AdminBars/Edit/5
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
            return View(bar);
        }

        // POST: AdminBars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Image")] Bar bar)
        {
            if (id != bar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid || (ModelState.ErrorCount == 1 && bar.UploadedImage == null))
            {
                try
                {
                    _context.Update(bar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BarExists(bar.Id))
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

        // GET: AdminBars/Delete/5
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

        // POST: AdminBars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bars == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Bar'  is null.");
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
