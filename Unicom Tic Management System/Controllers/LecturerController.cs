using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.StaffDTOs;
using Unicom_Tic_Management_System.Services.Interfaces;

namespace Unicom_Tic_Management_System.Controllers
{
    internal class LecturerController
    {
        private readonly ILecturerService _lecturerService;

        public LecturerController(ILecturerService lecturerService)
        {
            _lecturerService = lecturerService;
        }

        public void RegisterLecturer()
        {
            Console.WriteLine("=== Register New Lecturer ===");

            Console.Write("Name: ");
            var name = Console.ReadLine();

            Console.Write("NIC: ");
            var nic = Console.ReadLine();

            Console.Write("Phone: ");
            var phone = Console.ReadLine();

            Console.Write("Email: ");
            var email = Console.ReadLine();

            Console.Write("Department ID (optional): ");
            var departmentInput = Console.ReadLine();
            int? departmentId = int.TryParse(departmentInput, out var depId) ? depId : (int?)null;

            Console.Write("Hire Date (yyyy-MM-dd) (optional): ");
            var hireDateInput = Console.ReadLine();
            DateTime? hireDate = DateTime.TryParse(hireDateInput, out var date) ? date : (DateTime?)null;

            Console.Write("User ID: ");
            int userId = int.Parse(Console.ReadLine());

            var newLecturerDto = new LecturerDto
            {
                Name = name,
                Nic = nic,
                Phone = phone,
                Email = email,
                DepartmentId = departmentId,
                HireDate = hireDate
            };

            
            var lecturer = _lecturerService as Services.LecturerService;
            if (lecturer != null)
            {
                var entity = Utilities.Mappers.LecturerMapper.ToEntity(newLecturerDto);
                entity.UserId = userId;
                entity.CreatedAt = DateTime.Now;
                entity.UpdatedAt = DateTime.Now;
                _lecturerService.AddLecturer(Utilities.Mappers.LecturerMapper.ToDTO(entity));
            }
            else
            {
                _lecturerService.AddLecturer(newLecturerDto); 
            }

            Console.WriteLine("Lecturer registered successfully!");
        }
    }
}

