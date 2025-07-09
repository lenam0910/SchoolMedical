using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SchoolMedicalWPF.Models;
using SchoolMedicalWPF.Services;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace SchoolMedicalWPF.Windows
{
    public partial class RegisterWindow : Window
    {
        private readonly AuthService _authService;

        public RegisterWindow()
        {
            InitializeComponent();
            
            _authService = new AuthService();
        }

     

        private  void Register_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var staff = new Staff
                {
                    FirstName = FirstNameTextBox.Text,
                    LastName = LastNameTextBox.Text,
                    Role = RoleTextBox.Text,
                    Username = UsernameTextBox.Text,
                    Email = EmailTextBox.Text,
                    Contact = ContactTextBox.Text
                };
                 _authService.RegisterAsync(staff, PasswordBox.Password);
                MessageBox.Show("Đăng ký thành công! Vui lòng kiểm tra email để xác nhận.", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackToLogin_Click(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}