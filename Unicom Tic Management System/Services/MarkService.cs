using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.AcademicOperationsDTOs;
using Unicom_Tic_Management_System.Repositories.Interfaces;
using Unicom_Tic_Management_System.Services.Interfaces;

namespace Unicom_Tic_Management_System.Services
{
    internal class MarkService : IMarkService
    {
        private readonly IMarkRepository _markRepository;
        private readonly IStudentRepository _studentRepository; // Assuming you have this for getting student details
        private readonly ISubjectRepository _subjectRepository; // Assuming you have this for getting subject details
        private readonly IExamRepository _examRepository;     // Assuming you have this for getting exam details
        private readonly ILecturerRepository _lecturerRepository; // Assuming you have this for getting lecturer details

        // Constructor Injection for dependencies
        public MarkService(IMarkRepository markRepository,
                           IStudentRepository studentRepository,
                           ISubjectRepository subjectRepository,
                           IExamRepository examRepository,
                           ILecturerRepository lecturerRepository)
        {
            _markRepository = markRepository ?? throw new ArgumentNullException(nameof(markRepository));
            _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
            _subjectRepository = subjectRepository ?? throw new ArgumentNullException(nameof(subjectRepository));
            _examRepository = examRepository ?? throw new ArgumentNullException(nameof(examRepository));
            _lecturerRepository = lecturerRepository ?? throw new ArgumentNullException(nameof(lecturerRepository));
        }

        // Helper method to convert Mark entity to MarkDisplayDto
        private MarkDisplayDto MapMarkToMarkDisplayDto(Mark mark)
        {
            if (mark == null) return null;

            // Fetch related details from other repositories if needed,
            // though GetAllMarksWithDetails in repository already does this via JOINs.
            // This is for methods like GetMarkDisplayById which gets a single Mark entity.
            var student = _studentRepository.GetStudentById(mark.StudentId);
            var subject = _subjectRepository.GetSubjectById(mark.SubjectId);
            var exam = _examRepository.GetExamById(mark.ExamId);
            var lecturer = mark.GradedByLecturerId.HasValue ? _lecturerRepository.GetLecturerById(mark.GradedByLecturerId.Value) : null;

            return new MarkDisplayDto
            {
                MarkId = mark.MarkId,
                StudentId = mark.StudentId,
                StudentAdmissionNumber = student?.AdmissionNumber,
                StudentName = student != null ? $"{student.Name}" : "Unknown Student",
                SubjectId = mark.SubjectId,
                SubjectName = subject?.SubjectName,
                ExamId = mark.ExamId,
                
                MarksObtained = mark.MarksObtained,
                GradedByLecturerId = mark.GradedByLecturerId,
                GradedByLecturerName = lecturer.Name,
                Grade = mark.Grade, 
                EntryDate = mark.EntryDate
            };
        }


       
        public void AddMark(MarkDto markDto)
        {
            if (markDto == null)
                throw new ArgumentNullException(nameof(markDto));

           
            if (markDto.StudentId <= 0 || markDto.SubjectId <= 0 || markDto.ExamId <= 0)
            {
                throw new ArgumentException("Student, Subject, and Exam IDs must be valid.");
            }
            if (markDto.MarksObtained < 0 || markDto.MarksObtained > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(markDto.MarksObtained), "Marks must be between 0 and 100.");
            }

           
            markDto.Grade = CalculateGrade(markDto.MarksObtained);

            
            var mark = new Mark
            {
               
                StudentId = markDto.StudentId,
                SubjectId = markDto.SubjectId,
                ExamId = markDto.ExamId,
                MarksObtained = markDto.MarksObtained,
                GradedByLecturerId = markDto.GradedByLecturerId,
                Grade = markDto.Grade,
                EntryDate = markDto.EntryDate 
            };

            _markRepository.AddMark(mark);
        }

