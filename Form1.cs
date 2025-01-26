using System.IO.Ports;
using System.Text.RegularExpressions;
namespace AT_COMMEND
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static string[] info = new string[30];
        static AT at = new AT();
        static string recivedinfo = "";
        static VID v;
        private void Form1_Load(object sender, EventArgs e)
        {
            v = new VID();
            button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            at.sendCommend("AT+DEVCONINFO");
            recivedinfo = at.getresult();
            textBox1.Text = recivedinfo;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            JSON js = new JSON(recivedinfo);
            Dictionary<string, string> file = js.Insert();
            textBox1.Text = file["IMEI"];
            js.saveJsonFile("data.json");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = v.getvid();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = v.getpid();
        }
    }
}
