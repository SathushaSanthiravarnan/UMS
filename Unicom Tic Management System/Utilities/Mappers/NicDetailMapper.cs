using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.UserDtos;

namespace Unicom_Tic_Management_System.Utilities.Mappers
{

    namespace UnicomTIC_Management.Utilities
    {
        internal static class NicDetailMapper
        {
            public static NicDetailDTO ToDTO(NicDetail model)
            {
                if (model == null) return null;

                return new NicDetailDTO
                {
                    Nic = model.Nic,
                    IsUsed = model.IsUsed
                };
            }

            public static NicDetail ToEntity(NicDetailDTO dto)
            {
                if (dto == null) return null;

                return new NicDetail
                {
                    Nic = dto.Nic,
                    IsUsed = dto.IsUsed
                };
            }

            public static List<NicDetailDTO> ToDTOList(List<NicDetail> list)
            {
                return list?.Select(x => ToDTO(x)).ToList() ?? new List<NicDetailDTO>();
            }

            public static List<NicDetail> ToEntityList(List<NicDetailDTO> list)
            {
                return list?.Select(x => ToEntity(x)).ToList() ?? new List<NicDetail>();
            }
        }
    }
}

