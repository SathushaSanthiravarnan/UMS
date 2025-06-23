using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;

namespace Unicom_Tic_Management_System.Repositories.Interfaces
{
    internal interface IAdmissionNumberRepository
    {
        void AddAdmissionNumber(AdmissionNumber admissionNumber);
        void UpdateAdmissionNumber(AdmissionNumber admissionNumber);
        void DeleteAdmissionNumber(string admissionNumberText);
        AdmissionNumber GetAdmissionNumberByText(string admissionNumberText);
        AdmissionNumber GetAdmissionNumberByStudentId(int studentId);
        List<AdmissionNumber> GetAllAdmissionNumbers();
    }
}
