namespace Library.Services.Models
{
    public class BookDTO
    {

        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public string AuthorFirstName { get; set; }
        
        public string AuthorLastName { get; set; }
        
        public string Category { get; set; }
        
        public int PublicationDate { get; set; }
        
        public bool IsBorrowed { get; set; }



    }
}