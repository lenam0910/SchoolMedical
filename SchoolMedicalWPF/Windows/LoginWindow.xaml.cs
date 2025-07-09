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
    public partial class LoginWindow : Window
    {
        private readonly AuthService _authService;

        public LoginWindow()
        {
            InitializeComponent();
        
            _authService = new AuthService();
        }

 

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var staff = await _authService.LoginAsync(UsernameTextBox.Text, PasswordBox.Password);
                var dashboard = new Dashboard(staff);
                dashboard.Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ForgotPassword_Click(object sender, MouseButtonEventArgs e)
        {
            var forgotPasswordWindow = new ForgotPasswordWindow();
            forgotPasswordWindow.ShowDialog();
        }

        private void Register_Click(object sender, MouseButtonEventArgs e)
        {
            var registerWindow = new RegisterWindow();
            registerWindow.ShowDialog();
        }
    }
}