using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment1
{
    [Serializable]
    public class University : Institutional
    {
        private int nbFaculties;

        public University(int _id, Address _addressEstate, int _image, LegalForm _legalForm, Payment _paymentSystem, bool _library, int _nbSeatsCafeteria, int _nbFaculties)
            : base(_id, _addressEstate, _image, _legalForm, _paymentSystem, _library, _nbSeatsCafeteria)
        {
            nbFaculties = _nbFaculties;
            Type = Type.university;
        }

        public int NbFaculties
        {
            get { return nbFaculties; }
            set
            {
                if (value > 0)
                    nbFaculties = value;
            }
        }

        public override void DisplayType()
        {
            Console.WriteLine("This is a university!");
        }

        public override string DisplayInfo()
        {
            string Info = base.DisplayInfo() + "\n" + "Number of faculties: " + nbFaculties;
            return Info;
        }

        public override string ToString()
        {
            string Info = base.ToString() + "\t" + "University";
            return Info;
        }
    }
}