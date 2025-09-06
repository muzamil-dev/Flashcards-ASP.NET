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

            return View(deck); // Pass the deck (with flashcards) to view
        }

        // GET: Flashcards/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var flashcard = await _context.Flashcards
                .Include(f => f.Deck)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (flashcard == null)
                return NotFound();

            return View(flashcard);
        }

        // GET: Flashcards/Create?deckId=1
        public IActionResult Create(int deckId)
        {
            ViewBag.DeckId = deckId;
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
                return RedirectToAction(nameof(Index), new { deckId = flashcard.DeckId });
            }
            return View(flashcard);
        }

        // GET: Flashcards/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var flashcard = await _context.Flashcards.FindAsync(id);
            if (flashcard == null)
                return NotFound();

            return View(flashcard);
        }

        // POST: Flashcards/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Question,Answer,DeckId")] Flashcard flashcard)
        {
            if (id != flashcard.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flashcard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Flashcards.Any(e => e.Id == flashcard.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index), new { deckId = flashcard.DeckId });
            }
            return View(flashcard);
        }

        // GET: Flashcards/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var flashcard = await _context.Flashcards
                .Include(f => f.Deck)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (flashcard == null)
                return NotFound();

            return View(flashcard);
        }

        // POST: Flashcards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flashcard = await _context.Flashcards.FindAsync(id);
            if (flashcard != null)
            {
                _context.Flashcards.Remove(flashcard);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index), new { deckId = flashcard?.DeckId });
        }
    }
}
