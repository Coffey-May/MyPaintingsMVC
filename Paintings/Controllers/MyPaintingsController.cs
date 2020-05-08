using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Paintings.Data;
using Paintings.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
//using Paintings.Models.ProductViewModels;
using Microsoft.AspNetCore.Http;
using System.Runtime.InteropServices.ComTypes;
using System.IO;

namespace Paintings.Controllers
{

    [Authorize]
    public class MyPaintingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MyPaintingsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: MyProducts
        public async Task<ActionResult> Index()
        {

            var user = await GetCurrentUserAsync();
            var MyProducts = await _context.Painting
                    .Where(p => p.ApplicationUserId == user.Id)
                    .ToListAsync();

            return View(MyProducts);


        }


        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var painting = await _context.Painting
                
                .Include(p => p.ApplicationUser)
                .FirstOrDefaultAsync(m => m.PaintingId == id);
            if (painting == null)
            {
                return NotFound();
            }

            return View(painting);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var painting = await _context.Painting.FindAsync(id);
            var itemToDelete = _context.PaintingOrder.Where(po => po.PaintingId == id);
            foreach (PaintingOrder po in itemToDelete)
            {
                _context.PaintingOrder.Remove(po);
            }
            _context.Painting.Remove(painting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}


