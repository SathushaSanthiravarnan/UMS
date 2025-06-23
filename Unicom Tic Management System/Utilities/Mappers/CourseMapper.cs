using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.AcademicDTOs;

namespace Unicom_Tic_Management_System.Utilities.Mappers
{
    internal static class CourseMapper
    {
        
        public static CourseDto ToDTO(Course course)
        {
            if (course == null) return null;
            return new CourseDto
            {
                CourseId = course.CourseId,
                CourseName = course.CourseName,
                Description = course.Description
            };
        }

    
        public static Course ToEntity(CourseDto courseDto)
        {
            if (courseDto == null) return null;
            return new Course
            {
                CourseId = courseDto.CourseId,
                CourseName = courseDto.CourseName,
                Description = courseDto.Description
            };
        }

        
        public static List<CourseDto> ToDTOList(IEnumerable<Course> courses)
        {
            return courses?.Select(c => ToDTO(c)).ToList() ?? new List<CourseDto>();
        }

        
        public static List<Course> ToEntityList(IEnumerable<CourseDto> courseDtos)
        {
            return courseDtos?.Select(c => ToEntity(c)).ToList() ?? new List<Course>();
        }
    }
}
