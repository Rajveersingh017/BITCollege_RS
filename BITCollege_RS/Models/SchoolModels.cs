using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using Microsoft.Ajax.Utilities;
using BITCollege_RS.Data;
using System.Data.Entity.Migrations;
using BITCollege_RS.Controllers;
using System.Dynamic;
using System.Data.SqlClient;
using System.Data;

namespace BITCollege_RS.Models
{
    /// <summary>
    /// The GradePointState class 
    /// contains all the methods that describes the current state of grades.
    /// </summary>
    public abstract class GradePointState
    {
        protected static BITCollege_RSContext db = new BITCollege_RSContext();
        /// <summary>
        /// Auto-implemented property for getting and setting GradePointStateId.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int GradePointStateId { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting LowerLimit.
        /// </summary>
        [Required]
        [DisplayFormat(DataFormatString = "{0:F}")]
        [DisplayName("LowerLimit")]
        public double LowerLimit { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting UpperLimit.
        /// </summary>
        [Required]
        [DisplayFormat(DataFormatString = "{0:F}")]
        [DisplayName("upperLimit")]
        public double UpperLimit { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting TuitionRateFactor.
        /// </summary>
        [Required]
        [DisplayFormat(DataFormatString = "{0:F}")]
        [DisplayName("TuitionRateFactor")]
        public double TuitionRateFactor { get; set; }

        /// <summary>
        /// Read-only property to return the current state of grades.
        /// </summary>
        [DisplayName("GradePointState")]
        public string Description
        {
            get
            {
                return GetType().Name.ToString().Substring(0, GetType().Name.ToString().IndexOf("State"));
            }
        }
        /// <summary>
        /// Virtual method that returns the adjustment that has been made to the students fees.
        /// </summary>
        /// <param name="student">Student is the object of Student class</param>
        /// <returns></returns>
        public virtual double TuitionRateAdjustment(Student student)
        {
            return 0.0;
        }
        /// <summary>
        /// Virtual method that returns the state of the context object.
        /// </summary>
        /// <param name="student">Student is the object of Student class</param>
        public virtual void StateChangeCheck(Student student) { }

        // It shows 0 to many relationship.
        public virtual ICollection<Student> Student { get; set; }
    }
                                                                                                                             #region GradePointState's Sub Classes
    /// <summary>
    /// The SuspendedState class is the child class of GradePointState class.
    /// </summary>
    public class SuspendedState : GradePointState
    {
        private static SuspendedState suspendedState;

        /// <summary>
        /// SuspendedState is the private constructor of the class.
        /// </summary>
        private SuspendedState()
        {
            this.LowerLimit = 0.00;
            this.UpperLimit = 1.00;
            this.TuitionRateFactor = 1.1;
        }

        /// <summary>
        /// The GetInstance method follows the singleton pattern to ensure that the private variable has instantiated.
        /// </summary>
        /// <returns>Returns the private variable.</returns>
        public static SuspendedState GetInstance()
        {
            if (suspendedState == null)
            {
                suspendedState = db.SuspendedStates.SingleOrDefault();
                if (suspendedState == null)
                {
                    suspendedState = new SuspendedState();
                    db.SuspendedStates.Add(suspendedState);
                    db.SaveChanges();
                }
            }
            return suspendedState;
        }

        /// <summary>
        /// The TuitionRateAdjustment overrides the super class method to 
        /// set the new value to TuitionRateFactor.
        /// </summary>
        /// <param name="student">Student is the object of the Student class.</param>
        /// <returns></returns>
        public override double TuitionRateAdjustment(Student student)
        {
            double rate = this.TuitionRateFactor;
            // Registration reg = new Registration();
            IQueryable<Registration> reg = from results in db.Registrations
                                           where results.StudentId == student.StudentId
                                           where results.Grade == null
                                           select results;
            if (reg.Count() >= 1)
            {
                rate += .10;
            }
            if (student.GradePointAverage < .75)
            {
                rate += .02;
            }
            if (student.GradePointAverage < .50)
            {
                rate += .05;
            }
            return rate;
        }

        /// <summary>
        /// StateChangeCheck method that returns the state of the context object.
        /// </summary>
        /// <param name="student">Student is the Student class's object.</param>
        public override void StateChangeCheck(Student student)
        {
            if (student.GradePointAverage > this.UpperLimit)
            {
                student.GradePointStateId = ProbationState.GetInstance().GradePointStateId;
            }
            //db.Students.AddOrUpdate(student);
            db.SaveChanges();
        }
    }

    /// <summary>
    /// The ProbationState class is the child class of GradePointState Class
    /// </summary>
    public class ProbationState : GradePointState
    {
        private static ProbationState probationState;

        /// <summary>
        /// ProbationState is the private constructor of the class.
        /// </summary>
        private ProbationState()
        {
            this.LowerLimit = 1.00;
            this.UpperLimit = 2.00;
            this.TuitionRateFactor = 1.075;
        }

        /// <summary>
        /// GetInstance method follows the singleton pattern.
        /// </summary>
        /// <returns>Returns the private variable of the class.</returns>
        public static ProbationState GetInstance()
        {
            if (probationState == null)
            {
                probationState = db.ProbationStates.SingleOrDefault();
                if (probationState == null)
                {
                    probationState = new ProbationState();
                    db.ProbationStates.Add(probationState);
                    db.SaveChanges();
                }
            }
            return probationState;
        }
        /// <summary>
        /// TuitionRateAdjusment method overrides the super class method.
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override double TuitionRateAdjustment(Student student)
        {

            double rate = this.TuitionRateFactor;
            IQueryable<Registration> reg = from results in db.Registrations
                                           where results.StudentId == student.StudentId
                                           where results.Grade != null
                                           select results;
            if (reg.Count() == 0)
            {
                rate += .075;
            }
            if (reg.Count() >= 5)
            {
                rate += .035;
            }
            return rate;
        }

        /// <summary>
        /// StateChangeCheck method that returns the state of the context object.
        /// </summary>
        /// <param name="student">Student is the Student class's object.</param>
        public override void StateChangeCheck(Student student)
        {
            if (student.GradePointAverage > this.UpperLimit)
            {
                student.GradePointStateId = RegularState.GetInstance().GradePointStateId;
            }
            if (student.GradePointAverage < this.LowerLimit)
            {
                student.GradePointStateId = SuspendedState.GetInstance().GradePointStateId;
            }
            //db.Students.AddOrUpdate(student);
            db.SaveChanges();
        }
    }

    /// <summary>
    /// The RegularState class is the child class of GradePointState class.
    /// </summary>
    public class RegularState : GradePointState
    {
        private static RegularState regularState;

        /// <summary>
        /// RegularState is the private constructor of the class.
        /// </summary>
        private RegularState()
        {
            this.LowerLimit = 2;
            this.UpperLimit = 3.70;
            this.TuitionRateFactor = 1;
        }

        /// <summary>
        /// GetInstance method follows the singleton pattern.
        /// </summary>
        /// <returns>Returns the private variable of the class.</returns>
        public static RegularState GetInstance()
        {
            if (regularState == null)
            {
                regularState = db.RegularStates.SingleOrDefault();
                if (regularState == null)
                {
                    regularState = new RegularState();
                    db.RegularStates.Add(regularState);
                    db.SaveChanges();
                }
            }
            return regularState;
        }

        /// <summary>
        /// TuitionRateAdjusment method overrides the super class method.
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override double TuitionRateAdjustment(Student student)
        {
            return this.TuitionRateFactor;
        }

        /// <summary>
        /// StateChangeCheck method that returns the state of the context object.
        /// </summary>
        /// <param name="student">Student is the Student class's object.</param>
        public override void StateChangeCheck(Student student)
        {
            if (student.GradePointAverage > this.UpperLimit)
            {
                student.GradePointStateId = HonoursState.GetInstance().GradePointStateId;
            }
            if (student.GradePointAverage < this.LowerLimit)
            {
                student.GradePointStateId = ProbationState.GetInstance().GradePointStateId;
            }
            //db.Students.AddOrUpdate(student);
            db.SaveChanges();
        }

    }

    /// <summary>
    /// The HonoursState class is the child class of GradePointState class.
    /// </summary>
    public class HonoursState : GradePointState
    {
        private static HonoursState honoursState;
        /// <summary>
        /// HonoursState is the private constructor of the class.
        /// </summary>
        private HonoursState()
        {
            this.LowerLimit = 3.7;
            this.UpperLimit = 4.5;
            this.TuitionRateFactor = .9;
        }

        /// <summary>
        /// GetInstance method follows the singleton pattern.
        /// </summary>
        /// <returns>Returns the private variable of the class.</returns>
        public static HonoursState GetInstance()
        {
            if (honoursState == null)
            {
                honoursState = db.HonoursStates.SingleOrDefault();
                if (honoursState == null)
                {
                    honoursState = new HonoursState();
                    db.HonoursStates.Add(honoursState);
                    db.SaveChanges();
                }
            }
            return honoursState;
        }

        /// <summary>
        /// TuitionRateAdjustment overrides the super class method.
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override double TuitionRateAdjustment(Student student)
        {
            double rate = this.TuitionRateFactor;
            IQueryable<Registration> reg = from results in db.Registrations
                                           where results.StudentId == student.StudentId
                                           where results.Grade != null
                                           select results;
            if (reg.Count() == 0)
            {
                rate -= .10;
            }
            if (reg.Count() >= 5)
            {
                rate -= .15;
            }
            if (student.GradePointAverage > 4.25)
            {
                rate -= .02;
            }
            return rate;
        }

        /// <summary>
        /// StateChangeCheck method that returns the state of the context object.
        /// </summary>
        /// <param name="student">Student is the Student class's object.</param>
        public override void StateChangeCheck(Student student)
        {
            if (student.GradePointAverage < this.LowerLimit)
            {
                student.GradePointStateId = RegularState.GetInstance().GradePointStateId;
            }
            //db.Students.AddOrUpdate(student);
            db.SaveChanges();
        }
        
    }
    #endregion
    /// <summary>
    /// The Student class 
    /// contains all the methods that grabs/sets student's information.
    /// </summary>
    public class Student
    {
        // Database context object.
        BITCollege_RSContext db = new BITCollege_RSContext();

        /// <summary>
        /// Auto-implemented property for getting and setting Student ID.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting GradePointStateId.
        /// </summary>
        [ForeignKey("GradePointState")]
        [Required]
        public int GradePointStateId { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting AcademicProgramId.
        /// </summary>
        [ForeignKey("AcademicProgram")]
        public int? AcademicProgramId { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting StudentNumber.
        /// </summary>
        //[Required]
        //[Range(10000000, 99999999)]
        [DisplayName("StudentNumber")]
        public long StudentNumber { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting FirstName.
        /// </summary>
        [Required]
        [StringLength(35, MinimumLength = 1)]
        [DisplayName("First\nName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting LastName.
        /// </summary>
        [Required]
        [StringLength(35, MinimumLength = 1)]
        [DisplayName("LastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting Address.
        /// </summary>
        [Required]
        [StringLength(35, MinimumLength = 1)]
        public string Address { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting City.
        /// </summary>
        [Required]
        [StringLength(35, MinimumLength = 1)]
        public string City { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting Province.
        /// </summary>
        [Required]
        [RegularExpression("^(N[BLSTU]|[AMN]B|[BQ]C|ON|PE|SK)$", ErrorMessage = "Please enter a valid Canadian Province!")]
        public string Province { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting PostalCode.
        /// </summary>
        [Required]
        [RegularExpression("[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ] ?[0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]", ErrorMessage = "Please enter a valid postal code!")]
        [DisplayName("PostalCode")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting DateCreated.
        /// </summary>
        [Required]
        [DisplayName("DateCreated")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting GradePointAverage.
        /// </summary>
        [DisplayName("GradePointAverage")]
        [DisplayFormat(DataFormatString = "{0:F}")]
        [Range(0, 4.5)]
        public double? GradePointAverage { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting OutstandingFees.
        /// </summary>
        [Required]
        [DisplayName("OutstandingFees")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double OutstandingFees { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting Notes.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Read-only property that concatenates the first and last name of the student.
        /// </summary>
        [DisplayName("Name")]
        public string FullName
        {
            get
            {
                return String.Format("{0} {1}", FirstName, LastName);
            }
        }

        /// <summary>
        /// Read-only property that concatenates the full address of the student.
        /// </summary>
        [DisplayName("Address")]
        public string FullAddress
        {
            get
            {
                return String.Format("{0}, {1}, {2}, {3}", Address, City, Province, PostalCode);
            }
        }

        /// <summary>
        /// The ChangeState method invokes the StateChangeCheck of the sub classes of GradePointState.
        /// </summary>
        public void ChangeState()
        {
            GradePointState state = db.GradePointStates.Find(GradePointStateId);
            int currentId = state.GradePointStateId;
            int newId = -1;
            while (state.GradePointStateId != newId)
            {
                newId = state.GradePointStateId;
                state.StateChangeCheck(this);
                state = db.GradePointStates.Find(GradePointStateId);
            }
            db.SaveChanges();

        }
        /// <summary>
        /// The SetNextStudentNumber method invokes the NextUniqueNumber of the stored procedure 
        /// to fetch the next available student number
        /// </summary>
        public void SetNextStudentNumber() 
        {
            this.StudentNumber = (long)StoredProcedure.NextUniqueNumber("NextStudent");
        }

        // It shows only 1 relationship.
        public virtual GradePointState GradePointState { get; set; }

        // It shows only 1 relationship.
        public virtual AcademicProgram AcademicProgram { get; set; }

        // It shows 0 to many relationship.
        public virtual ICollection<Registration> Registration { get; set; }

        // It shows 0 to many relationship.
        public virtual ICollection<StudentCard> StudentCard { get; set; }
    }

    /// <summary>
    /// The AcademicProgram class
    /// contains all the methods to describe/set the program that a student is enrolled in.
    /// </summary>
    public class AcademicProgram
    {
        /// <summary>
        /// Auto-implemented property for getting and setting AcademicProgramId.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AcademicProgramId { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting ProgramAcronym.
        /// </summary>
        [Required]
        [DisplayName("Program")]
        public string ProgramAcronym { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting description.
        /// </summary>
        [Required]
        [DisplayName("ProgramName")]
        public string Description { get; set; }

        // It shows 0 to many relationship.
        public virtual ICollection<Student> Student { get; set; }

        // It shows 0 to many relationship.
        public virtual ICollection<Course> Course { get; set; }
    }

    /// <summary>
    /// The Registration class
    /// contains all the methods to register a student.
    /// </summary>
    public class Registration
    {
        /// <summary>
        /// Auto-implemented property for getting and setting RegistrationId.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegistrationId { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting StudentId
        /// </summary>
        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting CourseId.
        /// </summary>
        [Required]
        [ForeignKey("Course")]
        public int CourseId { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting RegistrationNumber.
        /// </summary>
        //[Required]
        [DisplayName("RegistrationNumber")]
        public long RegistrationNumber { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting RegistrationDate.
        /// </summary>
        [Required]
        [DisplayName("RegistrationDate")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting Grade.
        /// </summary>
        [Range(0, 1)]
        [DisplayFormat(NullDisplayText = "Ungraded")]
        public double? Grade { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting Notes.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// The SetNextRegistrationNumber method invokes the NextUniqueNumber of the stored procedure 
        /// to fetch the next available Registration number
        /// </summary>
        public void SetNextRegistrationNumber() 
        {
            this.RegistrationNumber = (long)StoredProcedure.NextUniqueNumber("NextRegistration");
        }

        // It shows only 1 relationship.
        public virtual Student Student { get; set; }

        // It shows only 1 relationship.
        public virtual Course Course { get; set; }
    }

    /// <summary>
    /// The Course class
    /// contains all the methods to provide/set information about courses.
    /// </summary>
    public abstract class Course
    {
        /// <summary>
        /// Auto-implemented property for getting and setting CourseId.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CourseId { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting AcademicProgramId.
        /// </summary>
        [ForeignKey("AcademicProgram")]
        public int? AcademicProgramId { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting CourseNumber.
        /// </summary>
        //[Required]
        [DisplayName("CourseNumber")]
        public string CourseNumber { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting Title.
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting CreditHours.
        /// </summary>
        [Required]
        [DisplayFormat(DataFormatString = "{0:F}")]
        [DisplayName("CreditHours")]
        public double CreditHours { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting TuitionAmount.
        /// </summary>
        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [DisplayName("TuitionAmount")]
        public double TuitionAmount { get; set; }

        /// <summary>
        /// calls the static function to return the type of course.
        /// </summary>
        [DisplayName("CourseType")]
        public string CourseType
        {
            get
            {
                return Utility.BusinessRules.chop(GetType().Name.ToString(), "Course");
            }
        }

        /// <summary>
        /// Auto-implemented property for getting and setting Notes.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// SetNextCourseNumber is an abstract method that is being overridden in the subclasses.
        /// </summary>
        public abstract void SetNextCourseNumber();

        // It shows 0 to 1 relationship.
        public virtual AcademicProgram AcademicProgram { get; set; }

        // It shows 0 to many relationship.
        public virtual ICollection<Registration> Registration { get; set; }
    }

    /// <summary>
    /// The GradedCourse class is the child class of Course class.
    /// </summary>
    public class GradedCourse : Course
    {
        /// <summary>
        /// Auto-implemented property for getting and setting AssignmentWeight.
        /// </summary>
        [Required]
        [DisplayName("AssignmentWeight")]
        [DisplayFormat(DataFormatString = "{0:P}")]
        public double AssignmentWeight { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting MidtermWeight.
        /// </summary>
        [Required]
        [DisplayName("MidtermWeight")]
        [DisplayFormat(DataFormatString = "{0:P}")]
        public double MidtermWeight { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting FinalWeight.
        /// </summary>
        [Required]
        [DisplayName("FinalWeight")]
        [DisplayFormat(DataFormatString = "{0:P}")]
        public double FinalWeight { get; set; }

        /// <summary>
        /// This method adds the prefix in front of the next available unique number and typecasts it into a string.
        /// </summary>
        public override void SetNextCourseNumber() 
        {
            this.CourseNumber = "G-" + StoredProcedure.NextUniqueNumber("NextGradedCourse").ToString();
        }
    }

    /// <summary>
    /// The AuditCourse class is the child class of Course class.
    /// </summary>
    public class AuditCourse : Course 
    {
        /// <summary>
        /// This method adds the prefix in front of the next available unique number and typecasts it into a string.
        /// </summary>
        public override void SetNextCourseNumber() 
        {
            this.CourseNumber = "A-" + StoredProcedure.NextUniqueNumber("NextAuditCourse").ToString(); 
        }
    }

    /// <summary>
    /// The MasteryCourse class is the child class of Course class.
    /// </summary>
    public class MasteryCourse : Course
    {
        /// <summary>
        /// Auto-implemented property to set the maximumAttempts.
        /// </summary>
        [Required]
        [DisplayName("MaximumAttempts")]
        public int MaximumAttempts { get; set; }

        /// <summary>
        /// The SetNextCourseNumber method overrides the parent class method and 
        /// invokes the NextUniqueNumber of the stored procedure 
        /// to fetch the next available Course number
        /// </summary>
        public override void SetNextCourseNumber() 
        {
            this.CourseNumber = "M-" + StoredProcedure.NextUniqueNumber("NextMasteryCourse").ToString();
        } 
    }

    /// <summary>
    /// The StudentCard class contains all the properties and methods related to 
    /// setting up the Student's studentCard.
    /// </summary>
    public class StudentCard
    {
        /// <summary>
        /// Auto-implemented property for getting and setting StudentCardId.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentCardId { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting StudentId.
        /// </summary>
        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting CardNumber.
        /// </summary>
        [Required]
        public long CardNumber { get; set; }

        // It shows only 1 relationship.
        public virtual Student Student { get; set; }
    }

    /// <summary>
    /// The NextUniqueNumber 
    /// </summary>
    public abstract class NextUniqueNumber
    {
        // DataContext Object.
        protected static BITCollege_RSContext db = new BITCollege_RSContext();

        /// <summary>
        /// Auto-implemented property for getting and setting NextUniqueNumberId.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int NextUniqueNumberId { get; set; }

        /// <summary>
        /// Auto-implemented property for getting and setting NextAvailableNumber.
        /// </summary>
        [Required]
        public long NextAvailableNumber { get; set; }
    }

                                                                                                                                  #region NextUniqueNumber's Sub classes. 
    /// <summary>
    /// The NextGradedCourse is a sub class of NextUniqueNumber that contains
    /// all the method to implement the singleton pattern for graded courses.
    /// </summary>
    public class NextGradedCourse : NextUniqueNumber
    {

        private static NextGradedCourse nextGradedCourse;

        /// <summary>
        /// NextGradedCourse is a private constructor of the class that sets the NextAvailableNumber.
        /// </summary>
        private NextGradedCourse() 
        {
            this.NextAvailableNumber = 200000;
        }

        /// <summary>
        /// The NextGradedCourse method is used to implement the singleton pattern.
        /// </summary>
        /// <returns>nextGradedCourse with a row populated into it.</returns>
        public static NextGradedCourse GetInstance()
        {
            if (nextGradedCourse == null)
            {
                nextGradedCourse = db.NextGradedCourses.SingleOrDefault();
                if ( nextGradedCourse == null)
                {
                    nextGradedCourse = new NextGradedCourse();
                    db.NextGradedCourses.Add(nextGradedCourse);
                    db.SaveChanges();
                }
            }
            return nextGradedCourse;
        }
    }

    /// <summary>
    /// The NextStudent Class is the SubClass of NextUniqueNumber that
    /// contains all the method to set the next available number.
    /// </summary>
    public class NextStudent : NextUniqueNumber
    {
        private static NextStudent nextStudent;

        /// <summary>
        /// NextStudent is the private constructor that sets the NextAvailableNumber.
        /// </summary>
        private NextStudent() 
        {
            this.NextAvailableNumber = 20000000;
        }

        /// <summary>
        /// The GetInstance method is used to implement the singleton pattern.
        /// </summary>
        /// <returns>nextStudent populated with a row into it.</returns>
        public static NextStudent GetInstance()
        {
            if ( nextStudent == null )
            {
                nextStudent = db.NextStudents.SingleOrDefault();
                if ( nextStudent == null)
                {
                    nextStudent = new NextStudent();
                    db.NextStudents.Add(nextStudent);
                    db.SaveChanges();
                }
            }
            return nextStudent;
        }
    }

    /// <summary>
    /// The NextAuditCourse Class is the SubClass of NextUniqueNumber that
    /// contains all the method to set the next available number.
    /// </summary>
    public class NextAuditCourse : NextUniqueNumber
    {
        private static NextAuditCourse nextAuditCourse;

        /// <summary>
        /// The NextAuditCourse is the private constructor that sets the NextAvailableNumber.
        /// </summary>
        private NextAuditCourse() 
        {
            this.NextAvailableNumber = 2000;
        }

        /// <summary>
        /// The GetInstance method is used to implement the singleton pattern.
        /// </summary>
        /// <returns>nextAuditCourse is returned with a row populated into it.</returns>
        public static NextAuditCourse GetInstance()
        {
            if (nextAuditCourse == null)
            {
                nextAuditCourse = db.NextAuditCourses.SingleOrDefault();
                if (nextAuditCourse == null)
                {
                    nextAuditCourse = new NextAuditCourse();
                    db.NextAuditCourses.Add(nextAuditCourse);
                    db.SaveChanges();
                }
            }
            return nextAuditCourse;
        }
    }

    /// <summary>
    /// The NextRegistration Class is the SubClass of NextUniqueNumber that
    /// contains all the method to set the next available number.
    /// </summary>
    public class NextRegistration : NextUniqueNumber
    {
        private static NextRegistration nextRegistration;

        /// <summary>
        /// NextRegistration is the private constructor that sets the NextAvailableNumber.
        /// </summary>
        private NextRegistration() 
        {
            this.NextAvailableNumber = 700;
        }

        /// <summary>
        /// The GetInstance method is used to implement the singleton pattern.
        /// </summary>
        /// <returns>nextRegistration</returns>
        public static NextRegistration GetInstance()
        {
            if (nextRegistration == null)
            {
                nextRegistration = db.NextRegistrations.SingleOrDefault();
                if (nextRegistration == null)
                {
                    nextRegistration = new NextRegistration();
                    db.NextRegistrations.Add(nextRegistration);
                    db.SaveChanges();
                }
            }
            return nextRegistration;
        }
    }

    /// <summary>
    /// The NextMasteryCourse Class is the SubClass of NextUniqueNumber that
    /// contains all the method to set the next available number.
    /// </summary>
    public class NextMasteryCourse : NextUniqueNumber
    {
        private static NextMasteryCourse nextMasteryCourse;

        /// <summary>
        /// NextMasteryCourse is the private constructor that sets the NextAvailableNumber.
        /// </summary>
        private NextMasteryCourse() 
        {
            this.NextAvailableNumber = 20000;
        }

        /// <summary>
        /// The GetInstance method is used to implement the singleton pattern.
        /// </summary>
        /// <returns>nextMasteryCourse</returns>
        public static NextMasteryCourse GetInstance()
        {
            if (nextMasteryCourse == null)
            {
                nextMasteryCourse = db.NextMasteryCourses.SingleOrDefault();
                if (nextMasteryCourse == null)
                {
                    nextMasteryCourse = new NextMasteryCourse();
                    db.NextMasteryCourses.Add(nextMasteryCourse);
                    db.SaveChanges();
                }
            }
            return nextMasteryCourse;
        }
    }
    #endregion

    /// <summary>
    /// StoredProcedure class contains a method to call the stored procedure.
    /// </summary>
    public static class StoredProcedure
    {

        /// <summary>
        /// The NextUniqueNumber is used to call the stored procedure and return the next available number.
        /// </summary>
        /// <param name="discriminator">the name of the class is passed.</param>
        /// <returns>returnValue</returns>
        public static long? NextUniqueNumber(string discriminator)
        {
            try
            {
                SqlConnection connection = new SqlConnection("Data Source=localhost; " +
                                           "Initial Catalog=BITCollege_RSContext;Integrated Security=True");
                long? returnValue = 0;
                SqlCommand storedProcedure = new SqlCommand("next_number", connection);
                storedProcedure.CommandType = CommandType.StoredProcedure;
                storedProcedure.Parameters.AddWithValue("@Discriminator", discriminator);
                // StoredProcedure(discriminator)
                SqlParameter outputParameter = new SqlParameter("@NewVal", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.Output
                };
                storedProcedure.Parameters.Add(outputParameter);
                connection.Open();
                storedProcedure.ExecuteNonQuery();
                connection.Close();
                returnValue = (long?)outputParameter.Value;
                return returnValue;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}