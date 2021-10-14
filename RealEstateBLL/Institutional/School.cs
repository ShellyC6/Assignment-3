using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealEstateBLL
{
    [Serializable]
    public class School : Institutional
    {
        private int nbClassrooms;

        public School(int _id, Address _addressEstate, int _image, LegalForm _legalForm, Payment _paymentSystem, bool _library, int _nbSeatsCafeteria, int _nbClassrooms)
            : base(_id, _addressEstate, _image, _legalForm, _paymentSystem, _library, _nbSeatsCafeteria)
        {
            nbClassrooms = _nbClassrooms;
            Type = Type.school;
        }

        public int NbClassrooms
        {
            get { return nbClassrooms; }
            set
            {
                if (value > 0)
                    nbClassrooms = value;
            }
        }

        public override void DisplayType()
        {
            Console.WriteLine("This is a school!");
        }

        public override string DisplayInfo()
        {
            string Info = base.DisplayInfo() + "\n" + "Number of classrooms: " + nbClassrooms;
            return Info;
        }

        public override string ToString()
        {
            string Info = base.ToString() + "\t" + "School";
            return Info;
        }
    }
}