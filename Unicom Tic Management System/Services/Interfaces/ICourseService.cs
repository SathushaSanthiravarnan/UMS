using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.AcademicDTOs;

namespace Unicom_Tic_Management_System.Services.Interfaces
{
    internal interface ICourseService
    {
        void AddCourse(CourseDto courseDto);
        void UpdateCourse(CourseDto courseDto);
        void DeleteCourse(int courseId);
        CourseDto GetCourseById(int courseId);
        CourseDto GetCourseByName(string courseName);
        List<CourseDto> GetAllCourses();
    }
}
