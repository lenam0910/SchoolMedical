using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SchoolMedicalWPF.Services;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace SchoolMedicalWPF.Windows
{
    public partial class ForgotPasswordWindow : Window
    {
        private readonly AuthService _authService;

        public ForgotPasswordWindow()
        {
            InitializeComponent();
       
            _authService = new AuthService();
        }

    

        private  void SendResetCode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var success =  _authService.RequestPasswordResetAsync(EmailTextBox.Text);
                if (success)
                    MessageBox.Show("Mã khôi phục đã được gửi tới email của bạn.", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Email không tồn tại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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