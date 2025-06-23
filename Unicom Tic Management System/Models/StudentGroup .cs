using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_Tic_Management_System.Models
{
    internal class StudentGroup
    {
        public int StudentId { get; set; } 
        public int SubGroupId { get; set; }
        public DateTime? AssignedDate { get; set; }
    }
}
