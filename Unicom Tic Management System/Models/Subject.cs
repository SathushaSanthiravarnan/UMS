using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_Tic_Management_System.Models
{
    internal class Subject
    {
        public int SubjectId { get; set; } 
        public string SubjectName { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
