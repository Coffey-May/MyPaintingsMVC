using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Paintings.Data;
using Paintings.Models;
using Paintings.Models.OrderViewModels;

namespace Paintings.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Orders
        public async Task<ActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
         
            var applicationDbContext = _context.Order

             .Include(o => o.ApplicationUser)
                .Where(o => o.ApplicationUserId == user.Id || user.IsAdmin == true);


            return View(await applicationDbContext.ToListAsync());
        }

       // GET: Orders/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var user = await GetCurrentUserAsync();

            var incompleteOrder = await _context.Order
                .Where(o => o.ApplicationUserId == user.Id && o.OrderId == id)

                    .Include(o => o.ApplicationUser)
                    .Include(o => o.PaintingOrder)
                        .ThenInclude(po => po.Painting)
            .FirstOrDefaultAsync();
            if (incompleteOrder != null)
            {
                var orderDetailViewModel = new OrderDetailViewModel();
                orderDetailViewModel.LineItems = incompleteOrder.PaintingOrder.GroupBy(po => po.PaintingId)
                        .Select(p => new OrderLineItem
                        {
                            Painting = p.FirstOrDefault().Painting,
                        });
                orderDetailViewModel.Order = incompleteOrder;
                return View(orderDetailViewModel);
            }
            else
            {
                return NotFound();
            }
        }

       // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int id, PaintingOrder paintingOrder)
        {
            try
            {
                var selectedPaintingOrder = _context.PaintingOrder.FirstOrDefault(po => po.PaintingId != 0);
                var user = await GetCurrentUserAsync();
                var userOrder = _context.Order.FirstOrDefault(o => o.ApplicationUser.Id == user.Id && o.IsComplete == false);
                var chosenPainting = _context.Painting.FirstOrDefault(p => p.ApplicationUserId == user.Id && p.IsSold == false);
                if (userOrder == null)
                {
                    var newOrder = new Order
                    {
                        IsComplete = false,
                        DateTime = DateTime.Now,
                        ApplicationUserId = user.Id
                    };
                    _context.Order.Add(newOrder);
                    await _context.SaveChangesAsync();
                    int orderId = newOrder.OrderId;
                    var newPainting = new PaintingOrder
                    {
                        OrderId = orderId,
                        PaintingId = id,
                    };
                    _context.PaintingOrder.Add(newPainting);
                    await _context.SaveChangesAsync();
                    //if (chosenPainting.IsSold == false)
                    //{

                     
 
                    //};


                    return RedirectToAction("Details", "Orders", new { id = orderId });
                }
                if (userOrder.IsComplete == true)
                {
                    var newOrder = new Order
                    {
                        IsComplete = false,
                        DateTime = DateTime.Now,
                        ApplicationUserId = user.Id
                    };
                    _context.Order.Add(newOrder);
                    await _context.SaveChangesAsync();
                    int orderId = newOrder.OrderId;
                    var newPainting = new PaintingOrder
                    {
                        OrderId = orderId,
                        PaintingId = id

                    };
                    _context.PaintingOrder.Add(newPainting);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Orders", new { id = orderId });
                }
                else 
                {
                    var newPaintingOrder = new PaintingOrder
                    {
                        OrderId = userOrder.OrderId,
                        PaintingId = id
                     
                    };
                    _context.PaintingOrder.Add(newPaintingOrder);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Orders", new { id = newPaintingOrder.OrderId });
                }
            }
            catch (Exception ex)
            {
                return (NotFound());
            }

        }
        // GET: Orders/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Order.FindAsync(id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", order.ApplicationUserId);
        //    return View(order);
        //}

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {

        
                    var userCurrentOrder = await _context.Order.Where(o => o.OrderId == id)
                        .Include(o => o.PaintingOrder).ThenInclude(o => o.Painting).FirstOrDefaultAsync();
                
                    foreach (var p in userCurrentOrder.PaintingOrder)
                    {
                        if (p.Painting.IsSold == false )
                        {
                           p.Painting.IsSold = true;
                            _context.Update(p);
                        }

                    }

                    await _context.SaveChangesAsync();
                    userCurrentOrder.IsComplete = true;
                  
                    _context.Update(userCurrentOrder);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Edit", "Orders");
            }
          
            ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", order.ApplicationUserId);
            return View(order);
        }
        private async void DeletePaintingOrder(int orderId)
        {
            var paintingOrders = await _context.PaintingOrder.Where(po => po.OrderId == orderId).ToListAsync();
            foreach (var po in paintingOrders)
            {
                _context.PaintingOrder.Remove(po);
            }
        }

       // POST: Orders/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete()
        {
            try
            {
                var user = await GetCurrentUserAsync();
                var order = await _context.Order
                   .Where(o => o.ApplicationUserId == user.Id || user.IsAdmin == true).FirstOrDefaultAsync(o => o.IsComplete == false || user.IsAdmin == true);
                if (order == null)
                {
                    return RedirectToAction("Index", "Paintings");
                }
          
            
                await DeletePaintingOrders(order.OrderId);
                _context.Order.Remove(order);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Paintings");
            }
            catch (Exception ex)
            {

                return View();
            }
        }

        private async Task DeletePaintingOrders (int orderId)
        {
            var paintingOrders = await _context.PaintingOrder.Where(po => po.OrderId == orderId).ToListAsync();
            foreach (var po in paintingOrders)
            {
                _context.PaintingOrder.Remove(po);
            }
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}