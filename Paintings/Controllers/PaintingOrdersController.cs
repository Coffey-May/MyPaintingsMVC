using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paintings.Data;
using Paintings.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
        public async Task<ActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var paintingOrders = await _context.PaintingOrder
                .ToListAsync();

            return View(paintingOrders);
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
                var userCurrentOrder =  _context.Order.Where(o => o.ApplicationUserId == user.Id).FirstOrDefault(o => o.IsComplete == false);
                //userCurrentOrder.IsComplete = true;
                if (userCurrentOrder != null)

                {
                   var newPaintingOrder = new PaintingOrder
                    {
                        OrderId = userCurrentOrder.OrderId,
                        PaintingId = id,
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
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                var user = await GetCurrentUserAsync();
                //var currentOrder = _context.Order.Where(o => o.ApplicationUserId == user.Id).FirstOrDefault(o => o.IsComplete == false);
                var userCurrentOrder = _context.Order.Where(o => o.ApplicationUserId == user.Id).FirstOrDefault(o => o.IsComplete == false);
                userCurrentOrder.IsComplete = true;
                if (userCurrentOrder != null)
                {

                    var newPaintingOrder = new PaintingOrder
                    {
                        OrderId = userCurrentOrder.OrderId,
                        PaintingId = id,
                    };
                    _context.PaintingOrder.Update(newPaintingOrder);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Orders");

                }

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