namespace Library.Repositories.Models
{
    public class Book
    {
        //public int BookID { get; set; }
        public Guid BookID {get;set;}//= Guid.NewGuid();
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public double Cost { get; set; }
        public int PublicationDate { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
    }
}
