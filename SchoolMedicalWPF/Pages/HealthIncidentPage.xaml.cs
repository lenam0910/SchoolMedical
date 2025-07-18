﻿using Microsoft.EntityFrameworkCore;
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
    public partial class HealthIncidentPage : Page
    {
        private readonly HealthIncidentService _incidentService;
        private readonly StudentService _studentService;
        private readonly StaffService _staffService;
        private HealthIncident _selectedIncident;
        public HealthIncidentPage()
        {
            InitializeComponent();
            _incidentService = new HealthIncidentService();
            _studentService = new StudentService();
            _staffService = new StaffService();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                IncidentsDataGrid.ItemsSource = _incidentService.GetAllAsync();
                StudentComboBox.ItemsSource = _studentService.GetAllAsync();
                StaffComboBox.ItemsSource = _staffService.GetAllAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddIncident(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedStudent = StudentComboBox.SelectedItem as Student;
                var selectedStaff = StaffComboBox.SelectedItem as Staff;
                if (selectedStudent == null || selectedStaff == null)
                    throw new ArgumentException("Vui lòng chọn học sinh và nhân viên.");

                var incident = new HealthIncident
                {
                    StudentId = selectedStudent.StudentId,
                    StaffId = selectedStaff.StaffId,
                    IncidentDate = IncidentDatePicker.SelectedDate ?? DateTime.Now,
                    Description = DescriptionTextBox.Text,
                    ActionTaken = ActionTakenTextBox.Text,
                    CreatedAt = DateTime.Now
                };
                _incidentService.AddAsync(incident);
                LoadData();
                ClearInputs();
                MessageBox.Show("Thêm sự cố sức khỏe thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateIncident(object sender, RoutedEventArgs e)
        {
            if (_selectedIncident == null)
            {
                MessageBox.Show("Vui lòng chọn sự cố sức khỏe để cập nhật.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var selectedStudent = StudentComboBox.SelectedItem as Student;
                var selectedStaff = StaffComboBox.SelectedItem as Staff;
                if (selectedStudent == null || selectedStaff == null)
                    throw new ArgumentException("Vui lòng chọn học sinh và nhân viên.");

                _selectedIncident.StudentId = selectedStudent.StudentId;
                _selectedIncident.StaffId = selectedStaff.StaffId;
                _selectedIncident.IncidentDate = IncidentDatePicker.SelectedDate ?? DateTime.Now;
                _selectedIncident.Description = DescriptionTextBox.Text;
                _selectedIncident.ActionTaken = ActionTakenTextBox.Text;
                _incidentService.UpdateAsync(_selectedIncident);
                LoadData();
                ClearInputs();
                MessageBox.Show("Cập nhật sự cố sức khỏe thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteIncident(object sender, RoutedEventArgs e)
        {
            if (_selectedIncident == null)
            {
                MessageBox.Show("Vui lòng chọn sự cố sức khỏe để xóa.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc muốn xóa sự cố sức khỏe này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _incidentService.DeleteAsync(_selectedIncident.IncidentId);
                    LoadData();
                    ClearInputs();
                    MessageBox.Show("Xóa sự cố sức khỏe thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void IncidentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedIncident = IncidentsDataGrid.SelectedItem as HealthIncident;
            if (_selectedIncident != null)
            {
                // Tìm Student trong ItemsSource của ComboBox dựa trên StudentId
                var selectedStudent = (StudentComboBox.ItemsSource as IEnumerable<Student>)?.FirstOrDefault(s => s.StudentId == _selectedIncident.StudentId);
                StudentComboBox.SelectedItem = selectedStudent;

                // Tìm Staff trong ItemsSource của ComboBox dựa trên StaffId
                var selectedStaff = (StaffComboBox.ItemsSource as IEnumerable<Staff>)?.FirstOrDefault(s => s.StaffId == _selectedIncident.StaffId);
                StaffComboBox.SelectedItem = selectedStaff;

                IncidentDatePicker.SelectedDate = _selectedIncident.IncidentDate;
                DescriptionTextBox.Text = _selectedIncident.Description;
                ActionTakenTextBox.Text = _selectedIncident.ActionTaken;
            }
        }

        private void ClearInputs(object sender, RoutedEventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs()
        {
            StudentComboBox.SelectedIndex = -1;
            StaffComboBox.SelectedIndex = -1;
            IncidentDatePicker.SelectedDate = null;
            DescriptionTextBox.Text = string.Empty;
            ActionTakenTextBox.Text = string.Empty;
            _selectedIncident = null;
        }
    }

}