using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolMedicalWPF.Models;
namespace SchoolMedicalWPF.Services
{
    public class StudentService
    {
        private readonly SchoolMedicalDbContext _context;

        public StudentService()
        {
            _context = new SchoolMedicalDbContext();
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task AddAsync(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.FirstName) || string.IsNullOrWhiteSpace(student.LastName))
                throw new ArgumentException("Họ và tên không được để trống.");
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.FirstName) || string.IsNullOrWhiteSpace(student.LastName))
                throw new ArgumentException("Họ và tên không được để trống.");
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                throw new KeyNotFoundException("Không tìm thấy học sinh.");
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
    }
}