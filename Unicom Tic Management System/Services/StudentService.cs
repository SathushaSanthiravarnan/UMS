using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.Grouping_MentoringDTOs;
using Unicom_Tic_Management_System.Models.Enums;
using Unicom_Tic_Management_System.Repositories.Interfaces;
using Unicom_Tic_Management_System.Services.Interfaces;
using Unicom_Tic_Management_System.Utilities.Mappers;

namespace Unicom_Tic_Management_System.Services
{
    internal class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public StudentDto GetStudentById(int studentId)
        {
            var student = _repository.GetStudentById(studentId);
            return student != null ? StudentMapper.ToDTO(student) : null;
        }

        public StudentDto GetStudentByNic(string nic)
        {
            var student = _repository.GetStudentByNic(nic);
            return student != null ? StudentMapper.ToDTO(student) : null;
        }

        public StudentDto GetStudentByAdmissionNumber(string admissionNumber)
        {
            var student = _repository.GetStudentByAdmissionNumber(admissionNumber);
            return student != null ? StudentMapper.ToDTO(student) : null;
        }

        public StudentDto GetStudentByEmail(string email)
        {
            var student = _repository.GetStudentByEmail(email);
            return student != null ? StudentMapper.ToDTO(student) : null;
        }

        public StudentDto GetStudentByUserId(int userId)
        {
            var student = _repository.GetStudentByUserId(userId);
            return student != null ? StudentMapper.ToDTO(student) : null;
        }

        public List<StudentDto> GetAllStudents()
        {
            var students = _repository.GetAllStudents();
            return StudentMapper.ToDTOList(students);
        }

        public List<StudentDto> GetStudentsByCourseId(int courseId)
        {
            var students = _repository.GetStudentsByCourseId(courseId);
            return StudentMapper.ToDTOList(students);
        }

        public List<StudentDto> GetStudentsByMainGroupId(int mainGroupId)
        {
            var students = _repository.GetStudentsByMainGroupId(mainGroupId);
            return StudentMapper.ToDTOList(students);
        }

        public List<StudentDto> GetStudentsBySubGroupId(int subGroupId)
        {
            var students = _repository.GetStudentsBySubGroupId(subGroupId);
            return StudentMapper.ToDTOList(students);
        }

        public List<StudentDto> GetStudentsByGender(GenderType gender)
        {
            var students = _repository.GetStudentsByGender((GenderType)gender);
            return StudentMapper.ToDTOList(students);
        }

        public void AddStudent(StudentDto studentDto)
        {
            if (studentDto == null)
                throw new ArgumentNullException(nameof(studentDto));

            var student = StudentMapper.ToEntity(studentDto);
            _repository.AddStudent(student);
        }

        public void UpdateStudent(StudentDto studentDto)
        {
            if (studentDto == null)
                throw new ArgumentNullException(nameof(studentDto));

            var student = StudentMapper.ToEntity(studentDto);
            _repository.UpdateStudent(student);
        }

        public void DeleteStudent(int studentId)
        {
            _repository.DeleteStudent(studentId);
        }
    }
}
