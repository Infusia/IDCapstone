using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IDCapstone.Data;
using IDCapstone.Models;

namespace IDCapstone.Controllers
{
    public class PlayerVideosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlayerVideosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PlayerVideos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PlayerVideos.Include(p => p.Player);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PlayerVideos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerVideo = await _context.PlayerVideos
                .Include(p => p.Player)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playerVideo == null)
            {
                return NotFound();
            }

            return View(playerVideo);
        }



        [HttpGet]
        public async Task<IActionResult> CreateVideoAsync(int playerId)
        {

            var playerSelectList = await _context.Players
                .OrderBy(player => player.PlayerName)
                .Select(player =>
                new SelectListItem()
                {
                    Text = player.PlayerName,
                    Value = player.Id.ToString(),
                    Selected = playerId == player.Id
                })
                .ToListAsync();

            return View(playerSelectList);

        }

        [HttpPost]
        public async Task<IActionResult> CreateVideoAsync(int playerId, string clipNameOrUrl)
        {
            if (string.IsNullOrEmpty(clipNameOrUrl))
            {
                throw new NullReferenceException("Please enter url");
            }


            Video video = new Video();
            video.Url = clipNameOrUrl;

            _context.Videos.Add(video);
            await _context.SaveChangesAsync();


            PlayerVideo pv = new PlayerVideo();
            pv.VideoId = video.Id;
            pv.PlayerId = playerId;
         //   pv.IsPrimary = false;

            _context.PlayerVideos.Add(pv);


            await _context.SaveChangesAsync();


            return RedirectToAction("Details", "Players", new { id = playerId });
        }


        // GET: PlayerVideos/Create
        public IActionResult Create()
        {
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Id");
            return View();
        }

        // POST: PlayerVideos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlayerId")] PlayerVideo playerVideo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(playerVideo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Id", playerVideo.PlayerId);
            return View(playerVideo);
        }

        // GET: PlayerVideos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerVideo = await _context.PlayerVideos.FindAsync(id);
            if (playerVideo == null)
            {
                return NotFound();
            }
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Id", playerVideo.PlayerId);
            return View(playerVideo);
        }

        // POST: PlayerVideos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlayerId")] PlayerVideo playerVideo)
        {
            if (id != playerVideo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playerVideo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerVideoExists(playerVideo.Id))
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
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Id", playerVideo.PlayerId);
            return View(playerVideo);
        }

        // GET: PlayerVideos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerVideo = await _context.PlayerVideos
                .Include(p => p.Player)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playerVideo == null)
            {
                return NotFound();
            }

            return View(playerVideo);
        }

        // POST: PlayerVideos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playerVideo = await _context.PlayerVideos.FindAsync(id);
            _context.PlayerVideos.Remove(playerVideo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerVideoExists(int id)
        {
            return _context.PlayerVideos.Any(e => e.Id == id);
        }
    }
}
