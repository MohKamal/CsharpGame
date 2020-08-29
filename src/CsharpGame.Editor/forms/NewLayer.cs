using CsharpGame.Editor.lib;
using CsharpGame.Engine.Base;
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
    public partial class NewLayer : Form
    {
        public NewLayer()
        {
            InitializeComponent();
        }

        public Engine.Base.Engine Engine { get; set; }

        private void NewLayer_Load(object sender, EventArgs e)
        {
            if(Engine == null)
            {
                MessageBox.Show("No Engine was found", "Engine error");
                this.Close();
            }

            if(Engine.Scenes.Count <= 0)
            {
                MessageBox.Show("No scene was found, please add a scene at least", "Engine error");
                this.Close();
            }

            foreach(KeyValuePair<int, Scene> scene in Engine.Scenes)
            {
                ComboBoxItem item = new ComboBoxItem() { Text = scene.Value.Name, Value = scene.Key };
                list_scene.Items.Add(item);
            }
        }

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
            if (string.IsNullOrEmpty(txt_name.Text))
            {
                MessageBox.Show("Please, give a name to the layer", "Adding error");
            }
            else
            {
                Scene scene = Engine.Scenes[(int)(list_scene.SelectedItem as ComboBoxItem).Value];
                if (scene != null)
                {
                    Layer layer = new Layer(txt_name.Text);
                    scene.RegisterLayer(layer);
                    this.Close();
                }
            }
        }

        private void txt_name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Add();
        }
    }
}
