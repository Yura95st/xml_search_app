namespace xml_search_app.Models
{
    public class BookItem
    {
        private Name _name;
        private Address _address;
        private int _phoneNumber = 0;

        public BookItem()
        { }

        public BookItem(Name name, Address address, int phoneNumber)
        {
            _name = name;
            _address = address;
            _phoneNumber = phoneNumber;
        }

        public Name Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public Address Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
            }
        }

        public int PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = value;
            }
        }
    }
}
