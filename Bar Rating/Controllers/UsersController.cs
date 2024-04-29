using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bar_Rating.Data;
using Bar_Rating.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Bar_Rating.Controllers
{
    [Authorize(Roles = GlobalConstants.AdminRoleName)]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager;

        public UsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var a = _context.Users.ToList();
              return _context.Users != null ? 
                          View(_context.Users.ToList()) :
                          Problem("Entity set 'ApplicationDbContext.DummyUser'  is null.");
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var dummyUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dummyUser == null)
            {
                return NotFound();
            }

            return View(dummyUser);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.DummyUser == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DummyUser'  is null.");
            }
            var dummyUser = await userManager.FindByIdAsync(id);
            if (dummyUser != null)
            {
                await userManager.DeleteAsync(dummyUser);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool DummyUserExists(string id)
        {
          return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
