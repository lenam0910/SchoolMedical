   M S B A M L   `   `   `  ����  SchoolMedicalWPF&     SchoolMedicalWPF.Pages.StaffPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        N� Title$  Quản lý Nhân viên��5      .��5      � $��20q�6      SecondaryColor#��[  6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6     CardBackground#��[ 6    �� CornerRadius$ 10��6K    �� Padding$ 15q�6]   $��0,0,0,15q�6j   ._�5      �� .+�5      �� $��Thông tin nhân viên��6     CustomTextBlock#��[ 67   $��18(�6`   $
��Bold"�6n   $��0,0,0,10q�6�   �� 5      $
��Họ��6   #��[ 6&   $��0,5,0,2q�6O   �� 5      -   5         ��Name$ FirstNameTextBox��5        CustomTextBox#��[ 64   $��0,0,0,5q�6[   
 � Tag$
 Họ��6l   �� 5      $
��Tên��6   #��[ 6'   $��0,5,0,2q�6P   �� 5      -   5        $ LastNameTextBox��5      #��[ 63   $��0,0,0,5q�6Z   $
 Tên��6k   �� 5      $��Vai trò��6   #��[ 6+   $��0,5,0,2q�6T   �� 5      -   5        $ RoleTextBox��5      #��[ 6/   $��0,0,0,5q�6V   $ Vai trò��6g   �� 5      $��Tên đăng nhập��6   #��[ 61   $��0,5,0,2q�6Z   �� 5      -   5        $ UsernameTextBox��5      #��[ 63   $��0,0,0,5q�6Z   $ Tên đăng nhập��6k   �� 5      $��Mật khẩu (Hash)��6   #��[ 63   $��0,5,0,2q�6\   �� 5      -   5        $ PasswordHashTextBox��5      #��[ 67   $��0,0,0,5q�6^   $ Mật khẩu (Hash)��6o   �� 5      $��Email��6   #��[ 6)   $��0,5,0,2q�6R   �� 5       -   5        $ EmailTextBox��5       #��[ 60   $��0,0,0,5q�6W   $ Email��6h   �� 5"      $��
Liên hệ��6   #��[ 6+   $��0,5,0,2q�6T   �� 5#      -   5        $ ContactTextBox��5#      #��[ 62   $��0,0,0,5q�6Y   $ 
Liên hệ��6j   �� 5%       �� Orientation$ 
Horizontal=�6   $��0,10,0,0q�66   .+�5&      �� -   5        $��Thêm��5&        CustomButton#��[ 6=   $��0,0,10,0q�6c   $	��100��6u   �� 5'      -	   5        $��Cập nhật��5'      #��[ 6D   $��0,0,10,0q�6j   $	��100��6|   �� 5(      -
   5        $
��Xóa��5(      #��[ 6?   $��0,0,10,0q�6e   $	��100��6w   �� 5)      -   5        $��	Xóa form��5)      #��[ 6D   $��0,0,10,0q�6j   $	��100��6|   5*      5+      5,      �� 5/   
   $��1��6   #��[ 6   $ 10��6K   $ 10q�6]   ._�50      &   System.Windows.Controls.DataGrid  -   5          Name$ StaffDataGrid��50         AutoGenerateColumns$ False��6.   	   
IsReadOnly$
	 True��6J   
   SelectionMode$
 Single=�6\   $��0q�51   I      Columns 52      0  *System.Windows.Controls.DataGridTextColumn  53      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 6Q   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *	StaffId+6Q     54      $
 Họ��6)   $	 100 6S    65   �� *	FirstName+6S     55      $
 Tên��6)   $	 100 6S    66   �� *
LastName+6S     56      $ Vai trò��6)   $	 100 6S    6:   �� *Role+6S     57      $ Tên đăng nhập��6)   $	 120 6]    6@   �� *
Username+6]     58      $ Email��6)   $	 150 6R    68   �� *Email+6R     59      $ 
Liên hệ��6)   $ * 6V    6:   �� *	Contact+6V   5;      5<      5=      5>      