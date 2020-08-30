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
    public partial class LogViewer : Form
    {
        public LogViewer()
        {
            InitializeComponent();
        }

        private void LogViewer_Load(object sender, EventArgs e)
        {
            string startupPath = System.IO.Directory.GetCurrentDirectory() + @"\MyProjectOutput.log";
            RichTextBox richTextBox = new RichTextBox();
            richTextBox.Dock = DockStyle.Fill;
            richTextBox.BorderStyle = BorderStyle.None;
            richTextBox.Margin = new Padding(0);
            richTextBox.AcceptsTab = true;
            richTextBox.Font = new Font(new FontFamily("Consolas"), 11);
            richTextBox.LoadFile(startupPath, RichTextBoxStreamType.PlainText);

            //SyntaxHighlighter.SyntaxRichTextBox m_syntaxRichTextBox = new SyntaxHighlighter.SyntaxRichTextBox();
            //m_syntaxRichTextBox.Dock = DockStyle.Fill;
            //m_syntaxRichTextBox.BorderStyle = BorderStyle.None;
            //m_syntaxRichTextBox.Margin = new Padding(0);
            //m_syntaxRichTextBox.AcceptsTab = true;
            //m_syntaxRichTextBox.Font = new Font(new FontFamily("Consolas"), 11);

            //// Add the keywords to the list.
            //List<string> keywords = new List<string>() { "public", "class", "void", "bool", "namespace", "using", "return", "base", "string", "int", "float", "double",
            //        "char", "true", "false", "new", "private", "protected", "static", "override", "readonly", "System" };
            //m_syntaxRichTextBox.Settings.Keywords.AddRange(keywords);

            //// Set the comment identifier. 
            //// For Lua this is two minus-signs after each other (--).
            //// For C++ code we would set this property to "//".
            //m_syntaxRichTextBox.Settings.Comment = "//";

            //// Set the colors that will be used.
            //m_syntaxRichTextBox.Settings.KeywordColor = Color.Blue;
            //m_syntaxRichTextBox.Settings.CommentColor = Color.Green;
            //m_syntaxRichTextBox.Settings.StringColor = Color.Brown;
            //m_syntaxRichTextBox.Settings.IntegerColor = Color.BlueViolet;

            //// Let's not process strings and integers.
            //m_syntaxRichTextBox.Settings.EnableStrings = false;
            //m_syntaxRichTextBox.Settings.EnableIntegers = false;

            //// Let's make the settings we just set valid by compiling
            //// the keywords to a regular expression.
            //m_syntaxRichTextBox.CompileKeywords();

            //// Load a file and update the syntax highlighting.
            //m_syntaxRichTextBox.LoadFile(startupPath, RichTextBoxStreamType.PlainText);
            //m_syntaxRichTextBox.ProcessAllLines();

            this.Controls.Add(richTextBox);
        }
    }
}
