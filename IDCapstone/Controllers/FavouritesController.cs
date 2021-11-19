using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IDCapstone.Data;
using IDCapstone.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace IDCapstone.Controllers
    
{
    
    public class FavouritesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public FavouritesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        [Authorize(Roles = "Admin, User")]
        // GET: Favourites
        public async Task<IActionResult> Index()
        {
          var  userId = _userManager.GetUserId(HttpContext.User);
            var getFavs = from x in _context.Favourites
                          where x.UserId == userId 
                          select x;

            var applicationDbContext = _context.Favourites.Include(f => f.Video).Include(g => g.User).Where(g =>g.UserId == userId);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        // GET: Favourites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favourite = await _context.Favourites
                .Include(f => f.Video)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favourite == null)
            {
                return NotFound();
            }

            return View(favourite);
        }
        [Authorize(Roles = "Admin, User")]
        // GET: Favourites/Create
        public IActionResult Create(int videoId)
        {
            try
            {
                ViewData["VideoId"] = new SelectList(_context.Videos, "Id", "Id", videoId);
            }catch(Exception ex)
            {
                return NotFound();
            }
          //  ViewData["User"] = new SelectList(_context.Users, "Name", "Name");
            return View();
        }

        // POST: Favourites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Create([Bind("Id,VideoId")] Favourite favourite)
        {


            try
            {

                if (ModelState.IsValid)
                {
                    var userId = _userManager.GetUserId(HttpContext.User);
                    favourite.UserId = userId;
                    _context.Add(favourite);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }catch(Exception ex)


            {
                //  Response.StatusCode = 404;
                return NotFound();
            }
            ViewData["VideoId"] = new SelectList(_context.Videos, "Id", "Id", favourite.VideoId);
            return View(favourite);
        }
        [Authorize(Roles = "Admin")]
        // GET: Favourites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favourite = await _context.Favourites.FindAsync(id);
            if (favourite == null)
            {
                return NotFound();
            }
            ViewData["VideoId"] = new SelectList(_context.Videos, "Id", "Id", favourite.VideoId);
            return View(favourite);
        }

        // POST: Favourites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VideoId")] Favourite favourite)
        {
            if (id != favourite.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(favourite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavouriteExists(favourite.Id))
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
            ViewData["VideoId"] = new SelectList(_context.Videos, "Id", "Id", favourite.VideoId);
            return View(favourite);
        }

        // GET: Favourites/Delete/5
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favourite = await _context.Favourites
                .Include(f => f.Video)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favourite == null)
            {
                return NotFound();
            }

            return View(favourite);
        }

        // POST: Favourites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var favourite = await _context.Favourites.FindAsync(id);
            _context.Favourites.Remove(favourite);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FavouriteExists(int id)
        {
            return _context.Favourites.Any(e => e.Id == id);
        }
    }
}
