using BITCollege_RS.Data;
using BITCollege_RS.Models;
using BITCollegeService;
using BITCollegeWindows.WindowsWCFService;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BITCollegeWindows
{
    public partial class Grading : Form
    {
        ///given:  student and registration data will passed throughout 
        ///application. This object will be used to store the current
        ///student and selected registration
        ConstructorData constructorData;
        BITCollege_RSContext db = new BITCollege_RSContext();
        
        public Grading()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// given:  This constructor will be used when called from the
        /// Student form.  This constructor will receive 
        /// specific information about the student and registration
        /// further code required:  
        /// </summary>
        /// <param name="student">specific student instance</param>
        /// <param name="registration">specific registration instance</param>
        public Grading(ConstructorData constructorData)
        {
            InitializeComponent();

            //further code to be added
            this.constructorData = constructorData;
            studentBindingSource.DataSource = constructorData.StudentSpecificData;
            registrationBindingSource.DataSource = constructorData.RegistrationSpecificData;
        }

        /// <summary>
        /// given: this code will navigate back to the Student form with
        /// the specific student and registration data that launched
        /// this form.
        /// </summary>
        private void lnkReturn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //return to student with the data selected for this form
            StudentData student = new StudentData(constructorData);
            student.MdiParent = this.MdiParent;
            student.Show();
            this.Close();
        }

        /// <summary>
        /// given:  open in top right of frame
        /// further code required:
        /// </summary>
        private void Grading_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
            Student student = constructorData.StudentSpecificData;
            Registration registration = (Registration)constructorData.RegistrationSpecificData;
            courseNumberMaskedLabel.Mask = Utility.BusinessRules.CourseFormat(registration.Course.CourseType);

            if(registration.Grade != null )
            {
                gradeTextBox.Enabled = false;
                lnkUpdate.Enabled = false;
                lblExisting.Visible = true;
            }
            else
            {
                gradeTextBox.Enabled = true;
                lnkUpdate.Enabled = true;
                lblExisting.Visible = false;
            }
        }

        /// <summary>
        /// Following event handler is used when the user enters the grade and clicks update.
        /// The code will invoke the functions of Business class to check if the right grade has been.
        /// Furthermore, it will make use of the WCF services to update the grade.
        /// </summary>
        /// <param name="sender">lnkUpdate(a link button on the form).</param>
        /// <param name="e">raises the event when lnkUpdate is clicked.</param>
        private void lnkUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string rawGrade1 = Utility.Numeric.ClearFormatting(gradeTextBox.Text, "%");
            string rawGrade = Utility.Numeric.ClearFormatting(rawGrade1, ",");



            // Checking if the grade is in the right format or not.
            if (Utility.Numeric.IsNumeric(rawGrade, System.Globalization.NumberStyles.Float) == true)
            {



                double grade = double.Parse(rawGrade) / 100;



                // Checking if the grade is within the required range or not.
                if (grade > 0 && grade <= 1)
                {



                    //string studentnumRaw = studentNumberMaskedLabel.Text;
                    //int studentum = int.Parse(studentnumRaw.Replace("-",""));
                    //Student student = db.Students.Where(x => x.StudentNumber == studentum).SingleOrDefault();



                    //string courseNumber = courseNumberMaskedLabel.Text.Replace("-", "").ToString();
                    //string coursenum = courseNumber.Insert(1, "-");
                    //Course course = db.Courses.Where(x=>x.CourseNumber == coursenum).SingleOrDefault();



                    //Registration registration = db.Registrations
                    //                              .Where(x => x.StudentId == student.StudentId)
                    //                            .Where(x => x.CourseId == course.CourseId)
                    //                          .Where(x=>x.Grade == null)
                    //                        .SingleOrDefault();



                    //int studentnum = int.Parse()
                    //long studentNumber = long.Parse(studentNumberMaskedLabel.Text);



                    Registration registration = (Registration)constructorData.RegistrationSpecificData;



                    //Student student = (Student)constructorData.StudentSpecificData;
                    //Registration reg = from reg in db.Registrations where;
                    CollegeRegistrationClient service = new CollegeRegistrationClient();
                    service.UpdateGrade(grade, registration.RegistrationId, "updated through windows application");
                    gradeTextBox.ReadOnly = true;

                }
                else
                {
                    MessageBox.Show("Please enter the grade after dividing it with 100.","Invalid Grade", MessageBoxButtons.OK);
                }

            }
        }
    }
}
