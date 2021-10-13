using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment1
{
    [Serializable]
    public class Villa : Residential
    {
        // fields
        private bool pool;

        // constructor
        public Villa (int _id, Address _addressEstate, int _image, LegalForm _legalForm, Payment _paymentSystem, int _nbOfRooms, bool _pool)
            : base(_id, _addressEstate, _image, _legalForm, _paymentSystem, _nbOfRooms)
        {
            pool = _pool;
            Type = Type.villa;
        }

        // properties
        public bool Pool
        {
           get { return pool; }
           set { pool = value; }
        }

        // methods
        public override void DisplayType()
        {
            Console.WriteLine("This is a Villa!");
        }

        public override string DisplayInfo()
        {
            string Info = base.DisplayInfo() + "\n" + "Pool: " + TranslateBool(pool);
            return Info;
        }
        public override string ToString()
        {
            string Info = base.ToString() + "\t" + "Villa";
            return Info;
        }
    }
}