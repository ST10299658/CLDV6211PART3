using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KhumaloCraft.Models;
using Microsoft.AspNetCore.Http;

namespace KhumaloCraft.Controllers
{
    public class RegisterLoginController : Controller
    {
        private readonly KhumaloCraftContext _context;

        public RegisterLoginController(KhumaloCraftContext context)
        {
            _context = context;
        }

        // GET: RegisterLogin
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: RegisterLogin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: RegisterLogin/Create
        public IActionResult Create()
        {
            return View();
        }

       



        // POST: RegisterLogin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Username,Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "RegisterLogin");
            }
            return View(user);
        }
        public async Task<IActionResult> Login(String username, String password)
        {
            if (ModelState.IsValid)
            {
                // Check if the provided username and password match a user in the database
                var userDB = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
                var AdminDB = await _context.Admins
                    .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

                if (AdminDB != null ) // Corrected from "user != null" to "usert != null"
                {
                    // User found, log them in (you may implement your authentication mechanism here)
                    // For demonstration purposes, we'll just set a flag in session to indicate the user is logged in
                  
                    return RedirectToAction("Index", "AdminPage");
                }
                else 
                if (userDB != null) // Corrected from "user != null" to "usert != null"
                {
                    // User found, log them in (you may implement your authentication mechanism here)
                    // For demonstration purposes, we'll just set a flag in session to indicate the user is logged in
                    HttpContext.Session.SetString("Username", userDB.Username);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // User not found, display error message
                    //ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    ModelState.AddModelError("Username", "Incorrect username, please try again");
                    ModelState.AddModelError("PasswordHash", "Incorrect password please try again");
                }
              
            }

            // If ModelState is not valid or user not found, return to login view with validation errors
            return View("Login");
        }


        // GET: RegisterLogin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: RegisterLogin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Username,Email,Password")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
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
            return View(user);
        }

        // GET: RegisterLogin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: RegisterLogin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
