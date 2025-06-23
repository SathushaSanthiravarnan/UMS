using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_Tic_Management_System.Models
{
    internal class GroupMentor
    {
        public int SubGroupId { get; set; } 
        public int MentorId { get; set; } 
        public DateTime? AssignedDate { get; set; }
    }
}
