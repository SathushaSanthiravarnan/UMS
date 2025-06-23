using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_Tic_Management_System.Models.DTOs.Grouping_MentoringDTOs
{
    internal class SubGroupDto
    {
        public int SubGroupId { get; set; }
        public int MainGroupId { get; set; }
        public string SubGroupName { get; set; }
        public string Description { get; set; }
    }
}
