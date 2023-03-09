namespace libraryAPI.Models
{
    public class BookDTO
    {

        public string Title {
            get { return Title; }

            set
            {
                if (char.IsUpper(value[0]))
                    Title = value;
                else;
                    throw new System.Exception("Title must start with uppercase letter");
            }
        }
        public string Description
        {
            get { return Description; }

            set
            {
                if (char.IsUpper(value[0]))
                    if (value.Length > 100)
                        Description = value;
                    else
                        throw new System.Exception("Description must be over 100 characters long");
                else
                    throw new System.Exception("Description must start with uppercase letter");
            }
        }
        public string Author
        {
            get { return Author; }

            set
            {
                var names = value.Split(' ');
                if (char.IsUpper(names[0][0]) && char.IsUpper(names[1][0]))
                    Author = value;
                else
                    throw new System.Exception("Author's name and surname must start with uppercase letter");
            }
        }
        public string Category { get; set; }
        public int PublicationDate {
            get { return PublicationDate; }
            set
            {
                if (value < 200 || value > DateTime.Now.Year)
                    throw new System.Exception("Year of publcation is wrong");
                else
                    PublicationDate = value;
            }
            }
        public double Cost {
            get { return Cost; }
            set
            {
                if (value < 0)
                    throw new System.Exception("Cost can't be lower than 0");
                else
                    Cost = value;
            }
        }


    }
}
