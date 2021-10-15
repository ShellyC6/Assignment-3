using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RealEstateDAL;

namespace RealEstateBLL
{
    public class EstateManager : ListManager<Estate>
    {
        public EstateManager()
        {

        }

        public bool CheckID(int ID)
        {
            foreach (Estate estate in m_list)
            {
                if (ID == estate.Id) return false;
            }
            return true;
        }
    }
}