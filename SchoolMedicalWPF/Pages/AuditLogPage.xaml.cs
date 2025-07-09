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
    public partial class AuditLogPage : Page
    {
        private readonly AuditLogService _logService;
        private readonly StaffService _staffService;
        private AuditLog _selectedLog;

        public AuditLogPage()
        {
            InitializeComponent();
            _logService = new AuditLogService();
            _staffService = new StaffService();
            LoadData();
        }

        private  void LoadData()
        {
            try
            {
                LogsDataGrid.ItemsSource =  _logService.GetAllAsync();
                StaffComboBox.ItemsSource =  _staffService.GetAllAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private  void AddLog(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedStaff = StaffComboBox.SelectedItem as Staff;
                if (selectedStaff == null)
                    throw new ArgumentException("Vui lòng chọn nhân viên.");

                var log = new AuditLog
                {
                    StaffId = selectedStaff.StaffId,
                    Action = ActionTextBox.Text,
                    Details = DetailsTextBox.Text,
                    LogDate = LogDatePicker.SelectedDate ?? DateTime.Now
                };
                 _logService.AddAsync(log);
                LoadData();
                ClearInputs();
                MessageBox.Show("Thêm nhật ký kiểm tra thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private  void UpdateLog(object sender, RoutedEventArgs e)
        {
            if (_selectedLog == null)
            {
                MessageBox.Show("Vui lòng chọn nhật ký kiểm tra để cập nhật.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var selectedStaff = StaffComboBox.SelectedItem as Staff;
                if (selectedStaff == null)
                    throw new ArgumentException("Vui lòng chọn nhân viên.");

                _selectedLog.StaffId = selectedStaff.StaffId;
                _selectedLog.Action = ActionTextBox.Text;
                _selectedLog.Details = DetailsTextBox.Text;
                _selectedLog.LogDate = LogDatePicker.SelectedDate ?? DateTime.Now;
                 _logService.UpdateAsync(_selectedLog);
                LoadData();
                ClearInputs();
                MessageBox.Show("Cập nhật nhật ký kiểm tra thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private  void DeleteLog(object sender, RoutedEventArgs e)
        {
            if (_selectedLog == null)
            {
                MessageBox.Show("Vui lòng chọn nhật ký kiểm tra để xóa.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc muốn xóa nhật ký kiểm tra này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                     _logService.DeleteAsync(_selectedLog.LogId);
                    LoadData();
                    ClearInputs();
                    MessageBox.Show("Xóa nhật ký kiểm tra thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void LogsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedLog = LogsDataGrid.SelectedItem as AuditLog;
            if (_selectedLog != null)
            {
                // Tìm Staff trong ItemsSource của ComboBox dựa trên StaffId
                var selectedStaff = (StaffComboBox.ItemsSource as IEnumerable<Staff>)?.FirstOrDefault(s => s.StaffId == _selectedLog.StaffId);
                StaffComboBox.SelectedItem = selectedStaff;

                ActionTextBox.Text = _selectedLog.Action;
                DetailsTextBox.Text = _selectedLog.Details;
                LogDatePicker.SelectedDate = _selectedLog.LogDate;
            }
        }

        private void ClearInputs(object sender, RoutedEventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs()
        {
            StaffComboBox.SelectedIndex = -1;
            ActionTextBox.Text = string.Empty;
            DetailsTextBox.Text = string.Empty;
            LogDatePicker.SelectedDate = null;
            _selectedLog = null;
        }
    }
}