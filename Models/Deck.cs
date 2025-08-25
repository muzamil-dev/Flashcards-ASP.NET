using System.Collections.Generic;

namespace FlashcardsApp.Models
{
    public class Deck
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation property
        public ICollection<Flashcard> Flashcards { get; set; }
    }
}
