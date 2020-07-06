using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpGame.PingPong
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public PingPong PingPong { get; private set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            PingPong = new PingPong(pictureBox1);
            PingPong.Start();
        }
    }
}
