   M S B A M L   `   `   `  ����  SchoolMedicalWPF3    -SchoolMedicalWPF.Windows.ForgotPasswordWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Quên mật khẩu��5   	   $	��400��6   $	��400��6,    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�6.      SecondaryColor#��[  6D    � 	Resources 5        CustomPasswordBox �� 
TargetType  CustomTextBox �� BasedOn��%0   &	       7  �� 5   
   " G�6*   8   6C   5      .��5	      � $��20q�6   .+�5
   
   ��   CardBackground#��[ 6    �� CornerRadius$ 10��6>    �� Padding$ 20q�6P   ._�5      �� .+�5      �� $��Quên mật khẩu��6     CustomTextBlock#��[ 61   $��20(�6Z   $
��Bold"�6h   $��Center=�6z   $��0,0,0,20q�6�   �� 5      -   5         ��Name$ 
EmailLabel��5   �   $��Email:��6   #��[ 6*   $��18(�6S   $
��Bold"�6a   $��0,0,0,0q�6s   6�   �� 5      -   5        	 ��Name$	 EmailTextBox��5      #��[ 60   $	��0,5q�6W   

 � Tag$
 Email��6d   �� 5      -   5         ��Name$ SendResetButton��5   �   $��Gửi mã khôi phục��6     CustomButton#��[ 6P   $��	0,20,0,10q�6v   $	��150��6�   6�   �� 5      -   5        $ ResetCodeLabel��5   �   $��Mã khôi phục:��6   #��[ 61   $��18(�6Z   $
��Bold"�6h   $��0,10,0,0q�6z   $y�	Collapsed=�6�   6�   �� 5      -   5        $	 ResetCodeTextBox��5      #��[ 64   $	��0,5q�6[   $
 	ResetCode��6h   $y�	Collapsed=�6x   �� 5      -   5        $ VerifyCodeButton��5   �   $��Xác nhận mã��6   #��[ 6M   $��	0,10,0,10q�6s   $	��150��6�   $y�	Collapsed=�6�   6�   �� 5      -   5        $ NewPasswordLabel��5   �   $��Mật khẩu mới:��6   #��[ 61   $��18(�6Z   $
��Bold"�6h   $��0,10,0,0q�6z   $y�	Collapsed=�6�   6�   G� 5      -   5         G�Name$ NewPasswordBox��5      $	��0,5q�67   $
 NewPassword��6D   $y�	Collapsed=�6V   �� 5      -	   5        $ ConfirmPasswordLabel��5   �   $��Xác nhận mật khẩu:��6   #��[ 66   $��18(�6_   $
��Bold"�6m   $��0,10,0,0q�6   $y�	Collapsed=�6�   6�   G� 5      -
   5        $ ConfirmPasswordBox��5      $	��0,5q�6;   $
 ConfirmPassword��6H   $y�	Collapsed=�6^   �� 5      -   5        $ ChangePasswordButton��5   �   $��Đổi mật khẩu��6   #��[ 6M   $��	0,20,0,10q�6s   $	��150��6�   $y�	Collapsed=�6�   6�   �� 5      -   5        $��Quay lại đăng nhập��5      #��[ 66   $��Center=�6_    � Cursor$
 Hand��6|     PrimaryColor#��[ 6�   6�   5      5      5      5      