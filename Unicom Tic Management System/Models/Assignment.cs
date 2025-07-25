﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_Tic_Management_System.Models
{
    internal class Assignment
    {
        public int AssignmentId { get; set; }
        public int SubjectId { get; set; } 
        public int LecturerId { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; } 
        public int? MaxMarks { get; set; }
    }
}
