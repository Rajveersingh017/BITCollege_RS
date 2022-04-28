namespace BITCollegeWindows
{
    partial class Batch
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label descriptionLabel;
            this.grpSelect = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.descriptionComboBox = new System.Windows.Forms.ComboBox();
            this.academicProgramBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lnkProcess = new System.Windows.Forms.LinkLabel();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radSelect = new System.Windows.Forms.RadioButton();
            this.radAll = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.rtxtLog = new System.Windows.Forms.RichTextBox();
            descriptionLabel = new System.Windows.Forms.Label();
            this.grpSelect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.academicProgramBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Location = new System.Drawing.Point(14, 86);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new System.Drawing.Size(80, 13);
            descriptionLabel.TabIndex = 5;
            descriptionLabel.Text = "Program Name:";
            // 
            // grpSelect
            // 
            this.grpSelect.Controls.Add(this.button1);
            this.grpSelect.Controls.Add(this.txtFileName);
            this.grpSelect.Controls.Add(this.label3);
            this.grpSelect.Controls.Add(descriptionLabel);
            this.grpSelect.Controls.Add(this.descriptionComboBox);
            this.grpSelect.Controls.Add(this.lnkProcess);
            this.grpSelect.Controls.Add(this.txtKey);
            this.grpSelect.Controls.Add(this.label1);
            this.grpSelect.Controls.Add(this.radSelect);
            this.grpSelect.Controls.Add(this.radAll);
            this.grpSelect.Location = new System.Drawing.Point(25, 23);
            this.grpSelect.Margin = new System.Windows.Forms.Padding(2);
            this.grpSelect.Name = "grpSelect";
            this.grpSelect.Padding = new System.Windows.Forms.Padding(2);
            this.grpSelect.Size = new System.Drawing.Size(524, 188);
            this.grpSelect.TabIndex = 0;
            this.grpSelect.TabStop = false;
            this.grpSelect.Text = "Batch Selection";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(338, 83);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Decrypt";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(281, 54);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(75, 20);
            this.txtFileName.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(288, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "File Name:";
            // 
            // descriptionComboBox
            // 
            this.descriptionComboBox.DataSource = this.academicProgramBindingSource;
            this.descriptionComboBox.DisplayMember = "Description";
            this.descriptionComboBox.Enabled = false;
            this.descriptionComboBox.FormattingEnabled = true;
            this.descriptionComboBox.Location = new System.Drawing.Point(100, 83);
            this.descriptionComboBox.Name = "descriptionComboBox";
            this.descriptionComboBox.Size = new System.Drawing.Size(121, 21);
            this.descriptionComboBox.TabIndex = 6;
            this.descriptionComboBox.ValueMember = "ProgramAcronym";
            // 
            // academicProgramBindingSource
            // 
            this.academicProgramBindingSource.DataSource = typeof(BITCollege_RS.Models.AcademicProgram);
            // 
            // lnkProcess
            // 
            this.lnkProcess.AutoSize = true;
            this.lnkProcess.Location = new System.Drawing.Point(17, 154);
            this.lnkProcess.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lnkProcess.Name = "lnkProcess";
            this.lnkProcess.Size = new System.Drawing.Size(76, 13);
            this.lnkProcess.TabIndex = 4;
            this.lnkProcess.TabStop = true;
            this.lnkProcess.Text = "Process Batch";
            this.lnkProcess.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkProcess_LinkClicked);
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(390, 54);
            this.txtKey.Margin = new System.Windows.Forms.Padding(2);
            this.txtKey.Name = "txtKey";
            this.txtKey.PasswordChar = '*';
            this.txtKey.Size = new System.Drawing.Size(76, 20);
            this.txtKey.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(400, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Enter Key:";
            // 
            // radSelect
            // 
            this.radSelect.AutoSize = true;
            this.radSelect.Location = new System.Drawing.Point(17, 54);
            this.radSelect.Margin = new System.Windows.Forms.Padding(2);
            this.radSelect.Name = "radSelect";
            this.radSelect.Size = new System.Drawing.Size(213, 17);
            this.radSelect.TabIndex = 1;
            this.radSelect.TabStop = true;
            this.radSelect.Text = "Select a Program to Grade and Register";
            this.radSelect.UseVisualStyleBackColor = true;
            this.radSelect.CheckedChanged += new System.EventHandler(this.radSelect_CheckedChanged);
            // 
            // radAll
            // 
            this.radAll.AutoSize = true;
            this.radAll.Location = new System.Drawing.Point(17, 27);
            this.radAll.Margin = new System.Windows.Forms.Padding(2);
            this.radAll.Name = "radAll";
            this.radAll.Size = new System.Drawing.Size(201, 17);
            this.radAll.TabIndex = 0;
            this.radAll.TabStop = true;
            this.radAll.Text = "Grade and Register for ALL Programs";
            this.radAll.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 230);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Log:";
            // 
            // rtxtLog
            // 
            this.rtxtLog.Location = new System.Drawing.Point(25, 259);
            this.rtxtLog.Margin = new System.Windows.Forms.Padding(2);
            this.rtxtLog.Name = "rtxtLog";
            this.rtxtLog.Size = new System.Drawing.Size(525, 195);
            this.rtxtLog.TabIndex = 2;
            this.rtxtLog.Text = "";
            // 
            // Batch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 492);
            this.Controls.Add(this.rtxtLog);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.grpSelect);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Batch";
            this.Text = "Batch Student Update";
            this.Load += new System.EventHandler(this.Batch_Load);
            this.grpSelect.ResumeLayout(false);
            this.grpSelect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.academicProgramBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSelect;
        private System.Windows.Forms.LinkLabel lnkProcess;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radSelect;
        private System.Windows.Forms.RadioButton radAll;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox rtxtLog;
        private System.Windows.Forms.ComboBox descriptionComboBox;
        private System.Windows.Forms.BindingSource academicProgramBindingSource;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label3;
    }
}