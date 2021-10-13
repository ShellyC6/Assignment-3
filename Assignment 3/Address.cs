using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment1
{
    [Serializable]
    public class Address
    {
        private int zipCode;
        private string street;
        private int country;
        private string city;

        public Address(int _num, string _street, int _country, string _city)
        {
            zipCode = _num;
            street = _street;
            country = _country;
            city = _city;
        }
        public int ZipCode
        {
            get { return zipCode; }
            set
            {
                if (value > 0)
                    zipCode = value;
            }
        }
        public string Street
        {
            get { return street; }
            set { street = value; }
        }
        public int Country
        {
            get { return country; }
            set { country = value; }
        }

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public string ReturnAddress()
        {
            string Info = "Address \n" + "   Street: " + street + "\n   Zip code: " + zipCode + "\n   City: " + city + "\n   Country: " + (Countries)country;
            return Info;
        }
    }
}