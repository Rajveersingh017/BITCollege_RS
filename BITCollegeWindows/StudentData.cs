using BITCollege_RS.Data;
using BITCollege_RS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BITCollegeWindows
{
    public partial class StudentData : Form
    {
        ///Given: Student and Registration data will be retrieved
        ///in this form and passed throughout application
        ///These variables will be used to store the current
        ///Student and selected Registration
        ConstructorData constructorData = new ConstructorData();
        BITCollege_RSContext db = new BITCollege_RSContext();
        public StudentData()
        {
            InitializeComponent();
        }


        /// <summary>
        /// given:  This constructor will be used when returning to frmStudent
        /// from another form.  This constructor will pass back
        /// specific information about the student and registration
        /// based on activites taking place in another form
        /// </summary>
        /// <param name="constructorData">Student data passed among forms</param>
        public StudentData(ConstructorData constructorData)
        {
            InitializeComponent();

            studentNumberMaskedTextBox.Select();
            //further code to be added  
            //registrationBindingSource.DataSource = constructorData.RegistrationSpecificData;
            this.constructorData = constructorData;
            Student student = (Student)constructorData.StudentSpecificData;
            studentNumberMaskedTextBox.Text = student.StudentNumber.ToString();
            studentNumberMaskedTextBox.TabStop = false;
            this.studentNumberMaskedTextBox_Leave(this, new EventArgs());
        }

        /// <summary>
        /// given: open grading form passing constructor data
        /// </summary>
        private void lnkUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Grading grading = new Grading(constructorData);
            grading.MdiParent = this.MdiParent;
            grading.Show();
            this.Close();
        }

        /// <summary>
        /// given: open history form passing data
        /// </summary>
        private void lnkDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            History history = new History(constructorData);
            history.MdiParent = this.MdiParent;
            history.Show();
            this.Close();
        }
        /// <summary>
        /// given:  opens form in top right of frame
        /// </summary>
        private void StudentData_Load(object sender, EventArgs e)
        {
            //keeps location of form static when opened and closed
            this.Location = new Point(0, 0);
        }

        /// <summary>
        /// Method to set the data in ConstructorData class.
        /// </summary>
        private void populateConstructorData()
        {
            
            constructorData.RegistrationSpecificData = (Registration)registrationBindingSource.Current;
            constructorData.StudentSpecificData = (Student)studentBindingSource.Current;
        }

        /// <summary>
        /// Following event runs when users tabs out of the studentNumberTextbox.
        /// Finds the related data and of the student and displays it to the user.
        /// </summary>
        /// <param name="sender">StudentMaskedTextBox (A text-box on the form).</param>
        /// <param name="e">Raises the event when user tabs out of the text-box.</param>
        private void studentNumberMaskedTextBox_Leave(object sender, EventArgs e)
        {
            
            int valid = 1;
            
            try
            {
                int studentNumber = int.Parse(studentNumberMaskedTextBox.Text);

                Student student = db.Students
                                    .Where(x => x.StudentNumber == studentNumber)
                                    .SingleOrDefault();

                studentBindingSource.DataSource = student;

                if (student == null)
                {
                    valid = 1;
                    lnkDetails.Enabled = false;
                    lnkUpdate.Enabled = false;

                    studentBindingSource.Clear();
                    registrationBindingSource.Clear();

                    MessageBox.Show("Student " + studentNumber.ToString() + " does not exist", "Invalid Student Number", MessageBoxButtons.OK);

                    studentNumberMaskedTextBox.Select();
                }
                else
                {
                    IQueryable<Registration> registration = db.Registrations.Where(x=>x.StudentId == student.StudentId);

                    if (registration.Count() == 0)
                    {
                        lnkDetails.Enabled = false;
                        lnkUpdate.Enabled = false;
                        
                        registrationBindingSource.Clear();
                    }
                    else
                    {                  
                        registrationBindingSource.DataSource = registration.ToList();

                        lnkDetails.Enabled = true;
                        lnkUpdate.Enabled = true;  
                    }

                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Calls the populateConstructorData method to pass the data to ConstructorData class. 
        /// </summary>
        /// <param name="sender">LnkUpdate, a link button on the form.</param>
        /// <param name="e">Raised when the link button is clicked.</param>
        private void lnkUpdate_Click(object sender, EventArgs e)
        {
            this.populateConstructorData();
        }

        /// <summary>
        /// Calls the populateConstructorData method to pass the data to ConstructorData class. 
        /// </summary>
        /// <param name="sender">LnkDetails, a link button on the form.</param>
        /// <param name="e">Raised when the link button is clicked.</param>
        private void lnkDetails_Click(object sender, EventArgs e)
        {
            this.populateConstructorData();
        }
    }
}
