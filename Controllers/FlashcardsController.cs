using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlashcardsApp.Data;
using FlashcardsApp.Models;

namespace FlashcardsApp.Controllers
{
    public class FlashcardsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FlashcardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Flashcards?deckId=1
        public async Task<IActionResult> Index(int deckId)
        {
            var deck = await _context.Decks
                .Include(d => d.Flashcards)
                .FirstOrDefaultAsync(d => d.Id == deckId);

            if (deck == null)
                return NotFound();

            return View(deck); // Pass whole deck (with flashcards) to view
        }

        // GET: Flashcards/Create?deckId=1
        public IActionResult Create(int deckId)
        {
            ViewBag.DeckId = deckId; // Pass deckId into the view
            return View();
        }

        // POST: Flashcards/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Question,Answer,DeckId")] Flashcard flashcard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flashcard);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { deckId = flashcard.DeckId });
            }
            return View(flashcard);
        }
    }
}
