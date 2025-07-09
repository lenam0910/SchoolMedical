using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SchoolMedicalWPF.Models;
using SchoolMedicalWPF.Services;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SchoolMedicalWPF.Pages
{
    public partial class NotificationPage : Page
    {
        private readonly NotificationService _notificationService;
        private readonly StaffService _staffService;
        private Notification _selectedNotification;

        public NotificationPage()
        {
            InitializeComponent();
            _notificationService = new NotificationService();
            _staffService = new StaffService();
            LoadData();
        }

       

        private async void LoadData()
        {
            try
            {
                NotificationsDataGrid.ItemsSource = await _notificationService.GetAllAsync();
                StaffComboBox.ItemsSource = await _staffService.GetAllAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void AddNotification(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedStaff = StaffComboBox.SelectedItem as Staff;
                if (selectedStaff == null)
                    throw new ArgumentException("Vui lòng chọn nhân viên.");

                var notification = new Notification
                {
                    StaffId = selectedStaff.StaffId,
                    Message = MessageTextBox.Text,
                    IsRead = IsReadCheckBox.IsChecked ?? false,
                    CreatedAt = DateTime.Now
                };
                await _notificationService.AddAsync(notification);
                LoadData();
                ClearInputs();
                MessageBox.Show("Thêm thông báo thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void UpdateNotification(object sender, RoutedEventArgs e)
        {
            if (_selectedNotification == null)
            {
                MessageBox.Show("Vui lòng chọn thông báo để cập nhật.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var selectedStaff = StaffComboBox.SelectedItem as Staff;
                if (selectedStaff == null)
                    throw new ArgumentException("Vui lòng chọn nhân viên.");

                _selectedNotification.StaffId = selectedStaff.StaffId;
                _selectedNotification.Message = MessageTextBox.Text;
                _selectedNotification.IsRead = IsReadCheckBox.IsChecked ?? false;
                await _notificationService.UpdateAsync(_selectedNotification);
                LoadData();
                ClearInputs();
                MessageBox.Show("Cập nhật thông báo thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteNotification(object sender, RoutedEventArgs e)
        {
            if (_selectedNotification == null)
            {
                MessageBox.Show("Vui lòng chọn thông báo để xóa.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc muốn xóa thông báo này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    await _notificationService.DeleteAsync(_selectedNotification.NotificationId);
                    LoadData();
                    ClearInputs();
                    MessageBox.Show("Xóa thông báo thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void NotificationsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedNotification = NotificationsDataGrid.SelectedItem as Notification;
            if (_selectedNotification != null)
            {
                StaffComboBox.SelectedItem = _selectedNotification.Staff;
                MessageTextBox.Text = _selectedNotification.Message;
                IsReadCheckBox.IsChecked = _selectedNotification.IsRead;
            }
        }

        private void ClearInputs(object sender, RoutedEventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs()
        {
            StaffComboBox.SelectedIndex = -1;
            MessageTextBox.Text = string.Empty;
            IsReadCheckBox.IsChecked = false;
            _selectedNotification = null;
        }
    }
}