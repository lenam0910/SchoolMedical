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
    public partial class MedicationPage : Page
    {
        private readonly MedicationService _medicationService;
        private readonly StudentService _studentService;
        private readonly StaffService _staffService;
        private Medication _selectedMedication;

        public MedicationPage()
        {
            InitializeComponent();
      
            _medicationService = new MedicationService();
            _studentService = new StudentService();
            _staffService = new StaffService();
            LoadData();
        }

      
        private async void LoadData()
        {
            try
            {
                MedicationsDataGrid.ItemsSource = await _medicationService.GetAllAsync();
                StudentComboBox.ItemsSource = await _studentService.GetAllAsync();
                PrescribedByComboBox.ItemsSource = await _staffService.GetAllAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void AddMedication(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedStudent = StudentComboBox.SelectedItem as Student;
                var selectedStaff = PrescribedByComboBox.SelectedItem as Staff;
                if (selectedStudent == null || selectedStaff == null)
                    throw new ArgumentException("Vui lòng chọn học sinh và bác sĩ.");

                var medication = new Medication
                {
                    StudentId = selectedStudent.StudentId,
                    Name = NameTextBox.Text,
                    Dosage = DosageTextBox.Text,
                    Frequency = FrequencyTextBox.Text,
                    StartDate = DateOnly.Parse(StartDatePicker.SelectedDate.ToString()),
                    EndDate = DateOnly.Parse(EndDatePicker.SelectedDate.ToString()),
                    PrescribedBy = selectedStaff.StaffId
                };
                await _medicationService.AddAsync(medication);
                LoadData();
                ClearInputs();
                MessageBox.Show("Thêm thuốc thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void UpdateMedication(object sender, RoutedEventArgs e)
        {
            if (_selectedMedication == null)
            {
                MessageBox.Show("Vui lòng chọn thuốc để cập nhật.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var selectedStudent = StudentComboBox.SelectedItem as Student;
                var selectedStaff = PrescribedByComboBox.SelectedItem as Staff;
                if (selectedStudent == null || selectedStaff == null)
                    throw new ArgumentException("Vui lòng chọn học sinh và bác sĩ.");

                _selectedMedication.StudentId = selectedStudent.StudentId;
                _selectedMedication.Name = NameTextBox.Text;
                _selectedMedication.Dosage = DosageTextBox.Text;
                _selectedMedication.Frequency = FrequencyTextBox.Text;
                _selectedMedication.StartDate = DateOnly.Parse(StartDatePicker.SelectedDate.ToString());
                _selectedMedication.EndDate =  DateOnly.Parse(EndDatePicker.SelectedDate.ToString());
                _selectedMedication.PrescribedBy = selectedStaff.StaffId;
                await _medicationService.UpdateAsync(_selectedMedication);
                LoadData();
                ClearInputs();
                MessageBox.Show("Cập nhật thuốc thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteMedication(object sender, RoutedEventArgs e)
        {
            if (_selectedMedication == null)
            {
                MessageBox.Show("Vui lòng chọn thuốc để xóa.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc muốn xóa thuốc này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    await _medicationService.DeleteAsync(_selectedMedication.MedicationId);
                    LoadData();
                    ClearInputs();
                    MessageBox.Show("Xóa thuốc thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void MedicationsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedMedication = MedicationsDataGrid.SelectedItem as Medication;
            if (_selectedMedication != null)
            {
                StudentComboBox.SelectedItem = _selectedMedication.Student;
                NameTextBox.Text = _selectedMedication.Name;
                DosageTextBox.Text = _selectedMedication.Dosage;
                FrequencyTextBox.Text = _selectedMedication.Frequency;
                StartDatePicker.SelectedDate = DateTime.Parse(_selectedMedication.StartDate.ToString());
                EndDatePicker.SelectedDate = DateTime.Parse(_selectedMedication.EndDate.ToString());
                PrescribedByComboBox.SelectedItem = _selectedMedication.PrescribedByNavigation;
            }
        }

        private void ClearInputs(object sender, RoutedEventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs()
        {
            StudentComboBox.SelectedIndex = -1;
            NameTextBox.Text = string.Empty;
            DosageTextBox.Text = string.Empty;
            FrequencyTextBox.Text = string.Empty;
            StartDatePicker.SelectedDate = null;
            EndDatePicker.SelectedDate = null;
            PrescribedByComboBox.SelectedIndex = -1;
            _selectedMedication = null;
        }
    }
}