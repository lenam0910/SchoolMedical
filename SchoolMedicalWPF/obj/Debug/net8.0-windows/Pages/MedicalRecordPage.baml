   M S B A M L   `   `   `  ����  SchoolMedicalWPF.    (SchoolMedicalWPF.Pages.MedicalRecordPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        N� Title$   Quản lý Hồ sơ Y tế��5      .��5      � $��20q�6      SecondaryColor#��[  6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6     CardBackground#��[ 6    �� CornerRadius$ 10��6K    �� Padding$ 15q�6]   $��0,0,0,15q�6j   ._�5      �� .+�5      �� $ ��Thông tin hồ sơ y tế��6     CustomTextBlock#��[ 68   $��18(�6a   $
��Bold"�6o   $��0,0,0,10q�6�   �� 5      $��
Học sinh��6   #��[ 6,   $��0,5,0,2q�6U   �� 5      -   5         ��Name$ StudentComboBox��5      $��0,0,0,5q�64   $��Segoe UI)�6E   $��14(�6[    �� DisplayMemberPath$ FullName��6i   �� 5      $��Tình trạng��6   #��[ 6.   $��0,5,0,2q�6W   �� 5      -   5         ��Name$ ConditionTextBox��5        CustomTextBox#��[ 64   $��0,0,0,5q�6[   
 � Tag$ Tình trạng��6l   �� 5      $��
Dị ứng��6   #��[ 6*   $��0,5,0,2q�6S   �� 5      -   5        $ AllergiesTextBox��5      #��[ 64   $��0,0,0,5q�6[   $ 
Dị ứng��6l   �� 5      $��Ghi chú��6   #��[ 6+   $��0,5,0,2q�6T   �� 5      -   5        $ NotesTextBox��5      #��[ 60   $��0,0,0,5q�6W   $ Ghi chú��6h    �� AcceptsReturn$
 True��6v   $��60��6�   �� 5      	 �� Orientation$	 
Horizontal=�6   $��0,10,0,0q�66   .+�5      �� -   5        $��Thêm��5        CustomButton#��[ 6>   $��0,0,10,0q�6d   $	��100��6v   �� 5      -   5        $��Cập nhật��5      #��[ 6E   $��0,0,10,0q�6k   $	��100��6}   �� 5      -   5        $
��Xóa��5      #��[ 6@   $��0,0,10,0q�6f   $	��100��6x   �� 5       -   5        $��	Xóa form��5       #��[ 6D   $��0,0,10,0q�6j   $	��100��6|   5!      5"      5#      �� 5&   
   $��1��6   #��[ 6   $ 10��6K   $ 10q�6]   ._�5'      &   System.Windows.Controls.DataGrid  -	   5        
  Name$
 RecordsDataGrid��5'         AutoGenerateColumns$ False��60      
IsReadOnly$
 True��6L      SelectionMode$ Single=�6^   $��0q�5(   K      Columns 5)      0  *System.Windows.Controls.DataGridTextColumn  5*      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 6R   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *
RecordId+6R     5+      $ 
Học sinh��6)   $	 150 6`    6;   �� *Student.FullName+6`     5,      $ Tình trạng��6)   $	 150 6[    6=   �� *	Condition+6[     5-      $ 
Dị ứng��6)   $	 150 6W    69   �� *	Allergies+6W     5.      $ Ghi chú��6)   $ * 6T    6:   �� *Notes+6T   50      51      52      53      