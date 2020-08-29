using CsharpGame.Editor.forms;
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

namespace CsharpGame.Editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        CsharpGame.Engine.Base.Engine Engine { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            Engine = new Engine.Base.Engine(drawingArea);
            InitTheInterface();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            Engine.Start();
            disableAdding();
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            Engine.Stop();
            enableAdding();
        }

        private void InitTheInterface()
        {
            RefreshScenesList();
        }

        private void list_scenes_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            list_layers.Items.Clear();
            if (list_scenes.SelectedItems.Count > 0)
            {
                Scene scene = Engine.Scenes[(int)(list_scenes.SelectedItems[0] as ListViewItem).Tag];
                if (scene != null)
                {
                    foreach (KeyValuePair<int, Layer> layer in scene.Layers)
                    {
                        list_layers.Items.Add(new ListViewItem { Text = $"{layer.Value.Name} [{layer.Value.z_order}]", Tag = layer.Key });
                    }
                }
            }
        }

        public void RefreshScenesList()
        {
            list_scenes.Items.Clear();
            list_layers.Items.Clear();
            list_gameobjects.Items.Clear();
            foreach (KeyValuePair<int, Scene> scene in Engine.Scenes)
            {
                list_scenes.Items.Add(new ListViewItem { Text = scene.Value.Name, Tag = scene.Key });
            }
        }

        private void list_gameobjects_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
        }

        private void enableAdding()
        {
            btn_adding.Enabled = true;
        }

        private void disableAdding()
        {
            btn_adding.Enabled = false;
        }

        private void btn_add_scene_Click(object sender, EventArgs e)
        {
            NewScene newScene = new NewScene();
            newScene.Engine = Engine;
            newScene.ShowDialog();
            RefreshScenesList();
        }

        private void btn_add_layer_Click(object sender, EventArgs e)
        {
            NewLayer newLayer = new NewLayer();
            newLayer.Engine = Engine;
            newLayer.ShowDialog();
            RefreshScenesList();
        }

        private void btn_add_gameobject_Click(object sender, EventArgs e)
        {
            NewGameObject newGameObject = new NewGameObject();
            newGameObject.Engine = Engine;
            newGameObject.ShowDialog();
            RefreshScenesList();
        }

        private void list_layers_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            list_gameobjects.Items.Clear();
            if (list_layers.SelectedItems.Count > 0)
            {
                Scene scene = Engine.Scenes[(int)(list_scenes.SelectedItems[0] as ListViewItem).Tag];
                Layer layer = scene.Layers[(int)(list_layers.SelectedItems[0] as ListViewItem).Tag];
                if (scene != null)
                {
                    foreach (GameObject @object in layer.RegistredObjects())
                    {
                        list_gameobjects.Items.Add(new ListViewItem { Text = $"{@object.Name} [{@object.Position.X}, {@object.Position.Y}]", Tag = @object.Name });
                    }
                }
            }
        }
    }
}
