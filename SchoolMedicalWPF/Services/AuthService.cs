using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using SchoolMedicalWPF.Models;

namespace SchoolMedicalWPF.Services
{
    public class AuthService
    {
        private readonly SchoolMedicalDbContext _context;
        private readonly EmailSenderService emailSender;

        public AuthService()
        {
            _context = new SchoolMedicalDbContext();
            emailSender = new EmailSenderService();
        }

        public Staff LoginAsync(string username, string password)
        {
            var staff =  _context.Staff.FirstOrDefault(s => s.Username == username);
            if (staff == null || !BCrypt.Net.BCrypt.Verify(password, staff.PasswordHash))
                throw new UnauthorizedAccessException("Tên đăng nhập hoặc mật khẩu không đúng.");
            staff.LastLogin = DateTime.Now;
            _context.SaveChanges();
            return staff;
        }
        public bool ChangePassword(string email, string newPassword)
        {
          

            var staff = _context.Staff.FirstOrDefault(s => s.Email == email);
            if (staff == null)
                return false;

            staff.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            _context.SaveChanges();
            return true;
        }
        public String RequestPasswordResetAsync(string email)
        {
            var staff =  _context.Staff.FirstOrDefaultAsync(s => s.Email == email);
            if (staff == null)
            {
                return null;
            }
            String OTP = emailSender.GenerateOTP();
            emailSender.SendEmail(email, OTP);
            return OTP;
        }

        public void RegisterAsync(Staff staff, string password)
        {
            if (string.IsNullOrWhiteSpace(staff.FirstName) || string.IsNullOrWhiteSpace(staff.LastName) ||
                string.IsNullOrWhiteSpace(staff.Username) || string.IsNullOrWhiteSpace(staff.Email) ||
                string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Vui lòng điền đầy đủ thông tin bắt buộc.");

            if ( _context.Staff.Any(s => s.Username == staff.Username || s.Email == staff.Email))
                throw new ArgumentException("Tên đăng nhập hoặc email đã tồn tại.");

            staff.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            staff.CreatedAt = DateTime.Now;
            _context.Staff.Add(staff);
             _context.SaveChangesAsync();

            // Giả lập gửi email xác nhận
            Console.WriteLine($"Email xác nhận đã gửi tới {staff.Email}");
        }
    }
}