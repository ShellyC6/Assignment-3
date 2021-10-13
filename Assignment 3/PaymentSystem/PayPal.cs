using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment3
{
    [Serializable]
    public class PayPal : Payment
    {
        private string email;

        public PayPal(int _amount, string _options, string _email)
            : base(_amount, _options)
        {
            email = _email;
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public override string ReturnPayment()
        {
            string Info = base.ReturnPayment() + "\n   Email: " + email;
            return Info;
        }
    }
}