using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using BITCollege_RS.Data;
using BITCollege_RS.Models;

namespace BITCollegeWindows
{
    class BatchProcess
    {
        private string inputFileName;
        private string logFileName;
        private string logData;
        BITCollege_RSContext db = new BITCollege_RSContext();
        
        /// <summary>
        /// Checks and finds the records that are excluded and outputs them onto the log file
        /// </summary>
        /// <param name="beforeQuery">base query of next query</param>
        /// <param name="afterQuery">the next query</param>
        /// <param name="message">The error message</param>
        private void ProcessErrors( IEnumerable<XElement> beforeQuery, IEnumerable<XElement> afterQuery, string message)
        {
            IEnumerable<XElement> failedRecords = beforeQuery.Except(afterQuery);

            foreach (XElement record in failedRecords)
            {
                this.logData += "\r\n ------ Error------";
                this.logData += "\r\n File: " + record.Element("inputFileName");
                this.logData += "\r\n Program: " + record.Element("program");
                this.logData += "\r\n Student Number: " + record.Element("student_no");
                this.logData += "\r\n Course Number: " + record.Element("course_no");
                this.logData += "\r\n Registration Number: " + record.Element("registration_no");
                this.logData += "\r\n Type: " + record.Element("type");
                this.logData += "\r\n Grade: " + record.Element("grade");
                this.logData += "\r\n Notes: " + record.Element("notes");
                this.logData += "\r\n Nodes: " + record.Elements().Nodes().Count();
                this.logData += "\r\n " + message;
                this.logData += "\r\n ------------------";
            }
        }

