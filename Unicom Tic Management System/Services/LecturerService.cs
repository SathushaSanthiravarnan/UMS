using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.StaffDTOs;
using Unicom_Tic_Management_System.Models.Enums;
using Unicom_Tic_Management_System.Repositories;
using Unicom_Tic_Management_System.Repositories.Interfaces;
using Unicom_Tic_Management_System.Services.Interfaces;
using Unicom_Tic_Management_System.Utilities.Mappers;

namespace Unicom_Tic_Management_System.Services
{
    internal class LecturerService : ILecturerService
    {
        private readonly ILecturerRepository _lecturerRepository;
        private readonly IUserRepository _userRepository;
        private readonly INicDetailsRepository _nicDetailRepository;
        private readonly IDepartmentRepository _departmentRepository; // Assuming this exists or is mocked

        public LecturerService()
        {
            // Initialize repositories. In a real application, you'd use Dependency Injection here.
            _lecturerRepository = new LecturerRepository();
            _userRepository = new UserRepository(); // Using the fixed UserRepository
            _nicDetailRepository = new NICDetailsRepository(); // Using the fixed NICDetailsRepository
            _departmentRepository = new DepartmentRepository(); // Assuming this exists or mock
        }

        public LecturerDto GetLecturerById(int lecturerId)
        {
            var lecturer = _lecturerRepository.GetLecturerById(lecturerId);
            return LecturerMapper.ToDTO(lecturer);
        }

        public LecturerDto GetLecturerByNic(string nic)
        {
            var lecturer = _lecturerRepository.GetLecturerByNic(nic);
            return LecturerMapper.ToDTO(lecturer);
        }

        public List<LecturerDto> GetAllLecturers()
        {
            var lecturers = _lecturerRepository.GetAllLecturers();
            return LecturerMapper.ToDTOList(lecturers);
        }

        public List<LecturerDto> GetLecturersByDepartment(int departmentId)
        {
            var lecturers = _lecturerRepository.GetLecturersByDepartmentId(departmentId);
            return LecturerMapper.ToDTOList(lecturers);
        }

        public void AddLecturer(LecturerDto lecturerDto)
        {
            if (lecturerDto == null)
                throw new ArgumentNullException(nameof(lecturerDto));

            // Basic validation for required fields
            if (string.IsNullOrWhiteSpace(lecturerDto.Nic) ||
                string.IsNullOrWhiteSpace(lecturerDto.Email) ||
                string.IsNullOrWhiteSpace(lecturerDto.Name))
            {
                throw new ArgumentException("NIC, Email, and Name are required for new lecturer registration.");
            }

            // Check if NIC exists in NicDetail table
            if (!_nicDetailRepository.NicExists(lecturerDto.Nic.Trim()))
            {
                throw new InvalidOperationException("This NIC is not registered in the NIC Details system. Please add NIC details first.");
            }

            // Check if a user with this NIC or username (derived from email) already exists
            // For lecturers, let's assume username is derived from email or a similar unique identifier
            // For simplicity, let's derive username from email for lecturers, e.g., first part of email
            string username = lecturerDto.Email.Split('@')[0]; // Simple username derivation

            if (_userRepository.GetUserByUsername(username) != null)
            {
                throw new InvalidOperationException($"A user with username '{username}' already exists.");
            }
            if (_userRepository.GetUserByNIC(lecturerDto.Nic.Trim()) != null)
            {
                throw new InvalidOperationException("A user with this NIC is already registered.");
            }


            // Create User account
            var newUser = new User
            {
                Username = username,
                PasswordHash = UserRepository.HashPassword("defaultPassword"), // Consider generating a strong random password or prompting in UI
                Nic = lecturerDto.Nic.Trim(),
                Role = UserRole.Lecturer, // Set role specifically for Lecturers
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _userRepository.AddUser(newUser);

            // Mark NIC as used if your logic requires it upon association with a user/lecturer
            // _nicDetailRepository.MarkAsUsed(lecturerDto.Nic.Trim());

            // Map DTO to Entity and link to User
            var lecturer = LecturerMapper.ToEntity(lecturerDto);
            lecturer.UserId = newUser.UserId; // Link the lecturer to the newly created user
            lecturer.CreatedAt = DateTime.Now;
            lecturer.UpdatedAt = DateTime.Now;

            _lecturerRepository.AddLecturer(lecturer);
        }

        public void UpdateLecturer(LecturerDto lecturerDto)
        {
            if (lecturerDto == null)
                throw new ArgumentNullException(nameof(lecturerDto));

            var existingLecturer = _lecturerRepository.GetLecturerById(lecturerDto.LecturerId);
            if (existingLecturer == null)
            {
                throw new InvalidOperationException("Lecturer not found for update.");
            }

            var existingUser = _userRepository.GetUserById(existingLecturer.UserId);
            if (existingUser == null)
            {
                throw new InvalidOperationException("Associated user not found for lecturer update.");
            }

            
            existingLecturer.Name = lecturerDto.Name;
            existingLecturer.Phone = lecturerDto.Phone;
            existingLecturer.Address = lecturerDto.Address;
            existingLecturer.Email = lecturerDto.Email;
            existingLecturer.DepartmentId = lecturerDto.DepartmentId;
            existingLecturer.HireDate = lecturerDto.HireDate;
            existingLecturer.UpdatedAt = DateTime.Now;

            _lecturerRepository.UpdateLecturer(existingLecturer);
        }

        public void DeleteLecturer(int lecturerId)
        {
            var lecturer = _lecturerRepository.GetLecturerById(lecturerId);
            if (lecturer == null)
            {
                throw new InvalidOperationException("Lecturer not found for deletion.");
            }

            

            _lecturerRepository.DeleteLecturer(lecturerId);
        }
    }
}
