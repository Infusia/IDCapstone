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
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Comments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Comments.ToListAsync());
        }

        // GET: Comments/Details/5
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



        [HttpGet]
        public async Task<IActionResult> CreateCommentAsync(int videoId)
        {

            var playerSelectList = await _context.Videos
                .OrderBy(player => player.Title)
                .Select(player =>
                new SelectListItem()
                {
                    Text = player.Title,
                    Value = player.Id.ToString(),
                    Selected = videoId == player.Id
                })
                .ToListAsync();

            return View(playerSelectList);

        }

        //public async Task<IActionResult> ShowComment(int videoId)
        //{
        //    Video video = new Video();
        //    _context.Videos.Add(video);
        //    await _context.SaveChangesAsync();
        //    Comment comment = new Comment();
        //    comment.Id = videoId;
        //  //comment.CommentText = commentText;
        //    _context.Comments.Add(comment);
        //    await _context.SaveChangesAsync();
        //    return View();
        //}





        [HttpPost]
        public async Task<IActionResult> CreateCommentAsync(int videoId,  string commentText)
        {
            //ApplicationUser applicationUser = new ApplicationUser();
            //applicationUser.Id = userId;
            //_context.Users.Add(applicationUser);
           
   
            Comment comment = new Comment();
            //  comment.Id = videoId;
            //comment.Video.Comments = commentText;
            //comment.Video.Id = 4;
          //  Video video = new Video();
            comment.CommentText = commentText;
          //  video.Id = videoId;
          //  comment.Id = videoId;
        //    comment.Video.Id = videoId;
            _context.Comments.Add(comment);
           // _context.Videos.Add(video);
    
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");


        }


        // GET: Comments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CommentText,TimeStamp")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comment);
        }

        // GET: Comments/Edit/5
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
