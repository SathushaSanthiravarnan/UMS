using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_Tic_Management_System.Models
{
    internal class LecturerStudent
    {
        public int LecturerId { get; set; } 
        public int StudentId { get; set; } 
        public DateTime AssignedDate { get; set; }
        public string RelationshipType { get; set; }

    }
}
