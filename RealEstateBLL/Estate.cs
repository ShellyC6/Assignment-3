using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealEstateBLL
{
    [Serializable]
    public abstract class Estate : IEstate
    {
        // fields
        private int id;
        private Address addressEstate;
        private int image;
        private LegalForm legalForm;
        private Payment paymentSystem;
        private Category category;
        private Type type;

        // constructor
        public Estate()
        {

        }
        public Estate(int _id, Address _addressEstate, int _image, LegalForm _legalForm, Payment _paymentSystem) 
        {
            id = _id;
            addressEstate = _addressEstate;
            image = _image;
            category = Category.nothing;
            type = Type.nothing;
            legalForm = _legalForm;
            paymentSystem = _paymentSystem;
        }

        // properties
        public Address AddressEstate
        {
            get { return addressEstate; }
            set { addressEstate = value; }
        }

        public int Id
        {
            get { return id; }
            set
            {
                if (value > 0)
                    id = value;
            }
        }

        public int Image
        {
            get { return image; }
            set
            {
                if (value >= 0 && value < 7)
                    image = value;
            }
        }

        public LegalForm LegalForm
        {
            get { return legalForm; }
            set { legalForm = value; }
        }

        public Payment PaymentSystem
        {
            get { return paymentSystem; }
            set { paymentSystem = value; }
        }

        public Category Category
        {
            get { return category; }
            set { category = value; }
        }

        public Type Type
        {
            get { return type; }
            set { type = value; }
        }

        // Methods

        public virtual string DisplayInfo()
        {
            string Info = "Legal form: " + legalForm.ToString() + "\n" + "ID : " + id + "\n" + addressEstate.ReturnAddress() + "\n" + "Payment - ";
            if (paymentSystem is Bank) Info += "Bank";
            else if (paymentSystem is WesternUnion) Info += "Western Union";
            else if (paymentSystem is PayPal) Info += "PayPal";
            Info += paymentSystem.ReturnPayment();

            return Info;
        }
        public override string ToString()
        {
            string Info = id + "\t" + addressEstate.Country;

            return Info;

        }

        public abstract void DisplayType();

        public string TranslateBool(bool boolean)
        {
            switch (boolean)
            {
                case false:
                    return "No";
                case true:
                    return "Yes";
            }
        }
    }

    
}