        /// <summary>
        /// Method checks for the attributes in the file.
        /// </summary>
        private void ProcessHeader()
        {
            XDocument xDocument = XDocument.Load(this.inputFileName);
            XElement rootNode = xDocument.Element("student_update");
            IEnumerable<XAttribute> xAttributeList = rootNode.Attributes();
            if(xAttributeList.Count() != 3)
            {
                throw new System.ArgumentException(
                                                        "The root element contains invalid number of arguments." +
                                                        "\n Must contain following arguments only: \n" +
                                                        "\t 1) date \n" +
                                                        "\t 2) program \n" +
                                                        "\t 3) CheckSum \n"
                                                   );
            }
            else
            {
                string date = rootNode.Attribute("date").Value;
                string program = rootNode.Attribute("program").Value;
                int checkSum = int.Parse(rootNode.Attribute("checksum").Value);
                if (date != DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    throw new System.ArgumentException(
                                                            "invalid value entered into the date argument \n" +
                                                            "Found: " +
                                                            date +
                                                            "instead of today\'s date, " +
                                                            DateTime.Now.ToString("yyyy-MM-dd")
                                                      );
                }
                
                IEnumerable<string> entireProgramAcronyms = db.AcademicPrograms.Select(x=>x.ProgramAcronym).ToList();
                
                if (!entireProgramAcronyms.Contains(program))
                {
                    throw new Exception("invalid program name!");
                }
                int sum = 0;
                IEnumerable<XElement> studentNumbers = rootNode.Descendants("student_no");
                foreach(XElement studentNum in studentNumbers)
                {
                    sum += int.Parse(studentNum.Value);
                }
                if(sum != checkSum)
                {
                    throw new System.ArgumentException("checksum is incorrect.");
                }
            }

        }
        /// <summary>
        /// validates the xml file and excludes the erroneous records to obtain all the valid ones.
        /// </summary>
        private void ProcessDetails()
        {
            XDocument xDocument = XDocument.Load(this.inputFileName);

            // Round 1
            IEnumerable<XElement> transactions = xDocument.Descendants("student_update");

            IEnumerable<XElement> transactionsWithValidChildren = transactions
                                                                  .Where(x=> x.Elements().Nodes().Count()==7);

            ProcessErrors(transactions,transactionsWithValidChildren,"The record contains incorrect number of child elements, must include 7 elements.");

            // Round 2
            IEnumerable<XElement> transactionsWithValidProgram = transactionsWithValidChildren.Where(x => x.Element("program").Value == x.Element("student_update").Attribute("program").Value);

            ProcessErrors(transactionsWithValidChildren, transactionsWithValidProgram, "The record contains incorrect program name in elements.");

            // Round 3
            IEnumerable<XElement> transactionsWithValidType = transactionsWithValidChildren.Where(x => Utility.Numeric.IsNumeric(x.Element("type").Value, System.Globalization.NumberStyles.Integer));

            ProcessErrors(transactionsWithValidProgram, transactionsWithValidType, "The record contains invalid value in the element \'Type\'");

            // Round 4
            IEnumerable<XElement> transactionWithValidGrade = transactionsWithValidType.Where(x => x.Element("grade").Value == "*" || Utility.Numeric.IsNumeric(x.Element("grade").Value, System.Globalization.NumberStyles.Float));

            ProcessErrors(transactionsWithValidType, transactionWithValidGrade, "The record contains invalid value in the element \'Grade\', must include a \'*\' or grades. ");

            // Round 5
            IEnumerable<XElement> transactionWithCorrectType = transactionWithValidGrade.Where(x => int.Parse(x.Element("type").Value) == 1 || int.Parse(x.Element("type").Value) == 2);

            ProcessErrors(transactionWithValidGrade, transactionWithCorrectType, "The type should be either 1 or 2.");

            // Round 6
            IEnumerable<XElement> transactionWithFilteredGrade = transactionWithCorrectType.Where(x => 
                                                                                                    (x.Element("grade").Value == "*" && int.Parse(x.Element("type").Value) == 1) ||
                                                                                                    (int.Parse(x.Element("type").Value) == 2 && 
                                                                                                        int.Parse(x.Element("grade").Value) >= 0 && 
                                                                                                        int.Parse(x.Element("grade").Value) <= 100
                                                                                                    )
                                                                                            );
            ProcessErrors(transactionWithCorrectType, transactionWithFilteredGrade, "if the grade is '*' it must have a type of 1 whereas if the type is 2 grade should range between 0 and 100");

            // Round 7
            IEnumerable<long> student_numbers = db.Students.Select(x => x.StudentNumber).ToList();

            IEnumerable<XElement> studentNumberValid = transactionWithFilteredGrade.Where(x => student_numbers.Contains(long.Parse(x.Element("student_no").Value)));

            ProcessErrors(transactionWithFilteredGrade, studentNumberValid, "The records were rejected as the student number does not exist");

            // Round 8
            IEnumerable<string> CourseNumbers = db.Courses.Select(x => x.CourseNumber).ToList();

            IEnumerable<XElement> transactionsWithValidCourse = studentNumberValid.Where(x => int.Parse(x.Element("type").Value)==2 && (x.Element("course_no").Value == "*") || CourseNumbers.Contains(x.Element("course_no").Value));

            ProcessErrors(studentNumberValid, transactionsWithValidCourse, "The record contains a wrong value in the course number");

            // Round 9
            IEnumerable<long> registrationNumbers = db.Registrations.Select(x => x.RegistrationNumber).ToList();

            IEnumerable<XElement> transactionsWithValidRegNum = transactionsWithValidCourse.Where(x => int.Parse(x.Element("type").Value) == 1 && (x.Element("registraion_no").Value == "*") || registrationNumbers.Contains(long.Parse(x.Element("registration_no").Value)));

            ProcessErrors(transactionsWithValidCourse, transactionsWithValidRegNum, "The record contains a wrong value in the Registration number element.");


            ProcessTransactions(transactionsWithValidRegNum);
                
        }
        /// <summary>
        /// updates the data into the data base.
        /// </summary>
        /// <param name="transactionRecords">collection of records with valid data</param>
        private void ProcessTransactions(IEnumerable<XElement> transactionRecords)
        {
            WindowsWCFService.CollegeRegistrationClient wcf = new WindowsWCFService.CollegeRegistrationClient();
            foreach(XElement transaction in transactionRecords)
            {
                int valid = 0;
                if(int.Parse(transaction.Element("type").Value) == 1)
                {
                    int studentId = db.Students
                                        .Where(x => x.StudentNumber == long.Parse(transaction.Element("student_no").Value))
                                        .Select(x => x.StudentId).SingleOrDefault();
                    int courseId = db.Courses
                                    .Where(x => x.CourseNumber == transaction.Element("course_no").Value)
                                    .Select(x => x.CourseId).SingleOrDefault();
                    valid = wcf.RegisterCourse(studentId, courseId, transaction.Element("notes").Value);

                    if (valid == -300)
                    {
                        logData += "ERROR: Unknown exception occurred.";
                    }
                    if (valid == -200)
                    {
                        logData += "ERROR: the student has exceeded the MaximumAttempts of a Mastery course.";
                    }
                    if (valid == -100)
                    {
                        logData += "ERROR: the student already has an ungraded registration for this course.";
                    }
                }
                if(int.Parse(transaction.Element("type").Value) == 2){
                    double grade = double.Parse(transaction.Element("grade").Value)/100;

                    int regId = db.Registrations.Where(x => x.RegistrationNumber == int.Parse(transaction.Element("registration_no").Value))
                                                .Select(x=>x.RegistrationId).SingleOrDefault();

                    wcf.UpdateGrade(grade, regId, transaction.Element("notes").Value);
                    valid = 2;
                }
                if (valid == 100)
                {
                    logData += "Successful Registration student " +
                                transaction.Element("registration_no").Value +
                                " for Course " +
                                transaction.Element("course_no").Value;
                }
                if (valid == 2)
                {
                    logData += "Grade " +
                                transaction.Element("grade").Value +
                                " applied to student " +
                                transaction.Element("student_no").Value;
                }
                if (valid == 0)
                {
                    logData += "Successful Registration student " +
                                transaction.Element("registration_no").Value +
                                " for Course " +
                                transaction.Element("course_no").Value;
                }
            }
            WriteLogData();
        }

