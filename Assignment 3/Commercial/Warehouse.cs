using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment3
{
    [Serializable]
    public class Warehouse : Commercial
    {
        private int nbShelfs;

        public Warehouse(int _id, Address _addressEstate, int _image, LegalForm _legalForm, Payment _paymentSystem, int _nbFloors, int _nbShelfs)
            : base(_id, _addressEstate, _image, _legalForm, _paymentSystem, _nbFloors)
        {
            nbShelfs = _nbShelfs;
            Type = Type.warehouse;
        }

        public int NbShelfs
        {
            get { return nbShelfs; }
            set
            {
                if (value > 0)
                    nbShelfs = value;
            }
        }

        public override void DisplayType()
        {
            Console.WriteLine("This is a warehouse!");
        }

        public override string DisplayInfo()
        {
            string Info = base.DisplayInfo() + "\n" + "Number of shelfs: " + nbShelfs;
            return Info;
        }
        public override string ToString()
        {
            string Info = base.ToString() + "\t" + "Warehouse" ;
            return Info;
        }
    }
}