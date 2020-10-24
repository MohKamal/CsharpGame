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
using System.Text.RegularExpressions;
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

        public string ProjectPath { get; set; }
        private string SpritePath { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NewGameObject_Load(object sender, EventArgs e)
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

        private void list_scene_SelectedIndexChanged(object sender, EventArgs e)
        {
            list_layers.Items.Clear();
            if(list_scene.SelectedItem != null)
            {

                ComboBoxItem scene = (list_scene.SelectedItem as ComboBoxItem);
                ComboBoxItem item0 = new ComboBoxItem() { Text = "ground" };
                list_layers.Items.Add(item0);
                if (scene != null)
                {
                    foreach (string s in File.ReadAllLines($@"{ProjectPath}\Empty\Scenes\{scene.Value}"))
                    {
                        
                        if (s.Contains("Layer") && s.EndsWith("{ get; set; }"))
                        {
                            int endIndex = s.IndexOf("{");
                            int startIndex = s.IndexOf("private");
                            int diff = endIndex - (startIndex + 14);
                            string layerName = s.Substring(startIndex + 14, diff);
                            ComboBoxItem item = new ComboBoxItem() { Text = layerName };
                            list_layers.Items.Add(item);
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Add();
        }

        public string GenerateSlug(string phrase)
        {
            string str = RemoveAccent(phrase).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        public string RemoveAccent(string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

        private void Add()
        {
            if (string.IsNullOrEmpty(txt_name.Text))
            {
                MessageBox.Show("Please provide a name for the game object", "Adding error");
            }
            else
            {
                if (!string.IsNullOrEmpty(txt_name.Text))
                {
                    if (System.CodeDom.Compiler.CodeGenerator.IsValidLanguageIndependentIdentifier(txt_name.Text))
                    {
                        ComboBoxItem scene = (list_scene.SelectedItem as ComboBoxItem);
                        ComboBoxItem layer = (list_layers.SelectedItem as ComboBoxItem);

                        if (scene != null)
                        {
                            if (layer != null)
                            {
                                string originalFileName = Path.GetFileNameWithoutExtension(SpritePath);
                                string originalSlug = GenerateSlug(originalFileName);
                                FileInfo fi = new FileInfo(SpritePath);
                                string extension = fi.Extension;
                                string newFileName = $"{originalSlug}{extension}";
                                if (!File.Exists($@"{ProjectPath}\Empty\Resources\{newFileName}"))
                                {
                                    File.Copy(SpritePath, $@"{ProjectPath}\Empty\Resources\{newFileName}");
                                    UpdateResx(originalSlug, newFileName);
                                }
                                System.Drawing.Image img = System.Drawing.Image.FromFile(SpritePath);
                                UpdateFile(scene.Value.ToString(), layer.Text, txt_name.Text, img.Size, originalSlug);
                                this.Close();
                            }
                            else
                                MessageBox.Show("Please select a layer", "Adding error");
                        }
                        else
                            MessageBox.Show("Please select a scene", "Adding error");
                    }
                    else
                        MessageBox.Show("Please, set a correct game object name, like a variable name :{ !!!!");
                }
                else
                    MessageBox.Show("Please fill the object name");
            }
        }

        private void UpdateResx(string spriteName, string spriteWithExtension)
        {
            string FilePath = $@"{ProjectPath}\Empty\Properties\Resources.resx";
            var text = new StringBuilder();
            foreach (string s in File.ReadAllLines(FilePath))
            {
                
                if (!s.Contains("</root>"))
                {
                    text.AppendLine(s);
                }
                else
                {
                    text.AppendLine("<data name=\"traffic - sign\" type=\"System.Resources.ResXFileRef, System.Windows.Forms\">");
                    text.AppendLine($@"<value>..\Resources\{spriteWithExtension};System.Drawing.Bitmap, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a</value>");
                    text.AppendLine("</data>");
                    text.AppendLine(s);
                }
            }

            using (var file = new StreamWriter(File.Create(FilePath)))
            {
                file.Write(text.ToString());
            }

            //Edit cs
            FilePath = $@"{ProjectPath}\Empty\Properties\Resources.Designer.cs";
            text = new StringBuilder();
            bool addVarPosition = false;
            int count = 0;
            foreach (string s in File.ReadAllLines(FilePath))
            {
                text.AppendLine(s);

                if (s.Contains("resourceCulture = value;"))
                    addVarPosition = true;

                if (addVarPosition)
                {
                    if (count < 2)
                        count++;
                    else
                    {
                        addVarPosition = false;
                        text.AppendLine($"{Environment.NewLine}\t\t/// <summary>");
                        text.AppendLine("\t\t///   Recherche une ressource localisée de type System.Drawing.Bitmap.");
                        text.AppendLine("\t\t/// </summary>");
                        text.AppendLine($"\t\tinternal static System.Drawing.Bitmap {spriteName}" + " {");
                        text.AppendLine("\t\t\tget {");
                        text.AppendLine($"\t\t\t\tobject obj = ResourceManager.GetObject(\"{spriteName}\", resourceCulture);");
                        text.AppendLine("\t\t\t\treturn ((System.Drawing.Bitmap)(obj));");
                        text.AppendLine("\t\t\t}");
                        text.AppendLine("\t\t}");
                    }
                }

            }

            using (var file = new StreamWriter(File.Create(FilePath)))
            {
                file.Write(text.ToString());
            }
        }

        private void UpdateFile(string className, string layerName, string objectName, Size imgSize, string spriteName)
        {
            string FilePath = $@"{ProjectPath}\Empty\Scenes\{className}";
            var text = new StringBuilder();
            bool addVarPosition = false;
            bool addLine = false;
            bool addInitPostition = false;
            bool addinitLine = false;
            foreach (string s in File.ReadAllLines(FilePath))
            {
                text.AppendLine(s);

                if (s.Contains($"public class {className.Replace(".cs", "")} : Scene"))
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
                    text.AppendLine($"\t//Creating Sprite Variable");
                    text.AppendLine($"\tprivate Sprite sprite_{objectName}" + " { get; set; }");
                    text.AppendLine($"\t//Creating GameObject Variable");
                    text.AppendLine($"\tprivate GameObject {objectName}" + " { get; set; }");
                    addVarPosition = false;
                    addLine = false;
                }

                if (s.Contains($"RegisterLayer({layerName.Replace(" ", "")})"))
                {
                    addInitPostition = true;
                }

                if (addInitPostition)
                {
                    text.AppendLine($"\t\t//Init the sprite_{objectName} Variable");
                    text.AppendLine($"\t\tsprite_{objectName} = new Sprite({imgSize.Width}, {imgSize.Height});"); 
                    text.AppendLine($"\t\tsprite_{objectName}.LoadFromFile(Empty.Properties.Resources.{spriteName});");
                    text.AppendLine($"\t\t{objectName} = new GameObject(new PointF({(int)txt_x.Value}, {(int)txt_y.Value}), sprite_{objectName});");
                    text.AppendLine("\t\t//Register gameobject to the layer");
                    text.AppendLine($"\t\t{layerName.Replace(" ", "")}.RegisterGameObject({objectName});");
                    addInitPostition = false;
                    addinitLine = false;
                }
            }

            using (var file = new StreamWriter(File.Create(FilePath)))
            {
                file.Write(text.ToString());
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
