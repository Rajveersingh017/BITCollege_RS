using BITCollege_RS.Data;
using BITCollege_RS.Models;
using BITCollegeSite.BITCollegeSiteServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BITCollegeSite
{
    public partial class CourseRegistration : System.Web.UI.Page
    {
        BITCollege_RSContext db = new BITCollege_RSContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Page.User.Identity.IsAuthenticated)
                {
                    if (!Page.IsPostBack)
                    {
                        Student student = (Student)Session["currentStudent"];
                        lblRegStudenName.Text = student.FullName.ToString();
                        /*  IQueryable<Registration> registrations = (IQueryable<Registration>)Session["allRegistrations"];
                          IQueryable<string> numbers = from results in registrations
                                                       where results.StudentId == student.StudentId
                                                       select results.Course.Title;


                          Course[] courses = { };

                          foreach ( int num in numbers)
                          {
                              Course course = db.Courses
                                              .Where(x => x.CourseId == num)
                                              .SingleOrDefault();
                              courses.Append(course);

                          }*/
                        IQueryable<Course> courses = db.Courses.Where(x => x.AcademicProgramId == student.AcademicProgramId);

                        ddlCourseSelector.DataSource = courses.ToList();
                        ddlCourseSelector.DataTextField = "Title";
                        this.DataBind();
                        /*
                        foreach (Course course in numbers)
                        {
                            lblRegErrors.Text = course.ToString();
                        }*/
                    }
                }
                else
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
            }
            catch(Exception exp)
            {
                lblRegErrors.Visible = true;
                lblRegErrors.Text = exp.Message;
            }
        }

        protected void lbRegList_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/studentRegistrations.aspx");
        }

        protected void lbRegister_Click(object sender, EventArgs e)
        {
            rfvNotes.Enabled = true;
            Page.Validate();
            string selected = ddlCourseSelector.SelectedValue.ToString();
            Course course = db.Courses
                            .Where(x=> x.Title == selected)
                            .SingleOrDefault();
            if (Page.IsValid == true)
            {
                CollegeRegistrationClient service = new CollegeRegistrationClient();
                Student student = (Student)Session["currentStudent"];
                service.RegisterCourse(student.StudentId, course.CourseId, txtNotes.Text);
            }
        }
    }
}