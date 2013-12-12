namespace xml_search_app.Models
{
    public class Name
    {
        public Name()
        { }

        public Name(string firstName, string lastName, string middleName)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
        }

        public string FirstName
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }
        public string MiddleName
        {
            get;
            set;
        }
    }
}
