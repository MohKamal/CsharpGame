namespace CsharpGame.Editor.forms
{
    partial class NewGameObject
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.list_scene = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.list_layers = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_x = new System.Windows.Forms.NumericUpDown();
            this.txt_y = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_upload = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txt_x)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_y)).BeginInit();
            this.SuspendLayout();
            // 
            // list_scene
            // 
            this.list_scene.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.list_scene.FormattingEnabled = true;
            this.list_scene.Location = new System.Drawing.Point(94, 12);
            this.list_scene.Name = "list_scene";
            this.list_scene.Size = new System.Drawing.Size(134, 21);
            this.list_scene.TabIndex = 15;
            this.list_scene.SelectedIndexChanged += new System.EventHandler(this.list_scene_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Scene name :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Layer name :";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(73, 144);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(154, 144);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 18;
            this.button2.Text = "Add";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txt_name
            // 
            this.txt_name.Location = new System.Drawing.Point(94, 65);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(135, 20);
            this.txt_name.TabIndex = 17;
            this.txt_name.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_name_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Object name :";
            // 
            // list_layers
            // 
            this.list_layers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.list_layers.FormattingEnabled = true;
            this.list_layers.Location = new System.Drawing.Point(95, 39);
            this.list_layers.Name = "list_layers";
            this.list_layers.Size = new System.Drawing.Size(134, 21);
            this.list_layers.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Position :";
            // 
            // txt_x
            // 
            this.txt_x.Location = new System.Drawing.Point(116, 89);
            this.txt_x.Maximum = new decimal(new int[] {
            -559939585,
            902409669,
            54,
            0});
            this.txt_x.Name = "txt_x";
            this.txt_x.Size = new System.Drawing.Size(42, 20);
            this.txt_x.TabIndex = 23;
            this.txt_x.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_name_KeyDown);
            // 
            // txt_y
            // 
            this.txt_y.Location = new System.Drawing.Point(184, 89);
            this.txt_y.Maximum = new decimal(new int[] {
            -559939585,
            902409669,
            54,
            0});
            this.txt_y.Name = "txt_y";
            this.txt_y.Size = new System.Drawing.Size(45, 20);
            this.txt_y.TabIndex = 24;
            this.txt_y.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_name_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(96, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "X";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(164, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Y";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Sprite :";
            // 
            // btn_upload
            // 
            this.btn_upload.Location = new System.Drawing.Point(94, 115);
            this.btn_upload.Name = "btn_upload";
            this.btn_upload.Size = new System.Drawing.Size(135, 23);
            this.btn_upload.TabIndex = 28;
            this.btn_upload.Text = "Upload sprite";
            this.btn_upload.UseVisualStyleBackColor = true;
            this.btn_upload.Click += new System.EventHandler(this.btn_upload_Click);
            // 
            // NewGameObject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 175);
            this.Controls.Add(this.btn_upload);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_y);
            this.Controls.Add(this.txt_x);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.list_layers);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txt_name);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.list_scene);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "NewGameObject";
            this.Text = "Add game object";
            this.Load += new System.EventHandler(this.NewGameObject_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txt_x)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_y)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox list_scene;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox list_layers;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown txt_x;
        private System.Windows.Forms.NumericUpDown txt_y;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_upload;
    }
}