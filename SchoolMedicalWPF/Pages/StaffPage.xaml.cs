using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SchoolMedicalWPF.Models;
using SchoolMedicalWPF.Services;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace SchoolMedicalWPF.Pages
{
    public partial class StaffPage : Page
    {
        private readonly StaffService _staffService;
        private Staff _selectedStaff;

        public StaffPage()
        {
            InitializeComponent();
            
            _staffService = new StaffService();
            LoadStaff();
        }

        

        private async void LoadStaff()
        {
            try
            {
                StaffDataGrid.ItemsSource = await _staffService.GetAllAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void AddStaff(object sender, RoutedEventArgs e)
        {
            try
            {
                var staff = new Staff
                {
                    FirstName = FirstNameTextBox.Text,
                    LastName = LastNameTextBox.Text,
                    Role = RoleTextBox.Text,
                    Username = UsernameTextBox.Text,
                    PasswordHash = PasswordHashTextBox.Text,
                    Email = EmailTextBox.Text,
                    Contact = ContactTextBox.Text
                };
                await _staffService.AddAsync(staff);
                LoadStaff();
                ClearInputs();
                MessageBox.Show("Thêm nhân viên thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void UpdateStaff(object sender, RoutedEventArgs e)
        {
            if (_selectedStaff == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên để cập nhật.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                _selectedStaff.FirstName = FirstNameTextBox.Text;
                _selectedStaff.LastName = LastNameTextBox.Text;
                _selectedStaff.Role = RoleTextBox.Text;
                _selectedStaff.Username = UsernameTextBox.Text;
                _selectedStaff.PasswordHash = PasswordHashTextBox.Text;
                _selectedStaff.Email = EmailTextBox.Text;
                _selectedStaff.Contact = ContactTextBox.Text;
                await _staffService.UpdateAsync(_selectedStaff);
                LoadStaff();
                ClearInputs();
                MessageBox.Show("Cập nhật nhân viên thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteStaff(object sender, RoutedEventArgs e)
        {
            if (_selectedStaff == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên để xóa.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc muốn xóa nhân viên này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    await _staffService.DeleteAsync(_selectedStaff.StaffId);
                    LoadStaff();
                    ClearInputs();
                    MessageBox.Show("Xóa nhân viên thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void StaffDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedStaff = StaffDataGrid.SelectedItem as Staff;
            if (_selectedStaff != null)
            {
                FirstNameTextBox.Text = _selectedStaff.FirstName;
                LastNameTextBox.Text = _selectedStaff.LastName;
                RoleTextBox.Text = _selectedStaff.Role;
                UsernameTextBox.Text = _selectedStaff.Username;
                PasswordHashTextBox.Text = _selectedStaff.PasswordHash;
                EmailTextBox.Text = _selectedStaff.Email;
                ContactTextBox.Text = _selectedStaff.Contact;
            }
        }

        private void ClearInputs(object sender, RoutedEventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs()
        {
            FirstNameTextBox.Text = string.Empty;
            LastNameTextBox.Text = string.Empty;
            RoleTextBox.Text = string.Empty;
            UsernameTextBox.Text = string.Empty;
            PasswordHashTextBox.Text = string.Empty;
            EmailTextBox.Text = string.Empty;
            ContactTextBox.Text = string.Empty;
            _selectedStaff = null;
        }
    }
}