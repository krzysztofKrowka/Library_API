namespace Library.Repositories.Models
{
    public class Book
    {
        public Guid BookID {get;set;}
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsBorrowed { get; set; }
        public int PublicationDate { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public Guid AuthorID { get; set; }
        
        // It's an infinite loop of books, gotta change that
        // I will just not set this to an 
        public virtual Author Author { get; set; }
    }
}
