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

        public async Task<List<Staff>> GetAllAsync()
        {
            return await _context.Staff.ToListAsync();
        }

        public async Task<Staff> GetByIdAsync(int id)
        {
            return await _context.Staff.FindAsync(id);
        }

        public async Task AddAsync(Staff staff)
        {
            if (string.IsNullOrWhiteSpace(staff.FirstName) || string.IsNullOrWhiteSpace(staff.LastName) || string.IsNullOrWhiteSpace(staff.Username))
                throw new ArgumentException("Họ, tên và tên đăng nhập không được để trống.");
            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Staff staff)
        {
            if (string.IsNullOrWhiteSpace(staff.FirstName) || string.IsNullOrWhiteSpace(staff.LastName) || string.IsNullOrWhiteSpace(staff.Username))
                throw new ArgumentException("Họ, tên và tên đăng nhập không được để trống.");
            _context.Staff.Update(staff);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff == null)
                throw new KeyNotFoundException("Không tìm thấy nhân viên.");
            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();
        }
    }
}