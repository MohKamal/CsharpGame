using CsharpGame.Editor.lib;
using CsharpGame.Engine.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        public string ProjectPath { get; set; }

        private void NewLayer_Load(object sender, EventArgs e)
        {
            list_scene.Items.Clear();
            if (Directory.Exists($@"{ProjectPath}\Empty\Scenes"))
                Directory.CreateDirectory($@"{ProjectPath}\Empty\Scenes");

            DirectoryInfo d = new DirectoryInfo($@"{ProjectPath}\Empty\Scenes");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.cs"); //Getting Text files
            foreach (FileInfo file in Files)
            {
                ComboBoxItem item = new ComboBoxItem() { Text = Path.GetFileNameWithoutExtension(file.Name), Value = file.Name };
                list_scene.Items.Add(item);
            }
            list_scene.SelectedIndex = 0;
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
                MessageBox.Show("Please, give a name to the layer", "Adding error");
            else
            {
                if (System.CodeDom.Compiler.CodeGenerator.IsValidLanguageIndependentIdentifier(txt_name.Text))
                {
                    if (list_scene.SelectedItem != null)
                    {
                        ComboBoxItem item = (list_scene.SelectedItem as ComboBoxItem);
                        UpdateFile(item.Text, txt_name.Text);
                        this.Close();
                    }
                    else
                        MessageBox.Show("Please, select a scene class", "Adding error");
                }else
                    MessageBox.Show("Please, set a correct layer name, like a class name :{ !!!!");
            }
        }

        private void UpdateFile(string className, string layerName)
        {
            string FilePath = $@"{ProjectPath}\Empty\Scenes\{className}.cs";

            var text = new StringBuilder();

            bool addVarPosition = false;
            bool addLine = false;

            bool addInitPostition = false;
            bool addinitLine = false;

            foreach (string s in File.ReadAllLines(FilePath))
            {
                text.AppendLine(s);

                if (s.Contains($"public class {className} : Scene"))
                    addVarPosition = true;

                if (addVarPosition)
                {
                    if (s.Contains("{"))
                    {
                        addLine = true;
                    }
                }

                if (addLine)
                {
                    text.AppendLine($"{Environment.NewLine}\t//Creating Layer Variable{Environment.NewLine}\tprivate Layer {layerName}" + " { get; set; }");
                    addVarPosition = false;
                    addLine = false;
                }

                if (s.Contains("public override bool OnCreate()"))
                {
                    addInitPostition = true;
                }

                if (addInitPostition)
                {
                    if (s.Contains("{"))
                        addinitLine = true;
                }

                if (addinitLine)
                {
                    text.AppendLine($"\t\t//Init the {layerName} Variable{Environment.NewLine}\t\t{layerName} = new Layer(\"{layerName}\");");
                    text.AppendLine($"\t\t//Register the Layer Variable to the scene{Environment.NewLine}\t\tRegisterLayer({layerName});");
                    text.AppendLine("\t\t//Register gameobject to the layer");
                    addInitPostition = false;
                    addinitLine = false;
                }
            }

            using (var file = new StreamWriter(File.Create(FilePath)))
            {
                file.Write(text.ToString());
            }
        }

        private void txt_name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Add();
        }
    }
}
