using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IDCapstone.Data;
using IDCapstone.Models;
using Microsoft.AspNetCore.Authorization;

namespace IDCapstone.Controllers
{
    public class VideosController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public VideosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Videos
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var videos = from vid in _context.Videos
                         select vid;
            if (!string.IsNullOrEmpty(searchString))
            {
                videos = videos.Where(v => v.Title.Contains(searchString));
            }


            return View(await videos.ToListAsync());
        }
     
    // GET: Videos
    public async Task<IActionResult> DropDown(string searchString)
    {
 
            ViewData["DListFilter"] = searchString;
            var games = from vid in _context.Videos
                           select vid;
            if (!string.IsNullOrEmpty(searchString))
            {
                if (searchString.Contains("All"))
                {
                    return RedirectToAction("Index", "Videos");
                }
                games = games.Where(v => v.GameName.Contains(searchString));
            }


            return View(await games.ToListAsync());
    }
    // GET: Videos/Details/5
    public async Task<IActionResult> Details(int? id)
        {

          
            if (id == null)
            {
                return NotFound();
            }

            var video = await _context.Videos
               .Include(c => c.Comments)
              .ThenInclude(c => c.Video)
            
             
               .FirstOrDefaultAsync(m => m.Id == id);
            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        // GET: Videos/Create
        [Authorize(Roles = "Admin, User")]
        public IActionResult Create()
        {
            //var games = Games.ToList();
           // ViewData["Games"] = new SelectList(Games, "Text", "Value");

            return View();
        }


      



        // POST: Videos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Create([Bind("Id,Url,Title,Description,GameName")] Video video)
        {
            if (ModelState.IsValid)
            {
                _context.Add(video);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(video);
        }

        // GET: Videos/Edit/5
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Edit(int? id)
        {

           
            if (id == null)
            {
                return NotFound();
            }

            var video = await _context.Videos.FindAsync(id);
            if (video == null)
            {
                return NotFound();
            }
            return View(video);
        }

        // POST: Videos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Url,Title,Description,GameName")] Video video)
        {
            if (id != video.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(video);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideoExists(video.Id))
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
            return View(video);
        }

        // GET: Videos/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await _context.Videos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var video = await _context.Videos.FindAsync(id);
            _context.Videos.Remove(video);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideoExists(int id)
        {
            return _context.Videos.Any(e => e.Id == id);
        }
    }
}
