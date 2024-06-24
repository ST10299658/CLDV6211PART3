using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KhumaloCraft.Models;

namespace KhumaloCraft.Controllers
{
    public class ViewOrdersController : Controller
    {
        private readonly KhumaloCraftContext _context;
        private readonly IOrderOrchestrator _orchestrationService;

        public ViewOrdersController(KhumaloCraftContext context, IOrderOrchestrator orchestrationService)
        {
            _context = context;
            _orchestrationService = orchestrationService;
        }

        // GET: ViewOrders
        public async Task<IActionResult> Index()
        {
            var username = HttpContext.Session.GetString("Username");
            ViewBag.Username = username;

            var khumaloCraftContext = _context.Transactions.Include(t => t.Product);



            return View(await khumaloCraftContext.ToListAsync());
        }

        // GET: ViewOrders/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            return View();
        }

        // POST: ViewOrders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionId,Username,ProductId,Availability,Description,ImageUrl,ProductName,Price")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();

                // Start the order processing orchestration
                await _orchestrationService.ProcessOrder(transaction);

                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", transaction.ProductId);
            return View(transaction);
        }

        // Other actions (Edit, Details, Delete) remain the same...
    }
}
