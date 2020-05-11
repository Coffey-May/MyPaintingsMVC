using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paintings.Data;
using Paintings.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Paintings.Controllers
{
    public class PaintingOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PaintingOrdersController(ApplicationDbContext context, UserManager<ApplicationUser> usermanager)
        {
            _userManager = usermanager;
            _context = context;
        }
        // GET: PaintingOrders
        public ActionResult Index()
        {
            return View();
        }

        // GET: PaintingOrders/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }



        // POST: PaintingOrders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int id)
        {
            try
            {
                var user = await GetCurrentUserAsync();
                var userCurrentOrder = _context.Order.Where(o => o.ApplicationUserId == user.Id).FirstOrDefault(o => o.IsComplete == null);
                if (userCurrentOrder != null)
                {
                    var newPaintingOrder = new PaintingOrder
                    {
                        OrderId = userCurrentOrder.OrderId,
                        PaintingId = id
                    };
                    _context.PaintingOrder.Add(newPaintingOrder);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Orders");
                }
                else
                {
                    var newOrder = new Order
                    {
                        ApplicationUserId = user.Id,
                        DateTime = DateTime.Now
                    };
                    _context.Order.Add(newOrder);
                    var newPaintingOrder = new PaintingOrder
                    {
                        Order = newOrder,
                        PaintingId = id
                    };
                    _context.PaintingOrder.Add(newPaintingOrder);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Orders");
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: PaintingOrders/Edit/5 
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PaintingOrders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: PaintingOrders/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var paintingOrder = _context.PaintingOrder.FirstOrDefault(po => po.PaintingId == id);
                _context.PaintingOrder.Remove(paintingOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Orders");
            }
            catch
            {
                return View();
            }
        }
        private async Task<ApplicationUser> GetCurrentUserAsync() => await _userManager.GetUserAsync(HttpContext.User);

    }
}