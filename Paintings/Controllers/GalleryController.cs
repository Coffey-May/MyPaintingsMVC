using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Paintings.Data;
using Paintings.Models;

namespace Paintings.Controllers
{
    [Authorize]
    public class GalleryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GalleryController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Gallery
        public async Task<ActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var galleries = await _context.Gallery
                
                .ToListAsync();
            return View(galleries);
        }

        // GET: Gallery/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var gallery = await _context.Gallery
               
                  .FirstOrDefaultAsync(g => g.GalleryId == id);
            if (gallery == null)
            {
                return NotFound();
            }



            return View(gallery);
        }

        // GET: Gallery/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gallery/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Gallery gallery)
        {
            try
            {
                // TODO: Add insert logic here

                var user = await GetCurrentUserAsync();
         

                _context.Gallery.Add(gallery);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Gallery/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var gallery = await _context.Gallery.FirstOrDefaultAsync(g => g.GalleryId == id);
            var loggedInUser = await GetCurrentUserAsync();

         
            return View(gallery);
        }

        // POST: Gallery/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Gallery gallery)
        {
            try
            {
                var user = await GetCurrentUserAsync();
            

                _context.Gallery.Update(gallery);
                await _context.SaveChangesAsync();
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Gallery/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var gallery = await _context.Gallery
               
                 .FirstOrDefaultAsync(g => g.GalleryId == id);
            if (gallery == null)
            {
                return NotFound();
            }

            return View(gallery);
        }

        // POST: Gallery/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            var gallery = await _context.Gallery.FindAsync(id);
            _context.Gallery.Remove(gallery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}