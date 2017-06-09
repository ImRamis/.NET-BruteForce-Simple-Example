#region

using System;
using System.Threading;
using System.Windows.Forms;

#endregion

namespace WindowsFormsApplication6
{
    public partial class BruteForce : Form
    {
        private const string Alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Small = "abcdefghijlmnopqrstuvwxyz";
        private readonly string _alphanumeric;
        private readonly int _bse;
        public readonly string Numeric = "0123456789";
        private int _cur;
        private int _pass;
        private string _pw;
        private bool _stop;

        public BruteForce()
        {
            InitializeComponent();
            _alphanumeric = Numeric + Alpha + Small;
            _bse = _alphanumeric.Length;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (((Button) sender).Text == @"BruteForce")
            {
                _stop = false;
                _pass = 0;
                new Thread(Attempt).Start();
                ((Button) sender).Text = @"Stop Brute Force";
            }
            else
            {
                _stop = true;
                ((Button) sender).Text = @"BruteForce";
            }
        }

        private void Attempt()
        {
            var pass = GenerateString();
            while (pass != textBox1.Text)
            {
                pass = GenerateString();
                Invoke((MethodInvoker) delegate
                {
                    label1.Text = @"Passwords Tested : " + ++_pass;
                    textBox2.Text = pass;
                    listBox1.Items.Add(pass);
                });
                if (!_stop) continue;
                _stop = false;
                Invoke((MethodInvoker) delegate { button1.Text = @"BruteForce"; });
                break;
            }
            _stop = false;
            Invoke((MethodInvoker) delegate { button1.Text = @"BruteForce"; });
            textBox2.Text = pass;
        }

        public string GenerateString()
        {
            _pw = string.Empty;
            _cur = _pass;
            do
            {
                _pw = _alphanumeric[_cur % _bse] + _pw;
                _cur /= _bse;
            } while (_cur != 0);
            return _pw;
        }
    }
}