using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.Grouping_MentoringDTOs;
using Unicom_Tic_Management_System.Models.Enums;

namespace Unicom_Tic_Management_System.Utilities.Mappers
{
    internal static class StudentMapper
    {
        public static StudentDto ToDTO(Student student)
        {
            if (student == null) return null;
            return new StudentDto
            {
                StudentId = student.StudentId,
                Name = student.Name,
                Nic = student.Nic,
                Address = student.Address,
                ContactNo = student.ContactNo,
                Email = student.Email,
                DateOfBirth = student.DateOfBirth,
                Gender = (GenderType)student.Gender, 
                EnrollmentDate = student.EnrollmentDate,
                CourseId = student.CourseId,
                UserId = student.UserId,
                MainGroupId = student.MainGroupId,
                SubGroupId = student.SubGroupId,
                AdmissionNumber = student.AdmissionNumber
            };
        }

        public static Student ToEntity(StudentDto studentDto)
        {
            if (studentDto == null) return null;
            return new Student
            {
                StudentId = studentDto.StudentId,
                Name = studentDto.Name,
                Nic = studentDto.Nic,
                Address = studentDto.Address,
                ContactNo = studentDto.ContactNo,
                Email = studentDto.Email,
                DateOfBirth = studentDto.DateOfBirth,
                Gender = (GenderType)studentDto.Gender, 
                EnrollmentDate = studentDto.EnrollmentDate,
                CourseId = studentDto.CourseId,
                UserId = studentDto.UserId,
                MainGroupId = studentDto.MainGroupId,
                SubGroupId = studentDto.SubGroupId,
                AdmissionNumber = studentDto.AdmissionNumber
            };
        }

        public static List<StudentDto> ToDTOList(IEnumerable<Student> students)
        {
            return students?.Select(s => ToDTO(s)).ToList() ?? new List<StudentDto>();
        }

        public static List<Student> ToEntityList(IEnumerable<StudentDto> studentDtos)
        {
            return studentDtos?.Select(s => ToEntity(s)).ToList() ?? new List<Student>();
        }
    }

}
