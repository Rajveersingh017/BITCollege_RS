using BITCollege_RS.Data;
using BITCollege_RS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
namespace BITCollegeService
{
    // NOTE: You can use the "Rename" command on the "Re factor" menu to change the class name "CollegeRegistration" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CollegeRegistration.svc or CollegeRegistration.svc.cs at the Solution Explorer and start debugging.
    public class CollegeRegistration : ICollegeRegistration
    {
        BITCollege_RSContext db = new BITCollege_RSContext();

        public void DoWork()
        {
        }

        /// <summary>
        /// The DropCourse method finds and deletes the registrationId.
        /// </summary>
        /// <param name="registrationId">Registration Id from the database is passed in the method.</param>
        /// <returns>returns true if the delete is successful.</returns>
        public bool DropCourse(int registrationId)
        {
            bool status = false;
            try
            {
                Registration registration = executeQuery(registrationId);
                if (registration != null) {
                    db.Registrations.Remove(registration);
                    db.SaveChanges();
                    status = true;
                }
            }
            catch(Exception)
            {
                 status = false;
            }
            return status;
        }

        /// <summary>
        /// the RegisterCourse method validates and registers the course.
        /// </summary>
        /// <param name="studentId">Student Id is passed through studentId.</param>
        /// <param name="courseId">Course Id is passed through courseId.</param>
        /// <param name="notes">Notes is passed through notes.</param>
        /// <returns>Returns Response variable with a specific error code.</returns>
        public int RegisterCourse(int studentId, int courseId, string notes)
        {
            // stores the errorcode.
            int response = 100;
            // stores 0 if the validation is succesfull.
            int validation = 0;

            Course course = (from results in db.Courses
                            where results.CourseId == courseId
                            select results).SingleOrDefault();


            // Extracting collection of registrations where course type is mastery.
            IQueryable<Registration> registrations = from results in db.Registrations
                                                        where results.StudentId == studentId
                                                        where results.CourseId == courseId
                                                        where results.Course.CourseType == "Mastery"
                                                        select results;
            // change it courses and extract it  from there 
            int maximumAttempts = (from results in db.MasteryCourses
                                    where results.CourseId == courseId
                                    select results.MaximumAttempts).SingleOrDefault();

            Registration registration = (from results in db.Registrations
                                         where results.StudentId == studentId
                                         where results.CourseId == courseId
                                         select results).SingleOrDefault();
            
            if(registration.CourseId == courseId && registration.Grade == null)
            {
                validation = 1;
                response = -100;
            }
            if (registrations.Count() > maximumAttempts)
            {
                validation = 1;
                response = -200;
            }
            if (validation == 0)
            {
                try {
                    registration.StudentId = studentId;
                    registration.CourseId = courseId;
                    registration.Notes = notes;
                    registration.RegistrationDate = DateTime.UtcNow;
                    //registration.RegistrationNumber = (long)StoredProcedure.NextUniqueNumber("NextRegistration");
                    registration.SetNextRegistrationNumber();
                    registration.Student.OutstandingFees = course.TuitionAmount * registration.Student.GradePointState.TuitionRateAdjustment(registration.Student);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    response = -300;
                }
            }
            return response;
        }

        /// <summary>
        /// The UpdateGrade method takes the arguments to update the registration record and saves the changes to the database. 
        /// </summary>
        /// <param name="grade">New grades are passed through grade.</param>
        /// <param name="registrationId">New Registration is being passed through registrationId.</param>
        /// <param name="notes">New notes are being passed through notes.</param>
        public void UpdateGrade(double grade, int registrationId, string notes)
        {
            Registration registration = (from results in db.Registrations
                                     where results.RegistrationId == registrationId
                                     select results).SingleOrDefault();
            registration.Grade = grade;
            registration.Notes = notes;
            CalculateGradePointAverage(registration.StudentId);
            db.SaveChanges();
        }

        /// <summary>
        /// The CalculateGradePointAverage method takes studentId as an argument 
        /// and updates the gradePointAverage of a particular student.
        /// </summary>
        /// <param name="studentId">contains the studentId of the student.</param>
        private void CalculateGradePointAverage(int studentId) 
        {
            double? totalgrade = 0;
            double? totalCreditHours = 0;
            IQueryable<Registration> registrations = from results in db.Registrations
                                                     where results.StudentId == studentId
                                                     //where results.Grade != null
                                                     select results;
            Student student = db.Students.Where(x => x.StudentId == studentId).SingleOrDefault();
            // student = db.Students.Find(studentId);



            foreach (Registration registration in registrations.ToList())
            {
                if (registration.Grade != null)
                {
                    if (registration.Course.CourseType != "Audit")
                    {
                        //double? grade = registration.Grade * registration.Course.CreditHours;
                        Utility.CourseType type = Utility.BusinessRules.CourseTypeLookup(registration.Course.CourseType);
                        double grade = Utility.BusinessRules.GradeLookup((double)registration.Grade, type);
                        totalgrade += grade * registration.Course.CreditHours;
                        totalCreditHours += registration.Course.CreditHours;
                    }
                }

            }

        }

        /// <summary>
        /// The ExecuteQuery method executes the query to extract the registration object.
        /// </summary>
        /// <param name="registrationId">Contains the registration id of the student and course.</param>
        /// <returns>Returns the object of registration populated with a row or null.</returns>
        private Registration executeQuery(int registrationId)
        {              
            Registration register = (from results in db.Registrations
                                    where results.RegistrationId == registrationId
                                    select results).SingleOrDefault();

            return register;
        }
    }
}
