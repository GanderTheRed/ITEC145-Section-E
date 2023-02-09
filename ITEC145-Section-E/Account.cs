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

        private int _accNum;
        private string _firstName;
        private string _lastName;
        private double _balance;
        private string _fullName;

        public int AccNum {
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

        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }

        public double Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }

        public int GetNewAccountNumber()
        {
            Random random = new Random();
            return random.Next(9999999, 99999999); 
        }

        public void Deposit(int deposit)
        {
            if(deposit < 0)
            {
                MessageBox.Show("You cannot deposit a negative amount!");
            }
            else
            {
                double finalBalance = Balance + deposit;
                Balance = finalBalance;
            }
        }

        public void Withdraw(int withdraw)
        {
            if (withdraw < 0)
            {
                MessageBox.Show("You cannot withdraw a negative amount!");
            }
            else
            {
                double finalBalance = Balance - withdraw;
                Balance = finalBalance;
            }
        }
    }
}