        /// <summary>
        /// generates the log data file and appends data into it.
        /// </summary>
        /// <returns>logData</returns>
        public string WriteLogData()
        {
            string fileName = "COMPLETE-" + inputFileName;

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            if (File.Exists(inputFileName))
            {
                File.Move(inputFileName, fileName);
            }

            StreamWriter logFile = new StreamWriter(logFileName, true);

            logFile.Write(logData);
            logFile.Close();

            string temp = this.logData;
            this.logData = string.Empty;

            return temp;
        }

        /// <summary>
        /// checks for the file name in the bin folder.
        /// </summary>
        /// <param name="programAcronym">program acronym</param>
        /// <param name="key"></param>
        public void ProcessTransmission(string programAcronym, string key)
        {
            this.inputFileName = DateTime.Now.Year.ToString()
                               + "-"
                               + DateTime.Now.DayOfYear.ToString()
                               + "-"
                               + programAcronym
                               + ".xml";

            this.logFileName = "LOG"
                             + inputFileName.Substring(0, inputFileName.Length - 4)
                             + ".txt";


            if (File.Exists(inputFileName))
            {
                try
                {
                    this.ProcessHeader();
                    this.ProcessDetails();
                }
                catch (Exception e) 
                {
                    this.logData += this.logData
                              + "\n ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ \n"
                              + "\t ERROR: Could not process file "
                              + inputFileName
                              + "\n ---------------------------------------------------------------------------- \n"
                              + "Caught an exception: " + e.Message.ToString()
                              + "\n ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ \n";
                }
            }
            else
            {
                this.logData += this.logData
                              + "\n ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ \n"
                              + "\t ERROR: File "
                              + inputFileName
                              + " does not exist in the bin folder."
                              + "\n ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ \n";
            }

        }
    }
}
