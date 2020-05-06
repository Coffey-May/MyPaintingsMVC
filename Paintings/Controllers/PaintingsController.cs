using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Paintings.Data;
using Paintings.Models;
using Paintings.Models.ViewModels;

namespace Paintings.Controllers
{
    [Authorize]
    public class PaintingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PaintingsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Paintings
        public async Task<ActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var paintings = await _context.Painting
                .Where(p => p.ApplicationUserId == user.Id)
                .ToListAsync();           
                return View(paintings);
        }

        // GET: Paintings/Details/5
        public async Task<ActionResult> Details(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}
            var painting = await _context.Painting
                .Include(p => p.ApplicationUser)
                .FirstOrDefaultAsync(p => p.PaintingId == id);
            if(painting == null)
            {
                return NotFound();
            }



            return View(painting);
        }


// GET: Paintings/Create
public ActionResult Create()
        {
            return View();
        }

        // POST: Paintings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PaintingViewModel paintingViewModel)
        {

            try
            {
                var user = await GetCurrentUserAsync();
                var paintingInstance = new Painting
                {

                    Title = paintingViewModel.Title,
                    MediumUsed = paintingViewModel.MediumUsed,

                    GalleryId = paintingViewModel.GalleryId,
                    Price = paintingViewModel.Price,
                    IsSold = paintingViewModel.IsSold,
                    ApplicationUserId = user.Id
                };


                _context.Painting.Add(paintingInstance);

                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images");
              
                var fileName = Guid.NewGuid().ToString() + paintingViewModel.File.FileName;
              
                using (var fileStream = new FileStream(Path.Combine(uploadPath, fileName), FileMode.Create))
                {
                    await paintingViewModel.File.CopyToAsync(fileStream);
                }
             
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Paintings/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var painting = await _context.Painting.FirstOrDefaultAsync(p => p.PaintingId == id);
            var loggedInUser = await GetCurrentUserAsync();

            if(painting.ApplicationUserId != loggedInUser.Id)
            {
                return NotFound();
            }
            return View(painting);
        }

        // POST: Paintings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Painting painting)
        {
            try
            {
                var user = await GetCurrentUserAsync();
                painting.ApplicationUserId = user.Id;

                _context.Painting.Update(painting);
                await _context.SaveChangesAsync();
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Paintings/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var painting = await _context.Painting
                .Include(p => p.ApplicationUser)
                .FirstOrDefaultAsync(p => p.PaintingId == id);
            if (painting == null)
            {
                return NotFound();
            }

            return View(painting);
        }


        // POST: Paintings/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {

            var painting = await _context.Painting.FindAsync(id);
            _context.Painting.Remove(painting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //try
            //{
            //    // TODO: Add delete logic here

            //    return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return View();
            //}
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}