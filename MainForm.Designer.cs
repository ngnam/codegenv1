namespace CodeGeneratorV1
{
    partial class MainForm
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
            this.m_Menu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToDB = new System.Windows.Forms.ToolStripMenuItem();
            this.genCodeTrigger = new System.Windows.Forms.ToolStripMenuItem();
            this.genCodeStored = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToFile = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabTrigger = new System.Windows.Forms.TabPage();
            this.tabStored = new System.Windows.Forms.TabPage();
            this.clbTables = new System.Windows.Forms.CheckedListBox();
            this.rtxTrigger = new System.Windows.Forms.RichTextBox();
            this.rtxStored = new System.Windows.Forms.RichTextBox();
            this.cbSelectAll = new System.Windows.Forms.CheckBox();
            this.m_Menu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabTrigger.SuspendLayout();
            this.tabStored.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_Menu
            // 
            this.m_Menu.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.m_Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.connectToDB,
            this.genCodeTrigger,
            this.genCodeStored,
            this.saveToFile});
            this.m_Menu.Location = new System.Drawing.Point(0, 0);
            this.m_Menu.Name = "m_Menu";
            this.m_Menu.Size = new System.Drawing.Size(959, 24);
            this.m_Menu.TabIndex = 0;
            this.m_Menu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openProfileToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openProfileToolStripMenuItem
            // 
            this.openProfileToolStripMenuItem.Name = "openProfileToolStripMenuItem";
            this.openProfileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openProfileToolStripMenuItem.Text = "Open Profile";
            this.openProfileToolStripMenuItem.Click += new System.EventHandler(this.openProfileToolStripMenuItem_Click);
            // 
            // connectToDB
            // 
            this.connectToDB.Name = "connectToDB";
            this.connectToDB.Size = new System.Drawing.Size(130, 20);
            this.connectToDB.Text = "Connect To Database";
            this.connectToDB.Click += new System.EventHandler(this.connectToDB_Click);
            // 
            // genCodeTrigger
            // 
            this.genCodeTrigger.Name = "genCodeTrigger";
            this.genCodeTrigger.Size = new System.Drawing.Size(110, 20);
            this.genCodeTrigger.Text = "Gen Code Trigger";
            this.genCodeTrigger.Click += new System.EventHandler(this.genCodeTrigger_Click);
            // 
            // genCodeStored
            // 
            this.genCodeStored.Name = "genCodeStored";
            this.genCodeStored.Size = new System.Drawing.Size(165, 20);
            this.genCodeStored.Text = "Gen Code Stored Procedure";
            this.genCodeStored.Click += new System.EventHandler(this.genCodeStored_Click);
            // 
            // saveToFile
            // 
            this.saveToFile.Name = "saveToFile";
            this.saveToFile.Size = new System.Drawing.Size(79, 20);
            this.saveToFile.Text = "Save To File";
            this.saveToFile.Click += new System.EventHandler(this.saveToFile_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbSelectAll);
            this.groupBox1.Controls.Add(this.clbTables);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(269, 590);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tables";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabTrigger);
            this.tabControl1.Controls.Add(this.tabStored);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(269, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(690, 590);
            this.tabControl1.TabIndex = 2;
            // 
            // tabTrigger
            // 
            this.tabTrigger.Controls.Add(this.rtxTrigger);
            this.tabTrigger.Location = new System.Drawing.Point(4, 23);
            this.tabTrigger.Name = "tabTrigger";
            this.tabTrigger.Padding = new System.Windows.Forms.Padding(3);
            this.tabTrigger.Size = new System.Drawing.Size(682, 563);
            this.tabTrigger.TabIndex = 0;
            this.tabTrigger.Text = "Trigger";
            this.tabTrigger.UseVisualStyleBackColor = true;
            // 
            // tabStored
            // 
            this.tabStored.Controls.Add(this.rtxStored);
            this.tabStored.Location = new System.Drawing.Point(4, 23);
            this.tabStored.Name = "tabStored";
            this.tabStored.Padding = new System.Windows.Forms.Padding(3);
            this.tabStored.Size = new System.Drawing.Size(682, 563);
            this.tabStored.TabIndex = 1;
            this.tabStored.Text = "Stoted";
            this.tabStored.UseVisualStyleBackColor = true;
            // 
            // clbTables
            // 
            this.clbTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbTables.FormattingEnabled = true;
            this.clbTables.Location = new System.Drawing.Point(3, 18);
            this.clbTables.Name = "clbTables";
            this.clbTables.Size = new System.Drawing.Size(263, 569);
            this.clbTables.TabIndex = 0;
            // 
            // rtxTrigger
            // 
            this.rtxTrigger.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxTrigger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxTrigger.Location = new System.Drawing.Point(3, 3);
            this.rtxTrigger.Name = "rtxTrigger";
            this.rtxTrigger.Size = new System.Drawing.Size(676, 557);
            this.rtxTrigger.TabIndex = 0;
            this.rtxTrigger.Text = "";
            // 
            // rtxStored
            // 
            this.rtxStored.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxStored.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxStored.Location = new System.Drawing.Point(3, 3);
            this.rtxStored.Name = "rtxStored";
            this.rtxStored.Size = new System.Drawing.Size(676, 557);
            this.rtxStored.TabIndex = 0;
            this.rtxStored.Text = "";
            // 
            // cbSelectAll
            // 
            this.cbSelectAll.AutoSize = true;
            this.cbSelectAll.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.cbSelectAll.Location = new System.Drawing.Point(51, 0);
            this.cbSelectAll.Name = "cbSelectAll";
            this.cbSelectAll.Size = new System.Drawing.Size(77, 18);
            this.cbSelectAll.TabIndex = 1;
            this.cbSelectAll.Text = "Select All";
            this.cbSelectAll.UseVisualStyleBackColor = false;
            this.cbSelectAll.CheckedChanged += new System.EventHandler(this.cbSelectAll_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 614);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.m_Menu);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.m_Menu;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "C# Gen Code - 2024 @nvn";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.m_Menu.ResumeLayout(false);
            this.m_Menu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabTrigger.ResumeLayout(false);
            this.tabStored.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip m_Menu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToDB;
        private System.Windows.Forms.ToolStripMenuItem genCodeTrigger;
        private System.Windows.Forms.ToolStripMenuItem genCodeStored;
        private System.Windows.Forms.ToolStripMenuItem saveToFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabTrigger;
        private System.Windows.Forms.TabPage tabStored;
        private System.Windows.Forms.CheckedListBox clbTables;
        private System.Windows.Forms.RichTextBox rtxTrigger;
        private System.Windows.Forms.RichTextBox rtxStored;
        private System.Windows.Forms.CheckBox cbSelectAll;
    }
}