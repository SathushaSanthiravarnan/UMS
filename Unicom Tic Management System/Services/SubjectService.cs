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
    internal class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _repository;

        public SubjectService(ISubjectRepository repository)
        {
            _repository = repository;
        }

        public void AddSubject(SubjectDto subjectDto)
        {
            if (subjectDto == null)
                throw new ArgumentNullException(nameof(subjectDto));

           
            var existing = _repository.GetSubjectByNameAndCourse(subjectDto.SubjectName, subjectDto.CourseId);
            if (existing != null)
                throw new Exception("This subject already exists under the selected course.");

            var subject = SubjectMapper.ToEntity(subjectDto);
            _repository.AddSubject(subject);
        }

        public void UpdateSubject(SubjectDto subjectDto)
        {
            if (subjectDto == null)
                throw new ArgumentNullException(nameof(subjectDto));

            var existing = _repository.GetSubjectById(subjectDto.SubjectId);
            if (existing == null)
                throw new Exception("Subject not found.");

            var subject = SubjectMapper.ToEntity(subjectDto);
            _repository.UpdateSubject(subject);
        }

        public void DeleteSubject(int subjectId)
        {
            var subject = _repository.GetSubjectById(subjectId);
            if (subject == null)
                throw new Exception("Subject not found.");

            _repository.DeleteSubject(subjectId);
        }

        public SubjectDto GetSubjectById(int subjectId)
        {
            var subject = _repository.GetSubjectById(subjectId);
            return subject != null ? SubjectMapper.ToDTO(subject) : null;
        }

        public SubjectDto GetSubjectByName(string subjectName)
        {

            var allSubjects = _repository.GetAllSubjects();
            var match = allSubjects.Find(s => s.SubjectName.Equals(subjectName, StringComparison.OrdinalIgnoreCase));
            return match != null ? SubjectMapper.ToDTO(match) : null;
        }

        public List<SubjectDto> GetSubjectsByCourse(int courseId)
        {
            var subjects = _repository.GetSubjectsByCourseId(courseId);
            return SubjectMapper.ToDTOList(subjects);
        }

        public List<SubjectDto> GetAllSubjects()
        {
            var subjects = _repository.GetAllSubjects();
            return SubjectMapper.ToDTOList(subjects);
        }
    }
}

