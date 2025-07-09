using Microsoft.EntityFrameworkCore;
using SchoolMedicalWPF.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolMedicalWPF.Services
{
    public class AuditLogService
    {
        private readonly SchoolMedicalDbContext _context;

        public AuditLogService()
        {
            _context = new SchoolMedicalDbContext();

        }

        public async Task<List<AuditLog>> GetAllAsync()
        {
            return await _context.AuditLogs.Include(a => a.Staff).ToListAsync();
        }

        public async Task<AuditLog> GetByIdAsync(int id)
        {
            return await _context.AuditLogs.FindAsync(id);
        }

        public async Task AddAsync(AuditLog log)
        {
            if (log.StaffId <= 0 || string.IsNullOrWhiteSpace(log.Action))
                throw new ArgumentException("ID nhân viên hoặc hành động không hợp lệ.");
            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AuditLog log)
        {
            if (log.StaffId <= 0 || string.IsNullOrWhiteSpace(log.Action))
                throw new ArgumentException("ID nhân viên hoặc hành động không hợp lệ.");
            _context.AuditLogs.Update(log);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var log = await _context.AuditLogs.FindAsync(id);
            if (log == null)
                throw new KeyNotFoundException("Không tìm thấy nhật ký kiểm tra.");
            _context.AuditLogs.Remove(log);
            await _context.SaveChangesAsync();
        }
    }
}