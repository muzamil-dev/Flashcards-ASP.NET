using System.ComponentModel.DataAnnotations;

namespace FlashcardsApp.Models
{
    public class Deck
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Deck name is required")]
        public string Name { get; set; } = string.Empty;

        public ICollection<Flashcard> Flashcards { get; set; }

        public Deck()
        {
            Flashcards = new List<Flashcard>();
        }
    }
}
