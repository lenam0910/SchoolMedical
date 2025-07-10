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
    public partial class AppointmentPage : Page
    {
        private readonly AppointmentService _appointmentService;
        private readonly StudentService _studentService;
        private readonly StaffService _staffService;
        private Appointment _selectedAppointment;
        private EmailSenderService emailSenderService;
        public AppointmentPage()
        {
            InitializeComponent();
            _appointmentService = new AppointmentService();
            _studentService = new StudentService();
            _staffService = new StaffService();
            emailSenderService = new();
            LoadData();
        }

        private  void LoadData()
        {
            try
            {
                AppointmentsDataGrid.ItemsSource =  _appointmentService.GetAllAsync();
                StudentComboBox.ItemsSource =  _studentService.GetAllAsync();
                StaffComboBox.ItemsSource =  _staffService.GetAllAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private  void AddAppointment(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedStudent = StudentComboBox.SelectedItem as Student;
                var selectedStaff = StaffComboBox.SelectedItem as Staff;
                if (selectedStudent == null || selectedStaff == null)
                    throw new ArgumentException("Vui lòng chọn học sinh và nhân viên.");

                var appointment = new Appointment
                {
                    StudentId = selectedStudent.StudentId,
                    StaffId = selectedStaff.StaffId,
                    AppointmentDate = AppointmentDatePicker.SelectedDate ?? DateTime.Now,
                    Reason = ReasonTextBox.Text,
                    Status = (StatusComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Scheduled",
                    CreatedAt = DateTime.Now
                };
                 _appointmentService.AddAsync(appointment);
                emailSenderService.SendAppointmentNotification(Email.Text,appointment);
                LoadData();
                ClearInputs();
                MessageBox.Show("Thêm lịch hẹn thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private  void UpdateAppointment(object sender, RoutedEventArgs e)
        {
            if (_selectedAppointment == null)
            {
                MessageBox.Show("Vui lòng chọn lịch hẹn để cập nhật.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var selectedStudent = StudentComboBox.SelectedItem as Student;
                var selectedStaff = StaffComboBox.SelectedItem as Staff;
                if (selectedStudent == null || selectedStaff == null)
                    throw new ArgumentException("Vui lòng chọn học sinh và nhân viên.");

                _selectedAppointment.StudentId = selectedStudent.StudentId;
                _selectedAppointment.StaffId = selectedStaff.StaffId;
                _selectedAppointment.AppointmentDate = AppointmentDatePicker.SelectedDate ?? DateTime.Now;
                _selectedAppointment.Reason = ReasonTextBox.Text;
                _selectedAppointment.Status = (StatusComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Scheduled";
                 _appointmentService.UpdateAsync(_selectedAppointment);
                LoadData();
                ClearInputs();
                MessageBox.Show("Cập nhật lịch hẹn thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private  void DeleteAppointment(object sender, RoutedEventArgs e)
        {
            if (_selectedAppointment == null)
            {
                MessageBox.Show("Vui lòng chọn lịch hẹn để xóa.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc muốn xóa lịch hẹn này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                     _appointmentService.DeleteAsync(_selectedAppointment.AppointmentId);
                    LoadData();
                    ClearInputs();
                    MessageBox.Show("Xóa lịch hẹn thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void AppointmentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedAppointment = AppointmentsDataGrid.SelectedItem as Appointment;
            if (_selectedAppointment != null)
            {
                // Tìm Student trong ItemsSource của ComboBox dựa trên StudentId
                var selectedStudent = (StudentComboBox.ItemsSource as IEnumerable<Student>)?.FirstOrDefault(s => s.StudentId == _selectedAppointment.StudentId);
                StudentComboBox.SelectedItem = selectedStudent;

                // Tương tự cho Staff
                var selectedStaff = (StaffComboBox.ItemsSource as IEnumerable<Staff>)?.FirstOrDefault(s => s.StaffId == _selectedAppointment.StaffId);
                StaffComboBox.SelectedItem = selectedStaff;

                AppointmentDatePicker.SelectedDate = _selectedAppointment.AppointmentDate;
                ReasonTextBox.Text = _selectedAppointment.Reason;
                StatusComboBox.SelectedItem = StatusComboBox.Items.Cast<ComboBoxItem>().FirstOrDefault(i => i.Content.ToString() == _selectedAppointment.Status);
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
            AppointmentDatePicker.SelectedDate = null;
            ReasonTextBox.Text = string.Empty;
            StatusComboBox.SelectedIndex = 0;
            _selectedAppointment = null;
        }
    }
}