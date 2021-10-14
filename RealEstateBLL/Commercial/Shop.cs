using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealEstateBLL
{
    [Serializable]
    public class Shop : Commercial
    {
        private int vitrineLength;

        public Shop(int _id, Address _addressEstate, int _image, LegalForm _legalForm, Payment _paymentSystem, int _nbFloors, int _vitrineLength)
            : base(_id, _addressEstate, _image, _legalForm, _paymentSystem, _nbFloors)
        {
            vitrineLength = _vitrineLength;
            Type = Type.shop;
        }

        public int VitrineLength
        {
            get { return vitrineLength; }
            set
            {
                if (value >= 0)
                    vitrineLength = 0;
            }
        }

        public override void DisplayType()
        {
            Console.WriteLine("This is a shop!");
        }

        public override string DisplayInfo()
        {
            string Info = base.DisplayInfo() + "\n" + "Vitrine length: " + vitrineLength;
            return Info;
        }

        public override string ToString()
        {
            string Info = base.ToString() + "\t" + "Shop";
            return Info;
        }
    }
}