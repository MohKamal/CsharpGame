﻿using CsharpGame.Editor.forms;
using CsharpGame.Engine.Base;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using CsharpGame.Editor.lib;

namespace CsharpGame.Editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string ProjectPath { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            disableAdding();
            InitTheInterface();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            CompileGame();
        }

        private void NewProject(string DestinationPath)
        {
            string startupPath = System.IO.Directory.GetCurrentDirectory();
            string SourcePath = $"{startupPath}/../../../_Empty";
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(SourcePath, "*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(SourcePath, DestinationPath));

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(SourcePath, "*.*",
                SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(SourcePath, DestinationPath), true);

            InitTheInterface();
        }

        private bool CompileGame()
        {
            string projectFile = $@"{ProjectPath}\Empty.sln -flp:logfile=MyProjectOutput.log;verbosity=diagnostic";
            string projectExe = $@"{ProjectPath}\Empty\bin\Debug\Empty.exe";
            string strCmdText = $@"C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe {projectFile} -t:go -fl -flp:logfile=MyProjectOutput.log;verbosity=diagnostic";

            try
            {
                File.Delete(projectExe);
            }
            catch
            {

            }

            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@"C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe");
            p.StartInfo.Arguments = string.Format(projectFile);
            p.Start();
            p.WaitForExit();
            try
            {
                Process.Start(projectExe);

            }
            catch
            {
                MessageBox.Show("An error is occuperd, please check your code!");
                return false;
            }
            return true;
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            KillGame();
        }

        private void KillGame()
        {
            Process[] proccess = Process.GetProcessesByName("Game");
            foreach (var process in proccess)
            {
                process.Kill();
            }
        }

        private void InitTheInterface()
        {
            InitContent();
        }

        private bool InitContent()
        {
            contentList.Items.Clear();
            if (string.IsNullOrEmpty(ProjectPath))
                return false;
            string path = ProjectPath;
            List<string> forbidenFiles = new List<string> { "Form1.Designer", "Program", "AssemblyInfo", "Resources.Designer", "Settings.Designer" };
            string[] files = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                var match = forbidenFiles.FirstOrDefault(stringToCheck => stringToCheck.Contains(Path.GetFileNameWithoutExtension(file)));

                if (match == null)
                {
                    ListViewItem item = new ListViewItem() { Text = Path.GetFileNameWithoutExtension(file), Tag = file };
                    contentList.Items.Add(item);
                }
            }
            return true;
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
            //newScene.Engine = Engine;
            //newScene.ShowDialog();
            //RefreshScenesList();
        }

        private void btn_add_layer_Click(object sender, EventArgs e)
        {
            NewLayer newLayer = new NewLayer();
            //newLayer.Engine = Engine;
            //newLayer.ShowDialog();
            //RefreshScenesList();
        }

        private void btn_add_gameobject_Click(object sender, EventArgs e)
        {
            NewGameObject newGameObject = new NewGameObject();
            //newGameObject.Engine = Engine;
            //newGameObject.ShowDialog();
            //RefreshScenesList();
        }

        private void nouveauToolStripButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ProjectPath = System.IO.Path.GetDirectoryName(saveFileDialog1.FileName);
                NewProject(ProjectPath);
                enableAdding();
            }
        }

        private void ouvrirToolStripButton_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    ProjectPath =  fbd.SelectedPath;
                    InitTheInterface();
                    enableAdding();
                }
            }
        }

        private void contentList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(contentList.SelectedItems.Count > 0)
            {
                ListViewItem item = contentList.SelectedItems[0];
                bool found = false;
                TabPage tp = null;
                foreach(TabPage p in code_edit_panel.Controls)
                {
                    if(p.Tag == item.Tag)
                    {
                        found = true;
                        tp = p;
                        break;
                    }
                }

                if (!found)
                {
                    string text = File.ReadAllText(item.Tag.ToString());
                    tp = new TabPage(item.Text);
                    tp.Tag = item.Tag;
                    tp.Leave += tabPage_Leave;
                    tp.ContextMenuStrip = tabsMenu;
                    code_edit_panel.TabPages.Add(tp);

                    SyntaxHighlighter.SyntaxRichTextBox m_syntaxRichTextBox = new SyntaxHighlighter.SyntaxRichTextBox();
                    m_syntaxRichTextBox.Dock = DockStyle.Fill;
                    m_syntaxRichTextBox.BorderStyle = BorderStyle.None;
                    m_syntaxRichTextBox.Margin = new Padding(0);
                    m_syntaxRichTextBox.AcceptsTab = true;
                    m_syntaxRichTextBox.Font = new Font(new FontFamily("Consolas"), 11);

                    // Add the keywords to the list.
                    List<string> keywords = new List<string>() { "public", "class", "void", "bool", "namespace", "using", "return", "base", "string", "int", "float", "double",
                    "char", "true", "false", "new", "private", "protected", "static", "override", "readonly", "System" };
                    m_syntaxRichTextBox.Settings.Keywords.AddRange(keywords);

                    // Set the comment identifier. 
                    // For Lua this is two minus-signs after each other (--).
                    // For C++ code we would set this property to "//".
                    m_syntaxRichTextBox.Settings.Comment = "//";

                    // Set the colors that will be used.
                    m_syntaxRichTextBox.Settings.KeywordColor = Color.Blue;
                    m_syntaxRichTextBox.Settings.CommentColor = Color.Green;
                    m_syntaxRichTextBox.Settings.StringColor = Color.Brown;
                    m_syntaxRichTextBox.Settings.IntegerColor = Color.BlueViolet;

                    // Let's not process strings and integers.
                    m_syntaxRichTextBox.Settings.EnableStrings = true;
                    m_syntaxRichTextBox.Settings.EnableIntegers = true;

                    // Let's make the settings we just set valid by compiling
                    // the keywords to a regular expression.
                    m_syntaxRichTextBox.CompileKeywords();

                    // Load a file and update the syntax highlighting.
                    m_syntaxRichTextBox.LoadFile(item.Tag.ToString(), RichTextBoxStreamType.PlainText);
                    m_syntaxRichTextBox.ProcessAllLines();

                    //RichTextBox tb = new RichTextBox();
                    //tb.Dock = DockStyle.Fill;
                    //tb.BorderStyle = BorderStyle.None;
                    //tb.Margin = new Padding(0);
                    //tb.Text = text;
                    tp.Controls.Add(m_syntaxRichTextBox);
                }

                if(tp != null)
                    code_edit_panel.SelectedTab = tp;
            }
        }

        private void tabPage_Leave(object sender, EventArgs e)
        {
            TabPage tabPage = (TabPage)sender;
            if(tabPage != null)
            {
                string path = tabPage.Tag.ToString();
                if (!string.IsNullOrEmpty(path))
                {
                    RichTextBox rich = tabPage.Controls.OfType<RichTextBox>().FirstOrDefault();
                    if (rich != null)
                    {
                        rich.SaveFile(path, RichTextBoxStreamType.PlainText);
                    }
                }
            }
        }

        private void saveTabsMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)sender;
            TabPage tabPage = (TabPage)toolStripMenuItem.Owner.Parent;
            if (tabPage != null)
            {
                string path = tabPage.Tag.ToString();
                if (!string.IsNullOrEmpty(path))
                {
                    RichTextBox rich = tabPage.Controls.OfType<RichTextBox>().FirstOrDefault();
                    if (rich != null)
                    {
                        rich.SaveFile(path, RichTextBoxStreamType.PlainText);
                    }
                }
            }
        }

        private void closeTabsMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)sender;
            TabPage tabPage = (TabPage)toolStripMenuItem.Owner.Parent;
            if (tabPage != null)
            {
                tabPage.Parent.Controls.Remove(tabPage);
            }
        }
    }
}
