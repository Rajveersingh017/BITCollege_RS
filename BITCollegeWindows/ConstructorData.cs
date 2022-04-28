using BITCollege_RS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITCollegeWindows
{
    /// <summary>
    /// given:TO BE MODIFIED
    /// this class is used to capture data to be passed
    /// among the windows forms
    /// </summary>
    public class ConstructorData
    {
        public Student StudentSpecificData { get; set; }
        public Registration RegistrationSpecificData { get; set; }
    }
}
