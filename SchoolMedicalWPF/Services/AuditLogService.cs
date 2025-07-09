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

        public  List<AuditLog> GetAllAsync()
        {
            return  _context.AuditLogs.Include(a => a.Staff).ToList();
        }

        public AuditLog GetByIdAsync(int id)
        {
            return _context.AuditLogs.Find(id);
        }

        public void AddAsync(AuditLog log)
        {
            if (log.StaffId <= 0 || string.IsNullOrWhiteSpace(log.Action))
                throw new ArgumentException("ID nhân viên hoặc hành động không hợp lệ.");
            _context.AuditLogs.Add(log);
            _context.SaveChanges();
        }

        public void UpdateAsync(AuditLog log)
        {
            if (log.StaffId <= 0 || string.IsNullOrWhiteSpace(log.Action))
                throw new ArgumentException("ID nhân viên hoặc hành động không hợp lệ.");
            _context.AuditLogs.Update(log);
            _context.SaveChanges();
        }

        public void DeleteAsync(int id)
        {
            var log =  _context.AuditLogs.Find(id);
            if (log == null)
                throw new KeyNotFoundException("Không tìm thấy nhật ký kiểm tra.");
            _context.AuditLogs.Remove(log);
            _context.SaveChanges();
        }
    }
}