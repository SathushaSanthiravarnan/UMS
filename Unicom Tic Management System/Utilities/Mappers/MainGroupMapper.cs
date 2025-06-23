using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.Grouping_MentoringDTOs;

namespace Unicom_Tic_Management_System.Utilities.Mappers
{
    internal static class MainGroupMapper
    {
        public static MainGroupDto ToDTO(MainGroup mainGroup)
        {
            if (mainGroup == null) return null;
            return new MainGroupDto
            {
                MainGroupId = mainGroup.MainGroupId,
                GroupCode = mainGroup.GroupCode,
                Description = mainGroup.Description
            };
        }

        public static MainGroup ToEntity(MainGroupDto mainGroupDto)
        {
            if (mainGroupDto == null) return null;
            return new MainGroup
            {
                MainGroupId = mainGroupDto.MainGroupId,
                GroupCode = mainGroupDto.GroupCode,
                Description = mainGroupDto.Description
            };
        }

        public static List<MainGroupDto> ToDTOList(IEnumerable<MainGroup> mainGroups)
        {
            return mainGroups?.Select(mg => ToDTO(mg)).ToList() ?? new List<MainGroupDto>();
        }

        public static List<MainGroup> ToEntityList(IEnumerable<MainGroupDto> mainGroupDtos)
        {
            return mainGroupDtos?.Select(mg => ToEntity(mg)).ToList() ?? new List<MainGroup>();
        }
    }
}
