using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment1
{
    [Serializable]
    public class Apartment : Residential
    {
        private bool balcony;

        public Apartment(int _id, Address _addressEstate, int _image, LegalForm _legalForm, Payment _paymentSystem, int _nbOfRooms, bool _balcony)
            : base(_id, _addressEstate, _image, _legalForm, _paymentSystem, _nbOfRooms)
        {
            balcony = _balcony;
            Type = Type.apartment;
        }

        public bool Balcony
        {
            get { return balcony; }
            set { balcony = value; }
        }

        public override void DisplayType()
        {
            Console.WriteLine("This is an apartment!");
        }

        public override string DisplayInfo()
        {
            string Info = base.DisplayInfo() + "\n" + "Balcony: " + TranslateBool(balcony);
            return Info;
        }
        public override string ToString()
        {
            string Info = base.ToString() + "\t" + "Appartment";
            return Info;
        }
    }
}