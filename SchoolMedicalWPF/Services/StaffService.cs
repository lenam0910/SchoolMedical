using Microsoft.EntityFrameworkCore;
using SchoolMedicalWPF.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolMedicalWPF.Services
{
    public class StaffService
    {
        private readonly SchoolMedicalDbContext _context;

        public StaffService()
        {
            _context = new SchoolMedicalDbContext();

        }

        public List<Staff> GetAllAsync()
        {
            return  _context.Staff.ToList();
        }

        public Staff GetByIdAsync(int id)
        {
            return  _context.Staff.Find(id);
        }

        public  void AddAsync(Staff staff)
        {
            if (string.IsNullOrWhiteSpace(staff.FirstName) || string.IsNullOrWhiteSpace(staff.LastName) || string.IsNullOrWhiteSpace(staff.Username))
                throw new ArgumentException("Họ, tên và tên đăng nhập không được để trống.");
            _context.Staff.Add(staff);
             _context.SaveChanges();
        }

        public void UpdateAsync(Staff staff)
        {
            if (string.IsNullOrWhiteSpace(staff.FirstName) || string.IsNullOrWhiteSpace(staff.LastName) || string.IsNullOrWhiteSpace(staff.Username))
                throw new ArgumentException("Họ, tên và tên đăng nhập không được để trống.");
            _context.Staff.Update(staff);
            _context.SaveChanges();
        }

        public  void DeleteAsync(int id)
        {
            var staff =  _context.Staff.Find(id);
            if (staff == null)
                throw new KeyNotFoundException("Không tìm thấy nhân viên.");
            _context.Staff.Remove(staff);
            _context.SaveChanges();
        }
    }
}