using BITCollege_RS.Data;
using BITCollege_RS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BITCollegeSite
{
    public partial class StudentRegistrations : System.Web.UI.Page
    {
        BITCollege_RSContext db = new BITCollege_RSContext();
        protected void Page_Load(object sender, EventArgs Exception)
        {
            try
            {
                if (Page.User.Identity.IsAuthenticated)
                {
                    if (!Page.IsPostBack)
                    {
                        string userName = Page.User.Identity.Name;
                        long studentNumber = long.Parse(userName.Substring(0, userName.IndexOf("@")));
                        Student student = db.Students
                                            .Where(x => x.StudentNumber == studentNumber)
                                            .SingleOrDefault();
                        Session["currentStudent"] = student;
                        LblUserName.Text = student.FullName;
                        IQueryable<Registration> registrations = db.Registrations
                                                                    .Where(x => x.StudentId == student.StudentId);
                        Session["allRegistrations"] = registrations;
                        GVRegistrations.DataSource = registrations.ToList();

                        this.DataBind();
                    }
                }
                else
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
            }
            catch (Exception e)
            {
                lblExceptions.Enabled = true;
                lblExceptions.Text = e.Message;
            }

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["SelectedCourseNumber"] = GVRegistrations.Rows[GVRegistrations.SelectedIndex].Cells[1].Text;
            Response.Redirect("~/ViewDrop.aspx");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CourseRegistration.aspx");
        }
    }
}