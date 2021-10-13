using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment3
{
    [Serializable]
    public class WesternUnion : Payment
    {
        private string name;
        private string email;

        public WesternUnion(int _amount, string _options, string _name, string _email)
            : base(_amount, _options)
        {
            name = _name;
            email = _email;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public override string ReturnPayment()
        {
            string Info = base.ReturnPayment() + "\n   Name: " + name + "\n   Email: " + email;
            return Info;
        }
    }
}