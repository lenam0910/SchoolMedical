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

        public AuthService()
        {
            _context = new SchoolMedicalDbContext();
        }

        public async Task<Staff> LoginAsync(string username, string password)
        {
            var staff = await _context.Staff.FirstOrDefaultAsync(s => s.Username == username);
            if (staff == null || !BCrypt.Net.BCrypt.Verify(password, staff.PasswordHash))
                throw new UnauthorizedAccessException("Tên đăng nhập hoặc mật khẩu không đúng.");
            staff.LastLogin = DateTime.Now;
            await _context.SaveChangesAsync();
            return staff;
        }

        public async Task<bool> RequestPasswordResetAsync(string email)
        {
            var staff = await _context.Staff.FirstOrDefaultAsync(s => s.Email == email);
            if (staff == null)
                return false;

            // Giả lập gửi email với mã khôi phục
            string resetCode = Guid.NewGuid().ToString().Substring(0, 8);
            Console.WriteLine($"Mã khôi phục cho {email}: {resetCode}"); // Thay bằng gửi email thực tế
            return true;
        }

        public async Task RegisterAsync(Staff staff, string password)
        {
            if (string.IsNullOrWhiteSpace(staff.FirstName) || string.IsNullOrWhiteSpace(staff.LastName) ||
                string.IsNullOrWhiteSpace(staff.Username) || string.IsNullOrWhiteSpace(staff.Email) ||
                string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Vui lòng điền đầy đủ thông tin bắt buộc.");

            if (await _context.Staff.AnyAsync(s => s.Username == staff.Username || s.Email == staff.Email))
                throw new ArgumentException("Tên đăng nhập hoặc email đã tồn tại.");

            staff.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            staff.CreatedAt = DateTime.Now;
            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();

            // Giả lập gửi email xác nhận
            Console.WriteLine($"Email xác nhận đã gửi tới {staff.Email}");
        }
    }
}