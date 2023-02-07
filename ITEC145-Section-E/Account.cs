using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC145_Section_E
{
    class Account
    {
        //Variables to include in one account.

        private string _accNum;
        private string _firstName;
        private string _lastName;
        private string _balance;

        public string AccNum {
            get { return _accNum; }
            set { _accNum = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }
    }
}
