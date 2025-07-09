using Microsoft.EntityFrameworkCore;
using SchoolMedicalWPF.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolMedicalWPF.Services
{
    public class NotificationService
    {
        private readonly SchoolMedicalDbContext _context;

        public NotificationService()
        {
            _context = new SchoolMedicalDbContext();
        }

        public async Task<List<Notification>> GetAllAsync()
        {
            return await _context.Notifications.Include(n => n.Staff).ToListAsync();
        }

        public async Task<Notification> GetByIdAsync(int id)
        {
            return await _context.Notifications.FindAsync(id);
        }

        public async Task AddAsync(Notification notification)
        {
            if (notification.StaffId <= 0 || string.IsNullOrWhiteSpace(notification.Message))
                throw new ArgumentException("ID nhân viên hoặc thông điệp không hợp lệ.");
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Notification notification)
        {
            if (notification.StaffId <= 0 || string.IsNullOrWhiteSpace(notification.Message))
                throw new ArgumentException("ID nhân viên hoặc thông điệp không hợp lệ.");
            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
                throw new KeyNotFoundException("Không tìm thấy thông báo.");
            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();
        }
    }
}