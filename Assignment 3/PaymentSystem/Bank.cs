using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment1
{
    [Serializable]
    public class Bank : Payment
    {
        private string name;
        private int accountNumber;

        public Bank(int _amount, string _options, string _name, int _accountNumber)
            : base(_amount, _options)
        {
            name = _name;
            accountNumber = _accountNumber;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int AccountNumber
        {
            get { return accountNumber; }
            set
            {
                if (value > 0)
                    accountNumber = 0;
            }
        }

        public override string ReturnPayment()
        {
            string Info = base.ReturnPayment() + "\n   Name: " + name + "\n   Account number: " + accountNumber;
            return Info;
        }

    }
}