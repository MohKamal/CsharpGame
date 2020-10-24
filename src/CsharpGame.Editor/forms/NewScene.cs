using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
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

        public string ProjectPath { get; set; }

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
            if (!string.IsNullOrEmpty(txt_name.Text) && !string.IsNullOrEmpty(txt_class_name.Text))
            {
                if (System.CodeDom.Compiler.CodeGenerator.IsValidLanguageIndependentIdentifier(txt_class_name.Text))
                {

                    string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\models\Scene.txt");
                    string text = File.ReadAllText(path);
                    text = text.Replace("<SceneClassName>", txt_class_name.Text);
                    text = text.Replace("<SceneName>", txt_name.Text);
                    string savePath = $@"{ProjectPath}\Empty\Scenes";
                    if (!Directory.Exists(savePath))
                        Directory.CreateDirectory(savePath);

                    savePath += $@"\{txt_class_name.Text}.cs";
                    File.WriteAllText(savePath, text);

                    UpdateFile(txt_class_name.Text, txt_name.Text);

                    this.Close();
                }
                else {
                    MessageBox.Show("Please, set a correct class name :{ !!!!");
                }
            }else
                MessageBox.Show("Please, fill the fiedls!");
        }

        private void UpdateFile(string className, string sceneName)
        {
            string FilePath = $@"{ProjectPath}\Empty\Game.cs";

            var text = new StringBuilder();

            bool usingExist = false;
            text.AppendLine("using Empty.Scenes;");

            foreach (string s in File.ReadAllLines(FilePath))
            {
                if(s == "using Empty.Scenes;")
                {
                    usingExist = true;
                    break;
                }
            }

            if (!usingExist)
                text.AppendLine("using Empty.Scenes;");

            bool addVarPosition = false;
            bool addLine = false;

            bool addInitPostition = false;
            bool addinitLine = false;

            foreach (string s in File.ReadAllLines(FilePath))
            {
                text.AppendLine(s);

                if (s.Contains("public class Game : Engine"))
                    addVarPosition = true;

                if (addVarPosition)
                {
                    if(s.Contains("{"))
                    {
                        addLine = true;
                    }
                }

                if (addLine)
                {
                    text.AppendLine($"\t//Creating {className} Variable{Environment.NewLine}\tprivate {className} {className.ToLower()}" + " { get; set; }" );
                    addVarPosition = false;
                    addLine = false;
                }

                if(s.Contains("public override bool OnCreate()"))
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
                    text.AppendLine($"\t\t//Init the {className} Variable{Environment.NewLine}\t\t{className.ToLower()} = new {className}(this);");
                    text.AppendLine($"\t\t//Register the {className} Variable to the engine{Environment.NewLine}\t\tRegisterScene({className.ToLower()});");
                    text.AppendLine($"\t\t//Set this scene as the first scene");
                    text.AppendLine($"\t\t//GoToScene({className.ToLower()};");
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
            if(e.KeyCode == Keys.Enter)
                Add();
        }

        private void NewScene_Load(object sender, EventArgs e)
        {

        }
    }
}
