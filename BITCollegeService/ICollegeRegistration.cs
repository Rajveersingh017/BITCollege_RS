using BITCollege_RS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BITCollegeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICollegeRegistration" in both code and config file together.
    [ServiceContract]
    public interface ICollegeRegistration
    {
        [OperationContract]
        void DoWork();

        /// <summary>
        /// The DropCourse method finds and deletes the registrationId.
        /// </summary>
        /// <param name="registrationId">Registration Id from the database is passed in the method.</param>
        /// <returns>returns true if the delete is successful.</returns>
        [OperationContract]
        bool DropCourse(int registrationId);

        /// <summary>
        /// the RegisterCourse method validates and registers the course.
        /// </summary>
        /// <param name="studentId">Student Id is passed through studentId.</param>
        /// <param name="courseId">Course Id is passed through courseId.</param>
        /// <param name="notes">Notes is passed through notes.</param>
        /// <returns>Returns Response variable with a specific error code.</returns>
        [OperationContract]
        int RegisterCourse(int studentId, int courseId, string notes);
        
        /// <summary>
        /// The UpdateGrade method takes the arguments to update the registration record and saves the changes to the database. 
        /// </summary>
        /// <param name="grade">New grades are passed through grade.</param>
        /// <param name="registrationId">New Registration is being passed through registrationId.</param>
        /// <param name="notes">New notes are being passed through notes.</param>
        [OperationContract]
        void UpdateGrade(double grade, int registrationId, string notes);
    

    }
}
