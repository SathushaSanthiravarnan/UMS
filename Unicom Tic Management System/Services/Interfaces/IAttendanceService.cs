using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.Scheduling_AttendanceDTOs;
using Unicom_Tic_Management_System.Models.Enums;

namespace Unicom_Tic_Management_System.Services.Interfaces
{
    internal interface IAttendanceService
    {
        AttendanceDto GetAttendanceById(int attendanceId);
        List<AttendanceDto> GetAllAttendanceRecords();
        List<AttendanceDto> GetAttendanceByStudent(int studentId);
        List<AttendanceDto> GetAttendanceBySubject(int subjectId);
        List<AttendanceDto> GetAttendanceByDate(DateTime date);
        List<AttendanceDto> GetAttendanceByTimeSlot(string timeSlot);
        List<AttendanceDto> GetAttendanceByStatus(AttendanceStatus status);
        void AddAttendance(AttendanceDto attendanceDto);
        void UpdateAttendance(AttendanceDto attendanceDto);
        void DeleteAttendance(int attendanceId);
    }
}
