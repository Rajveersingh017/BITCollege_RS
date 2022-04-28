namespace BITCollegeWindows
{
    partial class Grading
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
            System.Windows.Forms.Label studentNumberLabel;
            System.Windows.Forms.Label descriptionLabel;
            System.Windows.Forms.Label courseNumberLabel;
            System.Windows.Forms.Label courseTypeLabel;
            System.Windows.Forms.Label gradeLabel;
            this.gbStudent = new System.Windows.Forms.GroupBox();
            this.descriptionLabel1 = new System.Windows.Forms.Label();
            this.studentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.fullNameLabel1 = new System.Windows.Forms.Label();
            this.studentNumberMaskedLabel = new EWSoftware.MaskedLabelControl.MaskedLabel();
            this.gbGrading = new System.Windows.Forms.GroupBox();
            this.gradeTextBox = new System.Windows.Forms.TextBox();
            this.registrationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.courseTypeLabel1 = new System.Windows.Forms.Label();
            this.titleLabel1 = new System.Windows.Forms.Label();
            this.courseNumberMaskedLabel = new EWSoftware.MaskedLabelControl.MaskedLabel();
            this.lblExisting = new System.Windows.Forms.Label();
            this.lnkReturn = new System.Windows.Forms.LinkLabel();
            this.lnkUpdate = new System.Windows.Forms.LinkLabel();
            studentNumberLabel = new System.Windows.Forms.Label();
            descriptionLabel = new System.Windows.Forms.Label();
            courseNumberLabel = new System.Windows.Forms.Label();
            courseTypeLabel = new System.Windows.Forms.Label();
            gradeLabel = new System.Windows.Forms.Label();
            this.gbStudent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.studentBindingSource)).BeginInit();
            this.gbGrading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.registrationBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // studentNumberLabel
            // 
            studentNumberLabel.AutoSize = true;
            studentNumberLabel.Location = new System.Drawing.Point(41, 33);
            studentNumberLabel.Name = "studentNumberLabel";
            studentNumberLabel.Size = new System.Drawing.Size(87, 13);
            studentNumberLabel.TabIndex = 0;
            studentNumberLabel.Text = "Student Number:";
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Location = new System.Drawing.Point(41, 73);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new System.Drawing.Size(63, 13);
            descriptionLabel.TabIndex = 4;
            descriptionLabel.Text = "Description:";
            // 
            // courseNumberLabel
            // 
            courseNumberLabel.AutoSize = true;
            courseNumberLabel.Location = new System.Drawing.Point(50, 39);
            courseNumberLabel.Name = "courseNumberLabel";
            courseNumberLabel.Size = new System.Drawing.Size(83, 13);
            courseNumberLabel.TabIndex = 3;
            courseNumberLabel.Text = "Course Number:";
            // 
            // courseTypeLabel
            // 
            courseTypeLabel.AutoSize = true;
            courseTypeLabel.Location = new System.Drawing.Point(50, 85);
            courseTypeLabel.Name = "courseTypeLabel";
            courseTypeLabel.Size = new System.Drawing.Size(70, 13);
            courseTypeLabel.TabIndex = 7;
            courseTypeLabel.Text = "Course Type:";
            // 
            // gradeLabel
            // 
            gradeLabel.AutoSize = true;
            gradeLabel.Location = new System.Drawing.Point(50, 142);
            gradeLabel.Name = "gradeLabel";
            gradeLabel.Size = new System.Drawing.Size(39, 13);
            gradeLabel.TabIndex = 9;
            gradeLabel.Text = "Grade:";
            // 
            // gbStudent
            // 
            this.gbStudent.Controls.Add(descriptionLabel);
            this.gbStudent.Controls.Add(this.descriptionLabel1);
            this.gbStudent.Controls.Add(this.fullNameLabel1);
            this.gbStudent.Controls.Add(studentNumberLabel);
            this.gbStudent.Controls.Add(this.studentNumberMaskedLabel);
            this.gbStudent.Location = new System.Drawing.Point(20, 39);
            this.gbStudent.Margin = new System.Windows.Forms.Padding(2);
            this.gbStudent.Name = "gbStudent";
            this.gbStudent.Padding = new System.Windows.Forms.Padding(2);
            this.gbStudent.Size = new System.Drawing.Size(572, 105);
            this.gbStudent.TabIndex = 0;
            this.gbStudent.TabStop = false;
            this.gbStudent.Text = "Student Data";
            // 
            // descriptionLabel1
            // 
            this.descriptionLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.descriptionLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentBindingSource, "AcademicProgram.Description", true));
            this.descriptionLabel1.Location = new System.Drawing.Point(134, 72);
            this.descriptionLabel1.Name = "descriptionLabel1";
            this.descriptionLabel1.Size = new System.Drawing.Size(274, 23);
            this.descriptionLabel1.TabIndex = 5;
            // 
            // studentBindingSource
            // 
            this.studentBindingSource.DataSource = typeof(BITCollege_RS.Models.Student);
            // 
            // fullNameLabel1
            // 
            this.fullNameLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fullNameLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentBindingSource, "FullName", true));
            this.fullNameLabel1.Location = new System.Drawing.Point(252, 32);
            this.fullNameLabel1.Name = "fullNameLabel1";
            this.fullNameLabel1.Size = new System.Drawing.Size(263, 23);
            this.fullNameLabel1.TabIndex = 3;
            // 
            // studentNumberMaskedLabel
            // 
            this.studentNumberMaskedLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.studentNumberMaskedLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentBindingSource, "StudentNumber", true));
            this.studentNumberMaskedLabel.Location = new System.Drawing.Point(134, 32);
            this.studentNumberMaskedLabel.Mask = "0000-0000";
            this.studentNumberMaskedLabel.Name = "studentNumberMaskedLabel";
            this.studentNumberMaskedLabel.Size = new System.Drawing.Size(100, 23);
            this.studentNumberMaskedLabel.TabIndex = 1;
            this.studentNumberMaskedLabel.Text = "    -";
            // 
            // gbGrading
            // 
            this.gbGrading.Controls.Add(gradeLabel);
            this.gbGrading.Controls.Add(this.gradeTextBox);
            this.gbGrading.Controls.Add(courseTypeLabel);
            this.gbGrading.Controls.Add(this.courseTypeLabel1);
            this.gbGrading.Controls.Add(this.titleLabel1);
            this.gbGrading.Controls.Add(courseNumberLabel);
            this.gbGrading.Controls.Add(this.courseNumberMaskedLabel);
            this.gbGrading.Controls.Add(this.lblExisting);
            this.gbGrading.Controls.Add(this.lnkReturn);
            this.gbGrading.Controls.Add(this.lnkUpdate);
            this.gbGrading.Location = new System.Drawing.Point(71, 171);
            this.gbGrading.Margin = new System.Windows.Forms.Padding(2);
            this.gbGrading.Name = "gbGrading";
            this.gbGrading.Padding = new System.Windows.Forms.Padding(2);
            this.gbGrading.Size = new System.Drawing.Size(476, 245);
            this.gbGrading.TabIndex = 1;
            this.gbGrading.TabStop = false;
            this.gbGrading.Text = "Grading Information";
            // 
            // gradeTextBox
            // 
            this.gradeTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.registrationBindingSource, "Grade", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "P2"));
            this.gradeTextBox.Location = new System.Drawing.Point(139, 139);
            this.gradeTextBox.Name = "gradeTextBox";
            this.gradeTextBox.Size = new System.Drawing.Size(100, 20);
            this.gradeTextBox.TabIndex = 10;
            // 
            // registrationBindingSource
            // 
            this.registrationBindingSource.DataSource = typeof(BITCollege_RS.Models.Registration);
            // 
            // courseTypeLabel1
            // 
            this.courseTypeLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.courseTypeLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.registrationBindingSource, "Course.CourseType", true));
            this.courseTypeLabel1.Location = new System.Drawing.Point(139, 84);
            this.courseTypeLabel1.Name = "courseTypeLabel1";
            this.courseTypeLabel1.Size = new System.Drawing.Size(100, 23);
            this.courseTypeLabel1.TabIndex = 8;
            // 
            // titleLabel1
            // 
            this.titleLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.titleLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.registrationBindingSource, "Course.Title", true));
            this.titleLabel1.Location = new System.Drawing.Point(266, 38);
            this.titleLabel1.Name = "titleLabel1";
            this.titleLabel1.Size = new System.Drawing.Size(198, 23);
            this.titleLabel1.TabIndex = 6;
            // 
            // courseNumberMaskedLabel
            // 
            this.courseNumberMaskedLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.courseNumberMaskedLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.registrationBindingSource, "Course.CourseNumber", true));
            this.courseNumberMaskedLabel.Location = new System.Drawing.Point(139, 38);
            this.courseNumberMaskedLabel.Name = "courseNumberMaskedLabel";
            this.courseNumberMaskedLabel.Size = new System.Drawing.Size(100, 23);
            this.courseNumberMaskedLabel.TabIndex = 4;
            // 
            // lblExisting
            // 
            this.lblExisting.AutoSize = true;
            this.lblExisting.Location = new System.Drawing.Point(263, 142);
            this.lblExisting.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblExisting.Name = "lblExisting";
            this.lblExisting.Size = new System.Drawing.Size(185, 13);
            this.lblExisting.TabIndex = 2;
            this.lblExisting.Text = "Existing grades cannot be overridden.";
            this.lblExisting.Visible = false;
            // 
            // lnkReturn
            // 
            this.lnkReturn.AutoSize = true;
            this.lnkReturn.Location = new System.Drawing.Point(254, 211);
            this.lnkReturn.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lnkReturn.Name = "lnkReturn";
            this.lnkReturn.Size = new System.Drawing.Size(117, 13);
            this.lnkReturn.TabIndex = 1;
            this.lnkReturn.TabStop = true;
            this.lnkReturn.Text = "Return to Student Data";
            this.lnkReturn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkReturn_LinkClicked);
            // 
            // lnkUpdate
            // 
            this.lnkUpdate.AutoSize = true;
            this.lnkUpdate.Enabled = false;
            this.lnkUpdate.Location = new System.Drawing.Point(123, 211);
            this.lnkUpdate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lnkUpdate.Name = "lnkUpdate";
            this.lnkUpdate.Size = new System.Drawing.Size(74, 13);
            this.lnkUpdate.TabIndex = 0;
            this.lnkUpdate.TabStop = true;
            this.lnkUpdate.Text = "Update Grade";
            this.lnkUpdate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUpdate_LinkClicked);
            // 
            // Grading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 457);
            this.Controls.Add(this.gbGrading);
            this.Controls.Add(this.gbStudent);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Grading";
            this.Text = "Grading";
            this.Load += new System.EventHandler(this.Grading_Load);
            this.gbStudent.ResumeLayout(false);
            this.gbStudent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.studentBindingSource)).EndInit();
            this.gbGrading.ResumeLayout(false);
            this.gbGrading.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.registrationBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbStudent;
        private System.Windows.Forms.GroupBox gbGrading;
        private System.Windows.Forms.Label lblExisting;
        private System.Windows.Forms.LinkLabel lnkReturn;
        private System.Windows.Forms.LinkLabel lnkUpdate;
        private System.Windows.Forms.Label descriptionLabel1;
        private System.Windows.Forms.BindingSource studentBindingSource;
        private System.Windows.Forms.Label fullNameLabel1;
        private EWSoftware.MaskedLabelControl.MaskedLabel studentNumberMaskedLabel;
        private System.Windows.Forms.TextBox gradeTextBox;
        private System.Windows.Forms.BindingSource registrationBindingSource;
        private System.Windows.Forms.Label courseTypeLabel1;
        private System.Windows.Forms.Label titleLabel1;
        private EWSoftware.MaskedLabelControl.MaskedLabel courseNumberMaskedLabel;
    }
}