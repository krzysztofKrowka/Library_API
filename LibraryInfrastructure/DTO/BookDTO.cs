namespace LibraryInfrastructure.DTO
{
    public class BookDTO
    {

        public string Title { get; set; } /*{
            get { return Title; }

            set
            {
                if (char.IsUpper(value[0]))
                    Title = value;
               // else
                    //throw new System.Exception("Title must start with uppercase letter");
            }
        }*/
        public string Description { get; set; }/*
        {
            get { return Description; }

            set
            {
                if (char.IsUpper(value[0]))
                    Description = value;
              //  else
                  //  throw new System.Exception("Description must start with uppercase letter");
            }
        }*/
        public string Author { get; set; }/*
        {
            get { return Author; }

            set
            {
              //  var names = value.Split(' ');
              //  if (char.IsUpper(names[0][0]) && char.IsUpper(names[1][0]))
                    Author = value;
          //      else
                  //  throw new System.Exception("Author's name and surname must start with uppercase letter");
            }
        }*/
        public string Category { get; set; }
        public int PublicationDate { get; set; } /*{
            get { return PublicationDate; }
            set
            {
             //   if (value < 200 || value > DateTime.Now.Year)
                 //   throw new System.Exception("Year of publcation is wrong");
              //  else
                    PublicationDate = value;
            }
            }*/
        public double Cost { get; set; } /*{
            get { return Cost; }
            set
            {
               // if (value < 0)
               //     throw new System.Exception("Cost can't be lower than 0");
               // else
                    Cost = value;
            }
        }*/


    }
}
