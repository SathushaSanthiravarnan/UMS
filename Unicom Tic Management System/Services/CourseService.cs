using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.AcademicDTOs;
using Unicom_Tic_Management_System.Repositories.Interfaces;
using Unicom_Tic_Management_System.Services.Interfaces;
using Unicom_Tic_Management_System.Utilities.Mappers;

namespace Unicom_Tic_Management_System.Services
{
    internal class CourseService : ICourseService
    {
        private readonly ICourseRepository _repository;

        public CourseService(ICourseRepository repository)
        {
            _repository = repository;
        }

        public void AddCourse(CourseDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.CourseName))
                throw new ArgumentException("Course name is required");

            var course = CourseMapper.ToEntity(dto);
            _repository.AddCourse(course);
        }

        public void UpdateCourse(CourseDto dto)
        {
            if (dto.CourseId <= 0)
                throw new ArgumentException("Valid Course ID required");

            var course = CourseMapper.ToEntity(dto);
            _repository.UpdateCourse(course);
        }

        public void DeleteCourse(int courseId)
        {
            _repository.DeleteCourse(courseId);
        }

        public CourseDto GetCourseById(int courseId)
        {
            var course = _repository.GetCourseById(courseId);
            return CourseMapper.ToDTO(course);
        }

        public CourseDto GetCourseByName(string courseName)
        {
            var course = _repository.GetCourseByName(courseName);
            return CourseMapper.ToDTO(course);
        }

        public List<CourseDto> GetAllCourses()
        {
            var list = _repository.GetAllCourses();
            return CourseMapper.ToDTOList(list);
        }
    }
}
