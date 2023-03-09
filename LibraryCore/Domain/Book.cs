using LibraryCore.Domain;

namespace LibraryCore.Domain
{
    public class Book : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public double Cost { get; set; }
        public int PublicationDate { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
    }
}
