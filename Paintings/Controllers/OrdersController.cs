using System;
using System.Collections.Generic;
using System.Linq;
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
                .Where(o => o.ApplicationUserId == user.Id);
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
                        .ThenInclude(op => op.Painting)
            .FirstOrDefaultAsync();
            if (incompleteOrder != null)
            {
                var orderDetailViewModel = new OrderDetailViewModel();
                orderDetailViewModel.LineItems = incompleteOrder.PaintingOrder.GroupBy(op => op.PaintingId)
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
                var user = await GetCurrentUserAsync();
                var userOrder = _context.Order.FirstOrDefault(o => o.ApplicationUser.Id == user.Id);
                if (userOrder == null)
                {
                    //creates order object
                    var newOrder = new Order
                    {
                        IsComplete = false,
                        DateTime = DateTime.Now,
                        ApplicationUserId = user.Id
                    };
                    _context.Order.Add(newOrder);
                    await _context.SaveChangesAsync();

                    //pulls id from newly created Order to plug into OrderProduct object 
                    int orderId = newOrder.OrderId;

                    //adds product to order by creating OrderProduct object
                    var newPainting = new PaintingOrder
                    {
                        OrderId = orderId,
                        PaintingId = id
                    };
                    _context.PaintingOrder.Add(newPainting);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Orders", new {id = orderId });

                }
                else
                {

                    //creates just the order product if an order already exists
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Orders/Edit/5
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

        // GET: Orders/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Orders/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}