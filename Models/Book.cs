namespace BookWebApplication.Models
{
    public class Book
    {
        public string Name { get; set; }   // Book's name
        public string Type { get; set; }   // Hardcover, Paperback, etc.
    }

    public class Owner
    {
        public string Name { get; set; }   // Owner's name
        public int Age { get; set; }       // Owner's age
        public List<Book> Books { get; set; } // List of books owned by this person
    }

}
