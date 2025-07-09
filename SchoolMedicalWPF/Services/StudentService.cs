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

        public  List<Student> GetAllAsync()
        {
            return  _context.Students.ToList();
        }

        public Student GetByIdAsync(int id)
        {
            return  _context.Students.Find(id);
        }

        public async Task AddAsync(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.FirstName) || string.IsNullOrWhiteSpace(student.LastName))
                throw new ArgumentException("Họ và tên không được để trống.");
            _context.Students.Add(student);
             _context.SaveChanges();
        }

        public void UpdateAsync(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.FirstName) || string.IsNullOrWhiteSpace(student.LastName))
                throw new ArgumentException("Họ và tên không được để trống.");
            _context.Students.Update(student);
             _context.SaveChanges();
        }

        public void DeleteAsync(int id)
        {
            var student =  _context.Students.Find(id);
            if (student == null)
                throw new KeyNotFoundException("Không tìm thấy học sinh.");
            _context.Students.Remove(student);
            _context.SaveChanges();
        }
    }
}