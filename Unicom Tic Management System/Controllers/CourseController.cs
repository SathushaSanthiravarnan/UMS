using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_Tic_Management_System.Models.DTOs.AcademicDTOs;
using Unicom_Tic_Management_System.Repositories;
using Unicom_Tic_Management_System.Services;
using Unicom_Tic_Management_System.Services.Interfaces;

namespace Unicom_Tic_Management_System.Controllers
{
    internal class CourseController
    {
        private readonly ICourseService _service;

        public CourseController()
        {
            _service = new CourseService(new CourseRepository());
        }

        public void AddCourse(CourseDto courseDto)
        {
            _service.AddCourse(courseDto);
        }

        public void UpdateCourse(CourseDto courseDto)
        {
            _service.UpdateCourse(courseDto);
        }

        public void DeleteCourse(int courseId)
        {
            _service.DeleteCourse(courseId);
        }

        public CourseDto GetCourseById(int courseId)
        {
            return _service.GetCourseById(courseId);
        }

        public CourseDto GetCourseByName(string courseName)
        {
            return _service.GetCourseByName(courseName);
        }

        public List<CourseDto> GetAllCourses()
        {
            return _service.GetAllCourses();
        }
    }
}

