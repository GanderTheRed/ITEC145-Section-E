using System.Linq;

namespace ITEC145_Section_E                                     //Inspiration for this was JJ, and now I bit off more than I wanted to.
{
    public partial class Form1 : Form
    {
        int labelLocationX = 120;               //Sets a starting X and Y location for my labels
        int labelLocationY = 173;               //Redundant
        int currentCount = 0;
        int depositWithdraw;                    //Redundant

        string main = "Make a Choice:";         //main menu header text
        string myAccount = "Enter Your 8 Digit Account Number:";    //Header Text Variables
        string createAccount = "Enter Your Details Below:";
        string viewAllAccounts = "Accounts:";

        string enteredNumber;               //string to store the currently entered number.
        string finalNumber;                 //string to store the final account number (retrieved from accountNumberList list)
        string noValue = "";                //Default value, if account number is cleared or cancelled.

        int enteredNumberCount;             //Count of entered numbers.

        bool inMyAccount = false;           //Flags for if a state is ready for changes to the screen
        bool inCreateAccount = false;
        bool inViewAllAccounts = false;
        bool inDisplayAccount = false;
        bool inMyDeposit = false;
        bool inMyWithdrawal = false;

        Account ShowAccount;

        List<string> accountNumberList = new List<string>();   
        List<Label> labels = new List<Label>();     //List to keep track of my created labels to easily delete them later.
        List<TextBox> textboxes = new List<TextBox>();
        List<string> alphabet = new List<string>();
        List<bool> fieldCheck = new List<bool>();
        List<Account> accounts = new List<Account>();
               
        enum Menu
        {
            Main,               //States of my teller machine for if statements (CheckState() method)
            MyAccount,
            CreateAccount,
            ViewAll,
            NewAccount,
            DisplayAccount,
            Deposit,
            Withdraw
        }
        Menu BankMenu = Menu.Main;  //Default state on startup

        public Form1()
        {
            InitializeComponent();
            finalNumber = lblAccountNumber.Text;
            for(char c = 'A'; c <= 'Z'; c++)
            {
                alphabet.Add(c.ToString());                               //Took awhile to figure out characters need single quotes
            }
        }
        public void MakeLabel(string message, int locationy)
        {
            Font font = new Font("Segoe UI", 10);
            Label lbl = new Label();
            lbl.Top = labelLocationX + locationy;
            lbl.Left = labelLocationX;
            lbl.AutoSize = true;
            lbl.Text = message;                                 //Properties for my created labels
            lbl.ForeColor = Color.Chartreuse;
            lbl.BackColor = Color.Black;
            lbl.Font = font;
            lbl.Name = "Menu";
            Controls.Add(lbl);
            lbl.BringToFront();
            labels.Add(lbl);
        }
        public void MakeTextBox(string name, int locationy)
        {
            Font font = new Font("Segoe UI", 10);
            TextBox txt = new TextBox();
            txt.Top = labelLocationX + locationy;
            txt.Left = labelLocationX + 10;
            txt.Font = font;
            txt.ForeColor = Color.Chartreuse;
            txt.BackColor = Color.Black;
            txt.Name = name;
            Controls.Add(txt);
            txt.BringToFront();
            textboxes.Add(txt);
            txt.PlaceholderText = "_";
        }
        public void ClearScreen()                           //Removes all labels that have been added to list
        {
            foreach (Label label in labels)
            {
                Controls.Remove(label);
            }
            labels.Clear();
            foreach (TextBox txt in textboxes)
            {
                Controls.Remove(txt);
            }
            textboxes.Clear();
        }

