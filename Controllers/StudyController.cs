using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlashcardsApp.Data;
using FlashcardsApp.Models;

namespace MyWebApp.Controllers
{
    public class StudyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Study/Start?deckId=1
        public async Task<IActionResult> Start(int deckId)
        {
            var deck = await _context.Decks
                .Include(d => d.Flashcards)
                .FirstOrDefaultAsync(d => d.Id == deckId);

            if (deck == null)
                return NotFound();

            // Strip out deck reference to avoid cycles
            var safeFlashcards = deck.Flashcards
                .Select(f => new { f.Id, f.Question, f.Answer })
                .ToList();

            // Pass both deck name and flashcards to the view
            ViewBag.Flashcards = safeFlashcards;
            return View(deck);
        }

    }
}
