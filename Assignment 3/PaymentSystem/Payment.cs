using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment1
{
    [Serializable]
    public abstract class Payment
    {
        private int amount;
        private string options;

        public Payment(int _amount, string _options)
        {
            amount = _amount;
            if (_options != "")
                options = _options;
            else
                options = "None";
        }

        public int Amount
        {
            get { return amount; }
            set
            {
                if (value > 0)
                    amount = value;
            }
        }

        public string Options
        {
            get { return options; }
            set { options = value; }
        }

        public virtual string ReturnPayment()
        {
            string Info = "\n" + "   Amount: " + amount + "\n   Options: " + options;
            return Info;
        }

    }
}