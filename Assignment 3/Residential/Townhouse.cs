using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment3
{
    [Serializable]
    public class Townhouse : Residential
    {
        private int gardenSize;

        public Townhouse(int _id, Address _addressEstate, int _image, LegalForm _legalForm, Payment _paymentSystem, int _nbOfRooms, int _gardenSize)
            : base(_id, _addressEstate, _image, _legalForm, _paymentSystem, _nbOfRooms)
        {
            gardenSize = _gardenSize;
            Type = Type.townhouse;
        }

        public int GardenSize
        {
            get { return gardenSize; }
            set { gardenSize = value; }
        }

        public override void DisplayType()
        {
            Console.WriteLine("This is a townhouse!");
        }

        public override string DisplayInfo()
        {
            string Info = base.DisplayInfo() + "\n" + "Size of the garden: " + gardenSize;
            return Info;
        }
        public override string ToString()
        {
            string Info = base.ToString() + "\t" + "Townhouse";
            return Info;
        }
    }
}