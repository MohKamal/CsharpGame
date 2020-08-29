using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsharpGame.Editor.forms
{
    public partial class NewScene : Form
    {
        public NewScene()
        {
            InitializeComponent();
        }

        public CsharpGame.Engine.Base.Engine Engine { get; set; }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void Add()
        {
            if (Engine != null)
            {
                Engine.RegisterScene(new CsharpGame.Engine.Base.Scene(txt_name.Text, Engine));
                this.Close();
            }
        }

        private void txt_name_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                Add();
        }
    }
}
