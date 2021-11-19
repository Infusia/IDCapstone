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
    
    public class FlaggedCommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FlaggedCommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FlaggedComments
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.FlaggedComments.Include(f => f.Comment);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FlaggedComments/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flaggedComment = await _context.FlaggedComments
                .Include(f => f.Comment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flaggedComment == null)
            {
                return NotFound();
            }

            return View(flaggedComment);
        }

        // GET: FlaggedComments/Create
        [Authorize(Roles = "Admin, User")]
        public IActionResult Create(int? commentId)
        {
            try
            {
                ViewData["CommentId"] = new SelectList(_context.Comments, "Id", "Id", commentId);
                return View();
            }catch(Exception ex)
            {
                return NotFound();
            }
        }

        // POST: FlaggedComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Create([Bind("Id,CommentId")] FlaggedComment flaggedComment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(flaggedComment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Videos");
                }
            }catch(Exception ex)
            {
                return NotFound();
            }
            ViewData["CommentId"] = new SelectList(_context.Comments, "Id", "Id", flaggedComment.CommentId);
            return RedirectToAction("Index", "Home");
        }

        // GET: FlaggedComments/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flaggedComment = await _context.FlaggedComments.FindAsync(id);
            if (flaggedComment == null)
            {
                return NotFound();
            }
            ViewData["CommentId"] = new SelectList(_context.Comments, "Id", "Id", flaggedComment.CommentId);
            return View(flaggedComment);
        }

        // POST: FlaggedComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CommentId")] FlaggedComment flaggedComment)
        {
            if (id != flaggedComment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flaggedComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlaggedCommentExists(flaggedComment.Id))
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
            ViewData["CommentId"] = new SelectList(_context.Comments, "Id", "Id", flaggedComment.CommentId);
            return View(flaggedComment);
        }

        // GET: FlaggedComments/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flaggedComment = await _context.FlaggedComments
                .Include(f => f.Comment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flaggedComment == null)
            {
                return NotFound();
            }

            return View(flaggedComment);
        }

        // POST: FlaggedComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flaggedComment = await _context.FlaggedComments.FindAsync(id);
            _context.FlaggedComments.Remove(flaggedComment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlaggedCommentExists(int id)
        {
            return _context.FlaggedComments.Any(e => e.Id == id);
        }
    }
}
