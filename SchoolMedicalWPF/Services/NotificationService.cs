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

        public List<Notification> GetAllAsync()
        {
            return  _context.Notifications.Include(n => n.Staff).ToList();
        }

        public Notification GetByIdAsync(int id)
        {
            return  _context.Notifications.Find(id);
        }

        public void AddAsync(Notification notification)
        {
            if (notification.StaffId <= 0 || string.IsNullOrWhiteSpace(notification.Message))
                throw new ArgumentException("ID nhân viên hoặc thông điệp không hợp lệ.");
            _context.Notifications.Add(notification);
            _context.SaveChanges();
        }

        public void UpdateAsync(Notification notification)
        {
            if (notification.StaffId <= 0 || string.IsNullOrWhiteSpace(notification.Message))
                throw new ArgumentException("ID nhân viên hoặc thông điệp không hợp lệ.");
            _context.Notifications.Update(notification);
            _context.SaveChanges();
        }

        public void DeleteAsync(int id)
        {
            var notification =  _context.Notifications.Find(id);
            if (notification == null)
                throw new KeyNotFoundException("Không tìm thấy thông báo.");
            _context.Notifications.Remove(notification);
            _context.SaveChanges();
        }
    }
}