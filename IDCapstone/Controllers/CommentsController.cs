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
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        // GET: Comments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Comments.ToListAsync());
        }

        // GET: Comments/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(v => v.Video)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }


        /// <summary>
        /// Custom Create method to pass in VideoId and create a comment for that video
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> CreateCommentAsync(int videoId)
        {
            try
            {
                // --WORKS BUT NOT THE WAY I WANT
                var videos = await _context.Videos
                    .OrderBy(vid => vid.Title)
                    .Where(vid => vid.Title != null)
                    .Select(vid =>
                    new SelectListItem()
                    {
                        Text = vid.Title,
                        Value = vid.Id.ToString(),
                        Selected = videoId == vid.Id
                    })
                    .ToListAsync();
                return View(videos);
            } catch (Exception ex)
            {
                return NotFound();
            }

        }
        /// <summary>
        /// Custom Create method to pass in VideoId and the text of the comment from the user
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="commentText"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin, User")]
        public async Task<IActionResult> CreateCommentAsync(int videoId, string commentText)
        {

            try
            {

                if (ModelState.IsValid)
                {

                    Comment comment = new Comment();
                    comment.VideoId = videoId;
                    comment.CommentText = commentText;
                    comment.TimeStamp = DateTime.Now;


                    _context.Comments.Add(comment);


                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Videos");
                }
            }catch(Exception ex)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }


        // GET: Comments/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,CommentText,TimeStamp")] Comment comment)
        {
            try
            {

            
            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            }catch(Exception ex)
            {
                return NotFound();
            }
            return View(comment);
        }

        // GET: Comments/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CommentText,TimeStamp")] Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
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
            return View(comment);
        }

        // GET: Comments/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
