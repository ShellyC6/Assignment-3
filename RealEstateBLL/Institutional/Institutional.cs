using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealEstateBLL
{
    [Serializable]
    public class Institutional : Estate
    {
        private bool library;
        private int nbSeatsCafeteria;

        public Institutional(int _id, Address _addressEstate, int _image, LegalForm _legalForm, Payment _paymentSystem, bool _library, int _nbSeatsCafeteria)
            : base(_id, _addressEstate, _image, _legalForm, _paymentSystem)
        {
            library = _library;
            nbSeatsCafeteria = _nbSeatsCafeteria;
            Category = Category.institutional;
        }

        public bool Library
        {
            get { return library; }
            set { library = value; }
        }

        public int NbSeatsCafeteria
        {
            get { return nbSeatsCafeteria; }
            set
            {
                if (value >= 0)
                    nbSeatsCafeteria = value;
            }
        }

        public override void DisplayType()
        {
            Console.WriteLine("This is an institutional building!");
        }

        public override string DisplayInfo()
        {
            string Info = base.DisplayInfo() + "\n" + "Seats in the cafeteria: " + nbSeatsCafeteria + "\n" + "Library: " + TranslateBool(library);
            return Info;
        }

        public override string ToString()
        {
            string Info = base.ToString() +  "\t" + "Institutionnal";
            return Info;
        }
    }
}