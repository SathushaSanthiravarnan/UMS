using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.Grouping_MentoringDTOs;

namespace Unicom_Tic_Management_System.Utilities.Mappers
{
    internal static class SubGroupMapper
    {
        public static SubGroupDto ToDTO(SubGroup subGroup)
        {
            if (subGroup == null) return null;
            return new SubGroupDto
            {
                SubGroupId = subGroup.SubGroupId,
                MainGroupId = subGroup.MainGroupId,
                SubGroupName = subGroup.SubGroupName,
                Description = subGroup.Description
            };
        }

        public static SubGroup ToEntity(SubGroupDto subGroupDto)
        {
            if (subGroupDto == null) return null;
            return new SubGroup
            {
                SubGroupId = subGroupDto.SubGroupId,
                MainGroupId = subGroupDto.MainGroupId,
                SubGroupName = subGroupDto.SubGroupName,
                Description = subGroupDto.Description
            };
        }

        public static List<SubGroupDto> ToDTOList(IEnumerable<SubGroup> subGroups)
        {
            return subGroups?.Select(sg => ToDTO(sg)).ToList() ?? new List<SubGroupDto>();
        }

        public static List<SubGroup> ToEntityList(IEnumerable<SubGroupDto> subGroupDtos)
        {
            return subGroupDtos?.Select(sg => ToEntity(sg)).ToList() ?? new List<SubGroup>();
        }
    }
}