        public void CheckState()                                //Used to decide what to draw on the screen
        {
            if(BankMenu == Menu.Main)                                   
            {
                lblHeader.Text = main;
                MakeLabel("1. View My Account.", 50);
                MakeLabel("2. Create a New Account.", 70);
                MakeLabel("3. View ALL Accounts.", 90);
            }
            else if (BankMenu == Menu.MyAccount)
            {
                lblHeader.Text = myAccount;
                lblAccountNumber.Visible = true;
            }
            else if (BankMenu == Menu.NewAccount)
            {
                Account yourAccount = accounts.Last<Account>();
                ClearScreen();
                MakeLabel($"Your Account Number is: {yourAccount.AccNum}", 55);
                MakeLabel("Write it Down!", 95);
                MakeLabel("Press Cancel to Return to Main Menu...", 135);
            }
            else if(BankMenu == Menu.ViewAll)           //This section was the worst to figure out... I think I'll try to make my program look like yours next time...
            {
                ClearScreen();
                int position = 55;
                int maxOnScreen = 2;
                int maxCount = accounts.Count;
                
                for(int i = 0; i < maxOnScreen; i++)
                {
                    lblHeader.Text = viewAllAccounts;
                    MakeLabel($"Account ID: {accounts[currentCount].AccNum.ToString()}", position);
                    MakeLabel($"{accounts[currentCount].FullName}, Balance: {accounts[currentCount].Balance.ToString()}", position + 20);
                    position += 45;
                    currentCount++;

                    if (maxCount - i == 1)
                    {
                        i = 2;
                        MakeLabel("Press Cancel to Exit...", position + 25);
                        currentCount = 0;
                    }

                    if (currentCount == maxCount)
                    {
                        i = maxCount;
                        MakeLabel("Press Cancel to Exit...", position + 25);
                        currentCount = 0;
                    }
                    else if(i > 0 && currentCount > i)
                    {
                        MakeLabel("Press 4 for More...", position + 25);
                    }
                }
                if (currentCount == maxCount)
                    {

                    }
                
            }
            else if(BankMenu == Menu.CreateAccount)
            {
                if(inCreateAccount == false)
                {
                    ClearScreen();
                    lblHeader.Text = createAccount;
                    MakeLabel("Enter Your First Name:", 55);
                    MakeTextBox("FirstName", 75);
                    MakeLabel("Enter Your Surname:", 100);
                    MakeTextBox("LastName", 120);
                    MakeLabel("Press Enter to Continue...", 155);
                    inCreateAccount = true;
                }
                else
                {
                    try
                    {
                        bool passed;
                        Account Account = new Account();        //Potential problem with empty accounts if my if's don't pass

                        foreach (TextBox txt in textboxes)      //This was all was a pain in the butt and hurt my brain
                        {
                            List<String> testList = new List<String>();

                            for(int i = 0; i < txt.Text.Length; i++)
                            {
                                testList.Add(txt.Text.ToUpper().Substring(i,1));          //Sorry for writing this all out in one line. Adds each letter to testList.
                            }

                            foreach (string c in testList)                     //Checks each letter is a legal character and adds it to a true/false list
                            {
                                if (alphabet.Contains(c))
                                {
                                    passed = true;
                                    fieldCheck.Add(passed);
                                }
                                else
                                {
                                    passed = false;
                                    fieldCheck.Add(passed);
                                }
                            }

                            if (fieldCheck.Contains(false))                     //If an illegal character exists
                            {
                                fieldCheck.Clear();
                                throw new Exception();
                            }
                            else
                            {
                                if (txt.Name == "FirstName")
                                {
                                    Account.FirstName = txt.Text;
                                }
                                if (txt.Name == "LastName")
                                {
                                    Account.LastName = txt.Text;
                                }
                                if (Account.FirstName != null && Account.LastName != null)      //This was more brain pain
                                {
                                    Account.FullName = Account.FirstName + " " + Account.LastName;
                                    Account.Balance = 0;
                                    Account.AccNum = Account.GetNewAccountNumber();     //Need to check for account numbers that already exist, hopefully I don't forget.

                                    accounts.Add(Account);
                                    BankMenu = Menu.NewAccount;
                                    inCreateAccount = false;
                                }
                            }
                        }
                    }
                    catch
                    {
                        inCreateAccount = false;
                        MessageBox.Show("Your fields contain non-alphabetical characters, please try again");
                    }
                    CheckState();                       
                }
            }
            else if (BankMenu == Menu.DisplayAccount)
            {
                if (inDisplayAccount == false)
                {
                    ClearScreen();
                    lblHeader.Text = "Your Account Details...";
                    MakeLabel($"Account Number:{ShowAccount.AccNum}", 55);
                    MakeLabel($"Name: {ShowAccount.FullName}", 80);
                    MakeLabel($"Balance: {ShowAccount.Balance}", 105);
                    MakeLabel($"1.Deposit | 2.Withdraw", 130);
                    inDisplayAccount = true;
                }
            }
            else if (inDisplayAccount == true && BankMenu == Menu.Withdraw)
            {
                ClearScreen();
                lblHeader.Text = "Enter Withdrawal Amount";
                lblAccountNumber.Visible = true;
            }
            else if (inDisplayAccount == true && BankMenu == Menu.Deposit)
            {
                ClearScreen();
                lblHeader.Text = "Enter Deposit Amount";
                lblAccountNumber.Visible = true;
            }
        }
        public void PassButtonValueAndCheckState(string enteredNumber)              //Used to pass through a buttons number and to also check the state of the menu
        {
            if (BankMenu == Menu.Main)
            {

            }
            else if (BankMenu == Menu.MyAccount)
            {
                if (enteredNumberCount == 8)        //Ends once every string in list has been changed.
                {
                    //Do nothing
                }
                else
                {
                    if (inMyAccount == false)
                    {
                        ClearScreen();
                        CheckState();
                        inMyAccount = true;
                    }
                    else
                    {
                        accountNumberList.Add(enteredNumber);
                        string result = String.Join(" ", accountNumberList);    //Learnt about String.Join({char}, array) from stack overflow. 
                        lblAccountNumber.Text = result;
                        enteredNumberCount++;
                    }
                }
            }
            else if (BankMenu == Menu.ViewAll)
            {
                if (inViewAllAccounts == false)
                {
                    ClearScreen();
                    CheckState();
                    inViewAllAccounts = true;
                }
                else
                {
                    //do stuff
                }
            }
            else if (BankMenu == Menu.Deposit)
            {
                if (enteredNumberCount == 8)        //Ends once every string in list has been changed.
                {
                    //Do nothing
                }
                else
                {
                    if (inMyAccount == false)
                    {
                        ClearScreen();
                        inMyDeposit = true;
                        CheckState();
                        inMyAccount = true;
                    }
                    else if(inMyDeposit == true)
                    {
                        inMyDeposit = false;    
                        lblAccountNumber.Text = noValue;                        //Hack to fix code
                        PassButtonValueAndCheckState(enteredNumber);
                    }
                    else
                    {
                        accountNumberList.Add(enteredNumber);
                        string result = String.Join("", accountNumberList);    //Learnt about String.Join({char}, array) from stack overflow. 
                        lblAccountNumber.Text = result;
                        enteredNumberCount++;
                    }
                }
            }
            else if (BankMenu == Menu.Withdraw)
            {
                if (enteredNumberCount == 8)        //Ends once every string in list has been changed.
                {
                    //Do nothing
                }
                else
                {
                    if (inMyAccount == false)
                    {
                        ClearScreen();
                        inMyWithdrawal = true;
                        CheckState();
                        inMyAccount = true;
                    }
                    else if (inMyDeposit == true)
                    {
                        inMyWithdrawal = false;   
                        lblAccountNumber.Text = noValue;                     //Hack to fix code
                        PassButtonValueAndCheckState(enteredNumber);
                    }
                    else
                    {
                        accountNumberList.Add(enteredNumber);
                        string result = String.Join("", accountNumberList);    //Learnt about String.Join({char}, array) from stack overflow. 
                        lblAccountNumber.Text = result;
                        enteredNumberCount++;
                    }
                }
            }
            else
            {
                //do nothing
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            MakeLabel("1. View My Account.", 50);
            MakeLabel("2. Create a New Account.", 70);
            MakeLabel("3. View ALL Accounts.", 90);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
                if(BankMenu == Menu.ViewAll)
                {
                    currentCount = 0;
                }

                BankMenu = Menu.Main;
                inMyAccount = false;
                inCreateAccount = false;
                inViewAllAccounts = false;
                lblAccountNumber.Visible = false;
                lblAccountNumber.Text = noValue;
                finalNumber = noValue;
                enteredNumberCount = 0;
                accountNumberList.Clear();
                ClearScreen();
                CheckState();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (BankMenu == Menu.MyAccount)
            {
                finalNumber = noValue;
                accountNumberList.Clear();
                accountNumberList.Add(finalNumber);
                lblAccountNumber.Text = noValue;
                enteredNumberCount = 0;
            }
            else if (BankMenu == Menu.Deposit)
            {
                finalNumber = noValue;
                accountNumberList.Clear();
                accountNumberList.Add(finalNumber);
                lblAccountNumber.Text = noValue;
                enteredNumberCount = 0;
            }
            else if (BankMenu == Menu.Withdraw)
            {
                finalNumber = noValue;
                accountNumberList.Clear();
                accountNumberList.Add(finalNumber);
                lblAccountNumber.Text = noValue;
                enteredNumberCount = 0;
            }
        }
        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (BankMenu == Menu.MyAccount)
            {
                if (enteredNumberCount == 8)
                {
                    string enteredAccountNumber = "";

                    for (int i = 0; i < accountNumberList.Count; i++)
                    {
                        enteredAccountNumber += accountNumberList[i];
                    }
                    int finalAccountNumber = int.Parse(enteredAccountNumber);
                    
                    foreach(Account accNumber in accounts)
                    {
                        if (accNumber.AccNum == finalAccountNumber)
                        {
                            inDisplayAccount = false;
                            BankMenu = Menu.DisplayAccount;
                            ShowAccount = accNumber;
                            lblAccountNumber.Text = "";
                            enteredNumberCount = 0;             //may break code
                            accountNumberList.Clear();          //may break code
                            CheckState();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect");
                }

            }
            else if (BankMenu == Menu.Main)
            {
                MessageBox.Show("Please select an option with the number pad!");
            }
            else if (BankMenu == Menu.ViewAll)
            {
                //Code to view all accounts? -----No longer needed------
            }
            else if (BankMenu == Menu.CreateAccount)
            {
                CheckState();
            }
            else if (BankMenu == Menu.Deposit)
            {
                ShowAccount.Deposit(int.Parse(lblAccountNumber.Text));
                lblAccountNumber.Visible = false;
                lblAccountNumber.Text = noValue;
                inMyAccount = false;
                BankMenu = Menu.Main;
                CheckState();
            }
            else if (BankMenu == Menu.Withdraw)
            {
                ShowAccount.Withdraw(int.Parse(lblAccountNumber.Text));
                lblAccountNumber.Visible = false;
                lblAccountNumber.Text = noValue;
                inMyAccount = false;
                BankMenu = Menu.Main;
                CheckState();
            }

        }
        private void btn1_Click(object sender, EventArgs e)
        {
            if (BankMenu == Menu.Main)
            {
                BankMenu = Menu.MyAccount;
            }

            if (BankMenu == Menu.DisplayAccount)
            {
                BankMenu = Menu.Deposit;
                inMyAccount = false;
            }
            enteredNumber = "1";
            PassButtonValueAndCheckState(enteredNumber);
            CheckState();
        }
        private void btn2_Click(object sender, EventArgs e)
        {
            if(BankMenu == Menu.Main)
            {
                BankMenu = Menu.CreateAccount;
            }
            if (BankMenu == Menu.DisplayAccount)
            {
                BankMenu = Menu.Withdraw;
                inMyAccount = false;
            }
            enteredNumber = "2";
            PassButtonValueAndCheckState(enteredNumber);
            CheckState();
        }
        private void btn3_Click(object sender, EventArgs e)
        {
            if (BankMenu == Menu.Main && accounts.Count == 0)       //Prevents the accounts view page from opening.
            {
                MessageBox.Show("There are no accounts, please add one.");
            }
            else
            {
                if (BankMenu == Menu.Main)
                {
                    BankMenu = Menu.ViewAll;
                    CheckState();
                }
                else if (BankMenu == Menu.ViewAll)
                {
                    //Do nothing
                }
                else
                {
                    enteredNumber = "3";
                    PassButtonValueAndCheckState(enteredNumber);
                    CheckState();
                }
            }
        }
        private void btn4_Click(object sender, EventArgs e)
        {
            if (BankMenu == Menu.ViewAll)
            { 
                CheckState();
            }
            else
            {
                enteredNumber = "4";
                PassButtonValueAndCheckState(enteredNumber);
            }
            
        }
        private void btn5_Click(object sender, EventArgs e)
        {
            
            enteredNumber = "5";
            PassButtonValueAndCheckState(enteredNumber);
        }
        private void btn6_Click(object sender, EventArgs e)
        {
            enteredNumber = "6";
            PassButtonValueAndCheckState(enteredNumber);
        }
        private void btn7_Click(object sender, EventArgs e)
        {
            enteredNumber = "7";
            PassButtonValueAndCheckState(enteredNumber);
        }
        private void btn8_Click(object sender, EventArgs e)
        {
            enteredNumber = "8";
            PassButtonValueAndCheckState(enteredNumber);
        }
        private void btn9_Click(object sender, EventArgs e)
        {
            enteredNumber = "9";
            PassButtonValueAndCheckState(enteredNumber);
        }
        private void btn0_Click(object sender, EventArgs e)
        {
            enteredNumber = "0";
            PassButtonValueAndCheckState(enteredNumber);
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Accidental Clicks :/
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        public void EnterAccountNumber(string enteredNumber)
        {

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void timer1_Tick(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(accounts.Count == 0)
            {
                MessageBox.Show("No Accounts to Save.");
            }
            else
            {
                StreamWriter savefile = new StreamWriter("accounts.csv", true);

                foreach (Account account in accounts)
                {
                    savefile.WriteLine($"{account.AccNum},{account.FirstName},{account.LastName},{account.Balance}");
                }
                savefile.Close();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            StreamReader readfile = new StreamReader("accounts.csv");
            
            for (int i = 0; !readfile.EndOfStream; i++)
            {
                List<string> accountList = new List<string>();
                string account = readfile.ReadLine();
                accountList.AddRange(account.Split(","));

                Account loadAccount = new Account();
                loadAccount.AccNum = int.Parse(accountList[0]);
                loadAccount.FirstName = accountList[1];
                loadAccount.LastName = accountList[2];
                loadAccount.Balance = int.Parse(accountList[3]);
                loadAccount.FullName = $"{loadAccount.FirstName} {loadAccount.LastName}";
                accounts.Add(loadAccount);
            }
            readfile.Close();
            MessageBox.Show("Account Load Successful");
        }
    }
}