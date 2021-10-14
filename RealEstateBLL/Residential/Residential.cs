using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealEstateBLL
{
    [Serializable]
    public class Residential : Estate
    {
        // fields
        private int nbOfRooms;

        // constructor
        public Residential()
        {

        }
        public Residential(int _id, Address _addressEstate, int _image, LegalForm _legalForm, Payment _paymentSystem, int _nbOfRooms)
            :base(_id, _addressEstate, _image, _legalForm, _paymentSystem)
        {
            nbOfRooms = _nbOfRooms;
            Category = Category.residential;
        }

        // properties
        public int NbOfRooms
        {
            get { return nbOfRooms; }
            set
            {
                if (value > 0)
                    nbOfRooms = value;
            }
        }

        // methods
        public override void DisplayType()
        {
            Console.WriteLine("This is a residencial building!");
        }

        public override string DisplayInfo()
        {
            string Info = base.DisplayInfo() + "\n" + "Number of rooms: " + nbOfRooms;
            return Info;
        }
        public override string ToString()
        {
            string Info = base.ToString() + "\t" + "Residential";
            return Info;
        }

    }
}