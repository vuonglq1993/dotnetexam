using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ComicBooksController : Controller
    {
        private readonly ComicContext _context;

        public ComicBooksController(ComicContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.ComicBooks.ToListAsync());
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(ComicBook comicBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comicBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comicBook);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var comic = await _context.ComicBooks.FindAsync(id);
            if (comic == null) return NotFound();
            return View(comic);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ComicBook comicBook)
        {
            if (ModelState.IsValid)
            {
                _context.Update(comicBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comicBook);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var comic = await _context.ComicBooks.FindAsync(id);
            if (comic != null)
            {
                _context.ComicBooks.Remove(comic);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}