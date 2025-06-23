using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;

namespace Unicom_Tic_Management_System.Repositories.Interfaces
{
    internal interface IAttendanceRepository
    {
        void AddAttendance(Attendance attendance);
        void UpdateAttendance(Attendance attendance);
        void DeleteAttendance(int attendanceId);
        Attendance GetAttendanceById(int attendanceId);
        List<Attendance> GetAttendanceByStudentId(int studentId);
        List<Attendance> GetAttendanceBySubjectId(int subjectId);
        List<Attendance> GetAttendanceByDate(DateTime date);
        List<Attendance> GetAttendanceByStudentAndSubject(int studentId, int subjectId);
        List<Attendance> GetAttendanceByStudentAndDate(int studentId, DateTime date);
        List<Attendance> GetAllAttendance();
    }
}
