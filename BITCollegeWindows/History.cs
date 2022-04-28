using BITCollege_RS.Data;
using BITCollege_RS.Models;
using System;
using System.Collections;
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
    public partial class History : Form
    {
        ///given:  student and registration data will passed throughout 
        ///application. This object will be used to store the current
        ///student and selected registration
        ConstructorData constructorData;

        public History()
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
        public History(ConstructorData constructorData)
        {
            InitializeComponent();

            //further code to be added
            this.constructorData = constructorData;
            Student student1 = constructorData.StudentSpecificData;
            /*IQueryable<Registration> registrations = constructorData.RegistrationSpecificData;*/
            BITCollege_RSContext db = new BITCollege_RSContext();

            var reg = (from results in db.Registrations
                       where results.StudentId == student1.StudentId
                       join course in db.Courses
                       on results.CourseId equals course.CourseId
                       select new { RegistrationNumber = results.RegistrationNumber, RegistrationDate = results.RegistrationDate, Title = course.Title, Grade = results.Grade, Notes = results.Notes}) ;

            studentBindingSource.DataSource = student1;
            registrationBindingSource.DataSource = reg.ToList();
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
        private void History_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
        }
    }
}
