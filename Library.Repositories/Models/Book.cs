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
        public virtual Author Author { get; set; }
    }
}
