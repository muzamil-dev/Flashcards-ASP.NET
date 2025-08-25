using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FlashcardsApp.Data;
using FlashcardsApp.Models;

// namespace for the application database context
namespace FlashcardsApp.Data
{
    // Application database context class
   public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        // this is the constructor for the application database context
        // Constructor: passes options (like connection string, db provider) to base class
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // this is the Flashcards table
        public DbSet<Flashcard> Flashcards { get; set; }
        // this is the decks table
        public DbSet<Deck> Decks { get; set; }
    }
}