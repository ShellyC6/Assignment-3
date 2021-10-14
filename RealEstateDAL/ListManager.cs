using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealEstateDAL
{
    public class ListManager<T> : IListManager<T>
    {
        private List<T> m_list;

        public ListManager()
        {
            m_list = new List<T>();
        }

        public List<T> M_list
        {
            get { return m_list; }
            set { m_list = value;  }
        }

        public int Count()
        {
              return m_list.Count;
        }

        public bool Add(T aType)
        {
            try
            {
                m_list.Add(aType);
            }
            catch(Exception e)
            {
                //System.Windows.Forms.MessageBox.Show(e.Message, "Error"); 
                return false;
            }

            return true;
        }

        public bool DeleteAt(int anIndex)
        {
            try
            {
                m_list.RemoveAt(anIndex);
            }
            catch(Exception e)
            {
               //System.Windows.Forms.MessageBox.Show(e.Message, "Error");
                return false;
            }
            return true;
        }

        public bool ChangeAt(T aType, int anIndex)
        {
            try
            {
                m_list.RemoveAt(anIndex);
                m_list.Insert(anIndex, aType);
            }
            catch(Exception e)
            {
                //System.Windows.Forms.MessageBox.Show(e.Message, "Error");
                return false;
            }
            return true;
        }

        public T GetAt(int anIndex)
        {
                return m_list[anIndex];
        }

        public bool CheckIndex(int index)
        {
            if (index >= 0 && index < m_list.Count)
                return true;
            else return false;

        }
        public void DeleteAll()
        {
            m_list.Clear();
            //m_list = null;
        }
        
        public List<string> ToStringList()
        {
            var aList = new List<String>();
            foreach (var it in m_list)
            {
                aList.Add(it.ToString());
            }
            return aList;
        }

        public string[] ToStringArray()
        {
            string[] anArray = new string[Count()];
            for (int i=0; i<Count(); i++)
            {
                anArray[i] = m_list[i].ToString();
            }
            return anArray;
        }



    }
}