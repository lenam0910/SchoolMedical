   M S B A M L   `   `   `  ����  SchoolMedicalWPF(    "SchoolMedicalWPF.Pages.StudentPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        N� Title$  Quản lý Học sinh��5      .��5      � $��20q�6      SecondaryColor#��[  6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6     CardBackground#��[ 6    �� CornerRadius$ 10��6K    �� Padding$ 15q�6]   $��0,0,0,15q�6j   ._�5      �� .+�5      �� $��Thông tin học sinh��6     CustomTextBlock#��[ 66   $��18(�6_   $
��Bold"�6m   $��0,0,0,10q�6   �� 5      $
��Họ��6   #��[ 6&   $��0,5,0,2q�6O   �� 5      -   5         ��Name$ FirstNameTextBox��5        CustomTextBox#��[ 64   $��0,0,0,5q�6[   
 � Tag$
 Họ��6l   �� 5      $
��Tên��6   #��[ 6'   $��0,5,0,2q�6P   �� 5      -   5        $ LastNameTextBox��5      #��[ 63   $��0,0,0,5q�6Z   $
 Tên��6k   �� 5      $��
Ngày sinh��6   #��[ 6-   $��0,5,0,2q�6V   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ DateOfBirthPicker��5      $��0,0,0,5q�68   $��Segoe UI)�6I   $��14(�6_   �� 5      $��Giới tính��6   #��[ 6-   $��0,5,0,2q�6V   �� 5      -   5        $ GenderTextBox��5      #��[ 61   $��0,0,0,5q�6X   $ Giới tính��6i   �� 5      $��Lớp��6   #��[ 6'   $��0,5,0,2q�6P   �� 5      -   5        $ ClassTextBox��5      #��[ 60   $��0,0,0,5q�6W   $ Lớp��6h   �� 5      $��Liên hệ khẩn cấp��6   #��[ 64   $��0,5,0,2q�6]   �� 5       -   5        $ EmergencyContactTextBox��5       #��[ 6;   $��0,0,0,5q�6b   $ Liên hệ khẩn cấp��6s   �� 5"       �� Orientation$ 
Horizontal=�6   $��0,10,0,0q�66   .+�5#      �� -   5        $��Thêm��5#        CustomButton#��[ 6?   $��0,0,10,0q�6e   $	��100��6w   �� 5$      -   5        $��Cập nhật��5$      #��[ 6F   $��0,0,10,0q�6l   $	��100��6~   �� 5%      -	   5        $
��Xóa��5%      #��[ 6A   $��0,0,10,0q�6g   $	��100��6y   �� 5&      -
   5        $��	Xóa form��5&      #��[ 6D   $��0,0,10,0q�6j   $	��100��6|   5'      5(      5)      �� 5,   
   $��1��6   #��[ 6   $ 10��6K   $ 10q�6]   ._�5-      &   System.Windows.Controls.DataGrid  -   5          Name$ StudentsDataGrid��5-      	   AutoGenerateColumns$	 False��61   
   
IsReadOnly$

 True��6M      SelectionMode$ Single=�6_   $��0q�5.   L      Columns 5/      0  *System.Windows.Controls.DataGridTextColumn  50      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 6S   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *	StudentId+6S     51      $
 Họ��6)   $	 100 6S    65   �� *	FirstName+6S     52      $
 Tên��6)   $	 100 6S    66   �� *
LastName+6S     53      $ 
Ngày sinh��6)   $	 120 6u    6<   �� *DateOfBirth+ �� StringFormat$ 
dd/MM/yyyy��6u     54      $ Giới tính��6)   $ 80 6W    6<   �� *Gender+6W     55      $ Lớp��6)   $ 80 6P    66   �� *Class+6P     56      $ Liên hệ khẩn cấp��6)   $ * 6h    6C   �� *EmergencyContact+6h   58      59      5:      5;      