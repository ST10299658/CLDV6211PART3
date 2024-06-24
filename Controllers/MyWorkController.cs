using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KhumaloCraft.Models;

namespace KhumaloCraft.Controllers
{
    public class MyWorkController : Controller
    {
        private readonly KhumaloCraftContext _context;

        public MyWorkController(KhumaloCraftContext context)
        {
            _context = context;
        }

        // GET: MyWork
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        // GET: MyWork/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: MyWork/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MyWork/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Price,Category,Availability,Description,ImageUrl")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        public IActionResult AddToCart([Bind("TransactionID,Username,ProductId,ProductName,Price,Category,Availability,Description,ImageUrl")] Product product)
        {
           // var username = HttpContext.Session.GetString("Username");  // Replace GetUserId() with your actual method to retrieve the user's ID
          

            if (product != null )
            {
                // Create a new cart item
                var cartItem = new Transaction
                {
                    Username = HttpContext.Session.GetString("Username"),
                 
                    ImageUrl = product.ImageUrl,
                   ProductName = product.ProductName,
                   Description = product.Description,
                    ProductId = product.ProductId,
                    Availability = product.Availability,
                   Price = product.Price,
                   // Product = product.Description,
                  
                };

                // Add the cart item to the database
                _context.Transactions.Add(cartItem);
                _context.SaveChanges();

                // Redirect or return appropriate response
                return RedirectToAction("Success", "Home"); // Example: Redirect to the cart page
            }
            else
            {
                // Handle error (e.g., product not found or user not logged in)
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: MyWork/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: MyWork/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Price,Category,Availability,Description,ImageUrl")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            return View(product);
        }

        // GET: MyWork/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: MyWork/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
