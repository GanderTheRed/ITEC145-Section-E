namespace ITEC145_Section_E
{
    public partial class Form1 : Form
    {
        int labelLocationX = 120;
        int labelLocationY = 173;

        public void MakeLabel(string message, int locationy)
        {
            Font font = new Font("Segoe UI", 10);
            Label lbl = new Label();
            lbl.Top = labelLocationX + locationy;
            lbl.Left = labelLocationX;
            lbl.AutoSize = true;
            lbl.Text = message;
            lbl.ForeColor = Color.Chartreuse;
            lbl.BackColor = Color.Black;
            lbl.Font = font;
            Controls.Add(lbl);
            lbl.BringToFront();
        }
           

        public Form1()
        {
            InitializeComponent();
            MakeLabel("1. View My Account", 50);
            MakeLabel("2. -REDACTED-", 70);
            MakeLabel("3. -REDACTED-", 90);
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

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}