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
    public partial class NewGameObject : Form
    {
        public NewGameObject()
        {
            InitializeComponent();
        }

        public Engine.Base.Engine Engine { get; set; }

        private string SpritePath { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NewGameObject_Load(object sender, EventArgs e)
        {
            if (Engine == null)
            {
                MessageBox.Show("No Engine was found", "Engine error");
                this.Close();
            }

            if (Engine.Scenes.Count <= 0)
            {
                MessageBox.Show("No scene was found, please add a scene at least", "Engine error");
                this.Close();
            }

            foreach (KeyValuePair<int, Scene> scene in Engine.Scenes)
            {
                ComboBoxItem item = new ComboBoxItem() { Text = scene.Value.Name, Value = scene.Key };
                list_scene.Items.Add(item);
            }
        }

        private void list_scene_SelectedIndexChanged(object sender, EventArgs e)
        {
            list_layers.Items.Clear();
            if(list_scene.SelectedItem != null)
            {
                Scene scene = Engine.Scenes[(int)(list_scene.SelectedItem as ComboBoxItem).Value];
                if(scene != null)
                {
                    foreach (KeyValuePair<int, Layer> layer in scene.Layers)
                    {
                        ComboBoxItem item = new ComboBoxItem() { Text = layer.Value.Name, Value = layer.Key };
                        list_layers.Items.Add(item);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void Add()
        {
            if (string.IsNullOrEmpty(txt_name.Text))
            {
                MessageBox.Show("Please provide a name for the game object", "Adding error");
            }
            else
            {
                Scene scene = Engine.Scenes[(int)(list_scene.SelectedItem as ComboBoxItem).Value];
                if (scene != null)
                {
                    Layer layer = scene.Layers[(int)(list_layers.SelectedItem as ComboBoxItem).Value];
                    if (layer != null)
                    {
                        if (!string.IsNullOrEmpty(SpritePath))
                        {
                            Sprite sprite = new Sprite(100, 100);
                            sprite.LoadFromFile(SpritePath);
                            GameObject gameObject = new GameObject(new PointF((float)txt_x.Value, (float)txt_y.Value), sprite) { Name = txt_name.Text };
                            layer.RegisterGameObject(gameObject);
                            this.Close();
                        }
                    }
                }
            }
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Images | *.jpg;*.png;*.jpeg;*.bmp"; // file types, that will be allowed to upload
            dialog.Multiselect = false; // allow/deny user to upload more than one file at a time
            if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
            {
                SpritePath = dialog.FileName; // get name of file
            }
        }

        private void txt_name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Add();
        }
    }
}
