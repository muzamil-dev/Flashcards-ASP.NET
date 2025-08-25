namespace FlashcardsApp.Models
{
    public class Flashcard
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int DeckId { get; set; }
        public Deck Deck { get; set; }
    }
}
// {get : set} means the Question is a property of the Flashcard.
// A property is like a box that holds a value (like "Hola").

// get → means you can open the box and look inside.

// set → means you can put something new inside the box.

// If we wrote just { get; } → you can only look, not change it (read-only).

// If we wrote just { set; } → you can only change it, not look at it (write-only, very rare).