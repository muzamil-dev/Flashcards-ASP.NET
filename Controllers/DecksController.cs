using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlashcardsApp.Data;
using FlashcardsApp.Models;

namespace MyWebApp.Controllers
{
    public class DecksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DecksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Decks/Ping
        [HttpGet]
        public IActionResult Ping()
        {
            return Content("DecksController is reachable.");
        }

        // GET: Decks
        public async Task<IActionResult> Index()
        {
            var decks = await _context.Decks
                .Include(d => d.Flashcards)
                .ToListAsync();
            return View(decks);
        }

        // GET: Decks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Decks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Deck deck)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deck);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deck);
        }
    }
}