        public void UpdateMark(MarkDto markDto)
        {
            if (markDto == null)
                throw new ArgumentNullException(nameof(markDto));
            if (markDto.MarkId <= 0)
                throw new ArgumentException("Mark ID must be valid for update.");

            
            if (markDto.StudentId <= 0 || markDto.SubjectId <= 0 || markDto.ExamId <= 0)
            {
                throw new ArgumentException("Student, Subject, and Exam IDs must be valid.");
            }
            if (markDto.MarksObtained < 0 || markDto.MarksObtained > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(markDto.MarksObtained), "Marks must be between 0 and 100.");
            }

           
            markDto.Grade = CalculateGrade(markDto.MarksObtained);

            
            var mark = new Mark
            {
                MarkId = markDto.MarkId.Value,
                StudentId = markDto.StudentId,
                SubjectId = markDto.SubjectId,
                ExamId = markDto.ExamId,
                MarksObtained = markDto.MarksObtained,
                GradedByLecturerId = markDto.GradedByLecturerId,
                Grade = markDto.Grade,
                EntryDate = markDto.EntryDate 
            };

            _markRepository.UpdateMark(mark);
        }

        public void DeleteMark(int markId)
        {
            if (markId <= 0)
                throw new ArgumentException("Invalid Mark ID for deletion.");
            _markRepository.DeleteMark(markId);
        }

        public MarkDisplayDto GetMarkDisplayById(int markId)
        {
            if (markId <= 0)
                throw new ArgumentException("Invalid Mark ID.");

            var mark = _markRepository.GetMarkById(markId);
            return MapMarkToMarkDisplayDto(mark); 
        }

        
        public List<MarkDisplayDto> GetAllMarkDetails()
        {
           
            return _markRepository.GetAllMarksWithDetails();
        }

        
        public List<MarkDisplayDto> GetMarkDetailsByStudentId(int studentId)
        {
            if (studentId <= 0)
                throw new ArgumentException("Invalid Student ID.");

            
            var marks = _markRepository.GetMarksByStudentId(studentId);
            return marks.Select(m => MapMarkToMarkDisplayDto(m)).ToList();
        }

      
        public List<MarkDisplayDto> GetMarkDetailsBySubjectId(int subjectId)
        {
            if (subjectId <= 0)
                throw new ArgumentException("Invalid Subject ID.");

            var marks = _markRepository.GetMarksBySubjectId(subjectId);
            return marks.Select(m => MapMarkToMarkDisplayDto(m)).ToList();
        }

        public List<MarkDisplayDto> GetMarkDetailsByExamId(int examId)
        {
            if (examId <= 0)
                throw new ArgumentException("Invalid Exam ID.");

            var marks = _markRepository.GetMarksByExamId(examId);
            return marks.Select(m => MapMarkToMarkDisplayDto(m)).ToList();
        }

        
        public List<MarkDisplayDto> GetMarkDetailsByLecturerId(int lecturerId)
        {
            if (lecturerId <= 0)
                throw new ArgumentException("Invalid Lecturer ID.");

            var marks = _markRepository.GetMarksByLecturerId(lecturerId);
            return marks.Select(m => MapMarkToMarkDisplayDto(m)).ToList();
        }

        
        public List<TopPerformerDto> GetTopPerformers(int count)
        {
            if (count <= 0)
                throw new ArgumentException("Count must be greater than zero.");
            return _markRepository.GetTopNPerformers(count);
        }

        
        public string CalculateGrade(int marksObtained)
        {
            if (marksObtained < 0 || marksObtained > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(marksObtained), "Marks must be between 0 and 100 to calculate grade.");
            }

            
            if (marksObtained >= 75)
                return "A";
            else if (marksObtained >= 65)
                return "B";
            else if (marksObtained >= 55)
                return "C";
            else if (marksObtained >= 45)
                return "D";
            else if (marksObtained >= 35)
                return "E";
            else
                return "F";
        }
    }
}
