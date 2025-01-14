using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace incrypt
{
    public partial class MessageForm : Form
    {
        public MessageForm()
        {
            InitializeComponent();
        }
        private bool entered = false;
        public bool Entered { get { return entered; } }
        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length >= 1) { Hide(); entered = true; }
        }
    }
}
