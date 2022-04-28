using BITCollege_RS.Data;
using BITCollege_RS.Models;
using BITCollegeSite.BITCollegeSiteServiceReference;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BITCollegeSite
{
    public partial class ViewDrop : System.Web.UI.Page
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
                        String courseNumber = (String)Session["SelectedCourseNumber"].ToString();


                        int course = db.Courses
                                        .Where(x => x.CourseNumber == courseNumber)
                                        .Select(x => x.CourseId)
                                        .SingleOrDefault();

                        IQueryable<Registration> registrations = (IQueryable<Registration>)Session["allRegistrations"];

                        IQueryable<Registration> reg = registrations.Where(x => x.CourseId == course);

                        //IQueryable<Registration> reg = from results in registrations where results.CourseId == course select results;
                        /* Registration registration = db.Registrations
                                                        .Where(x => x.CourseId == course)
                                                        .Where(x => x.StudentId == student.StudentId)
                                                        .SingleOrDefault();*/
                        Session["viewRegs"] = reg;
                        dvDropViewDetails.DataSource = reg.ToList();

                        this.DataBind();
                        this.lbdrop_Enable();
                        
                        
                    }
                }
                else
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
            }
            catch (Exception ex)
            {
                lblViewDropExceptions.Visible = true;
                lblViewDropExceptions.Text = ex.Message;
            }


        }
        protected void lbdrop_Enable()
        {
            if (dvDropViewDetails.Rows[4].Cells[1].Text == "&nbsp;")
            {
                lbDrop.Enabled = true;
            }
            else
            {
                lbDrop.Enabled = false;
            }
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StudentRegistrations.aspx");
        }

        protected void lbDrop_Click(object sender, EventArgs e)
        {
            CollegeRegistrationClient service = new CollegeRegistrationClient();
            int regId = int.Parse(dvDropViewDetails.Rows[0].Cells[1].Text);
            if (service.DropCourse(regId) == true)
            {
                Response.Redirect("~/StudentRegistrations.aspx");
            }
            else
            {
                lblViewDropExceptions.Text = "Error! Unable to drop the course.";
            }
        }

        protected void dvDropViewDetails_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
        {
            dvDropViewDetails.PageIndex = e.NewPageIndex;
            IQueryable<Registration> reg = (IQueryable<Registration>)Session["viewRegs"];
            dvDropViewDetails.DataSource = reg.ToList();
            this.DataBind();
            this.lbdrop_Enable();

        }
    }
}