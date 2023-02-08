namespace ITEC145_Section_E
{
    public partial class Form1 : Form
    {
        int labelLocationX = 120;               //Sets a starting X and Y location for my labels
        int labelLocationY = 173;

        string main = "Make a Choice:";         //main menu header text
        string myAccount = "Enter Your 8 Digit Account Number:";    //Header Text Variables
        string createAccount = "Enter Your Details Below";

        string enteredNumber;               //string to store the currently entered number.
        string finalNumber;                 //string to store the final account number (retrieved from accountNumberList list)
        string noValue = "";                //Default value, if account number is cleared or cancelled.

        int enteredNumberCount;             //Count of entered numbers.

        bool inMyAccount = false;           //Flags for if a state is ready for changes to the screen
        bool inCreateAccount = false;
        bool inViewAllAccounts = false;
        List<string> accountNumberList = new List<string>();   
        List<Label> labels = new List<Label>();     //List to keep track of my created labels to easily delete them later.

        enum Menu
        {
            Main,               //States of my teller machine for if statements (CheckState() method)
            MyAccount,
            CreateAccount,
            ViewAll,
            NoState
        }
        Menu BankMenu = Menu.Main;  //Default state on startup

        public Form1()
        {
            InitializeComponent();
            finalNumber = lblAccountNumber.Text;
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
            else if(BankMenu == Menu.CreateAccount)
            {
                lblHeader.Text = createAccount;
                MakeLabel("Enter Your First Name:", 55);
                //Code to create a text box
                MakeLabel("_", 75);
                MakeLabel("Enter Your Surname:", 95);
                //Code to create a text box
            }
            else if (BankMenu == Menu.NoState)
            {
              
            }
        }

        public void ClearScreen()                           //Removes all labels that have been added to list
        {
            foreach (Label label in labels)
            {
                Controls.Remove(label);
            }
            labels.Clear();
        }
        public void CheckButtonState(string enteredNumber)              //Used to pass through a buttons number and to also check the state of the menu
        {

            if (BankMenu == Menu.Main)
            {
                
            }
            else if(BankMenu == Menu.MyAccount)
            {
                if (enteredNumberCount == 8)        //Ends once every string in list has been changed.
                {
                    //Do nothing
                }
                else
                {
                    if(inMyAccount == false)
                    {
                        ClearScreen();
                        CheckState();
                        inMyAccount = true;
                    }
                    else
                    {
                        accountNumberList.Add(enteredNumber);
                        string result = String.Join(" ", accountNumberList);    //Learnt about String.Join({char}, array) this from stack overflow. 
                        lblAccountNumber.Text = result;
                        enteredNumberCount++;
                    }
                } 
            }
            else if(BankMenu == Menu.CreateAccount)
            {
                if(inCreateAccount == false)
                {
                    ClearScreen();
                    CheckState();
                    inCreateAccount = true;
                }
                else
                {
                    //int textBoxX;
                    //int textBoxY;

                    //foreach(Label label in labels)
                    //{
                    //    if(label.Text.Contains("_"))
                    //    {
                    //        textBoxX = label.Left;
                    //        textBoxY = label.Top;                         In Progress
                    //        this.Controls.Remove(label);
                    //        //Create Textbox
                    //        MakeLabel("_", 115);
                    //    }
                    //}
                }
                
            }
            else if(BankMenu == Menu.ViewAll)
            {

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
        }
        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (BankMenu == Menu.MyAccount)
            {
                //Code to find account and show details

            }
            else if (BankMenu == Menu.Main)
            {
                MessageBox.Show("Please select an option with the number pad!");
            }
            else if (BankMenu == Menu.ViewAll)
            {
                //Code to view all accounts
            }
            else if (BankMenu == Menu.CreateAccount)
            {
                //Code to create accounts
            }
        }
        private void btn1_Click(object sender, EventArgs e)
        {
            if (BankMenu == Menu.Main)
            {
                BankMenu = Menu.MyAccount;
            }
            enteredNumber = "1";
            CheckButtonState(enteredNumber);
        }
        private void btn2_Click(object sender, EventArgs e)
        {
            if(BankMenu == Menu.Main)
            {
                BankMenu = Menu.CreateAccount;
            }
            enteredNumber = "2";
            CheckButtonState(enteredNumber);
        }
        private void btn3_Click(object sender, EventArgs e)
        {
            if (BankMenu == Menu.Main)
            {
                BankMenu = Menu.ViewAll;
            }
            enteredNumber = "3";
            CheckButtonState(enteredNumber);
        }
        private void btn4_Click(object sender, EventArgs e)
        {
            enteredNumber = "4";
            CheckButtonState(enteredNumber);
        }
        private void btn5_Click(object sender, EventArgs e)
        {
            
            enteredNumber = "5";
            CheckButtonState(enteredNumber);
        }
        private void btn6_Click(object sender, EventArgs e)
        {
            enteredNumber = "6";
            CheckButtonState(enteredNumber);
        }
        private void btn7_Click(object sender, EventArgs e)
        {
            enteredNumber = "7";
            CheckButtonState(enteredNumber);
        }
        private void btn8_Click(object sender, EventArgs e)
        {
            enteredNumber = "8";
            CheckButtonState(enteredNumber);
        }
        private void btn9_Click(object sender, EventArgs e)
        {
            enteredNumber = "9";
            CheckButtonState(enteredNumber);
        }
        private void btn0_Click(object sender, EventArgs e)
        {
            enteredNumber = "0";
            CheckButtonState(enteredNumber);
        }
        


        //Accidental Clicks :/

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
    }
}