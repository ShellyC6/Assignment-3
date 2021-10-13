using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment3
{
    [Serializable]
    public class Commercial : Estate
    {
        private int nbFloors;

        public Commercial(int _id, Address _addressEstate, int _image, LegalForm _legalForm, Payment _paymentSystem, int _nbFloors)
            : base(_id, _addressEstate, _image, _legalForm, _paymentSystem)
        {
            nbFloors = _nbFloors;
            Category = Category.commercial;
        }

        public int NbFloors
        {
            get { return nbFloors; }
            set
            {
                if (value > 0)
                    nbFloors = value;
            }
        }

        public override void DisplayType()
        {
            Console.WriteLine("This is a commercial building!");
        }

        public override string DisplayInfo()
        {
            string Info = base.DisplayInfo() + "\n" + "Number of floors: " + nbFloors;
            return Info;
        }
        public override string ToString()
        {
            string Info = base.ToString() + "\t" + "Commercial";
            return Info;
        }

    }
}