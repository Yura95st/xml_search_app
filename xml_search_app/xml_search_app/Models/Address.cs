namespace xml_search_app.Models
{
    public class Address
    {
        public Address()
        { }

        public Address(string city, string street, string house, int apartment)
        {
            City = city;
            Street = street;
            House = house;
            Apartment = apartment;
        }

        public string City
        {
            get;
            set;
        }

        public string Street
        {
            get;
            set;
        }

        public string House
        {
            get;
            set;
        }

        public int Apartment 
        {
            get;
            set;
        }
    }
}
