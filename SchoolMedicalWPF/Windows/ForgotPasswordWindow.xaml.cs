using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SchoolMedicalWPF.Services;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SchoolMedicalWPF.Windows
{
    public partial class ForgotPasswordWindow : Window
    {
        private readonly AuthService _authService;
        private string _email;
        private string _otp;

        public ForgotPasswordWindow()
        {
            InitializeComponent();
            _authService = new AuthService();
        }

        private void SendResetCode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var success = _authService.RequestPasswordResetAsync(EmailTextBox.Text);
                if (success != null)
                {
                    MessageBox.Show("Mã khôi phục đã được gửi tới email của bạn.", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                    _email = EmailTextBox.Text;
                    _otp = success;
                    EmailTextBox.IsEnabled = false;
                    EmailLabel.Visibility = Visibility.Collapsed; // Hide email label
                    EmailTextBox.Visibility = Visibility.Collapsed; // Hide email textbox
                    SendResetButton.Visibility = Visibility.Collapsed;
                    ResetCodeLabel.Visibility = Visibility.Visible;
                    ResetCodeTextBox.Visibility = Visibility.Visible;
                    VerifyCodeButton.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Email không tồn tại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void VerifyResetCode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var isValid = !string.IsNullOrEmpty(ResetCodeTextBox.Text) && ResetCodeTextBox.Text.Equals(_otp);
                if (isValid)
                {
                    MessageBox.Show("Mã xác nhận hợp lệ.", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                    ResetCodeTextBox.IsEnabled = false;
                    ResetCodeLabel.Visibility = Visibility.Collapsed; // Hide OTP label
                    ResetCodeTextBox.Visibility = Visibility.Collapsed; // Hide OTP textbox
                    VerifyCodeButton.Visibility = Visibility.Collapsed;
                    NewPasswordLabel.Visibility = Visibility.Visible;
                    NewPasswordBox.Visibility = Visibility.Visible;
                    ConfirmPasswordLabel.Visibility = Visibility.Visible;
                    ConfirmPasswordBox.Visibility = Visibility.Visible;
                    ChangePasswordButton.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Mã xác nhận không đúng.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (NewPasswordBox.Password != ConfirmPasswordBox.Password)
                {
                    MessageBox.Show("Mật khẩu xác nhận không khớp.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var success = _authService.ChangePassword(_email, NewPasswordBox.Password);
                if (success)
                {
                    MessageBox.Show("Đổi mật khẩu thành công.", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                    NewPasswordLabel.Visibility = Visibility.Collapsed; // Hide new password label
                    NewPasswordBox.Visibility = Visibility.Collapsed; // Hide new password box
                    ConfirmPasswordLabel.Visibility = Visibility.Collapsed; // Hide confirm password label
                    ConfirmPasswordBox.Visibility = Visibility.Collapsed; // Hide confirm password box
                    ChangePasswordButton.Visibility = Visibility.Collapsed;
                    Close();
                }
                else
                {
                    MessageBox.Show("Đổi mật khẩu thất bại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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