   M S B A M L   `   `   `  ����D  @LikeBerry, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:LikeBerry	LikeBerry  5   	   "    LikeBerry.BookingProcessPage   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:LikeBerry     5   	     � Title$&   LikeBerry Library - Book Booking��5   	   $	��700��62   $	��700��6?    � WindowStartupLocation$ CenterScreen=�5	   	    � 	Resources 5      "   LikeBerry.ImagePathConverter   ImagePathConverter��%   &	          5   
   6#   5      .��5      � .+�5   
   � $��20q�6    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��* �6    �� 5      $
��Auto �6    .+�5      �� ���#007ACC$��60��6*   $	��Top=�66   $��0,0,0,25q�6N   ._�5      �� $��Center=�6   $��Center=�69   
�������$��Booking Form��6g   $��24(�6{   $
��Bold"�6�   $��0,0,0,20q�6�   5      �� 5      $��1��6   $
��Auto=�6(   $��0,0,0,18q�6K   $��2��6]   .��5      �� .+�5      �� $��15(�6    $��Selected Book:��6.   $
��Bold"�6D   $��0,0,0,5q�6V   �� 5       -   5         ��Name$ SelectedBookName��5    .   $��14(�6    $��0,0,0,10q�6H   �� 5!      -   5         ��Name$ 	BookImage��5!      $	��150��6/   $	��200��6;   $��0,0,0,10q�6H   ��5"      �� *Img+ �� 	Converter# [  �� 5$      $��Borrow Duration:��6    $
��Bold"�68   $��0,0,0,5q�6J   �� 5%      -   5         ��Name$ BorrowDurationComboBox��5%   F   $	��250��6   $
��Left=�6+   $��0,0,0,10q�6f   .@�5&      �� $��1 day��6'   �� 5'      $��3 days��6'   �� 5(      $��7 days��6'   �� 5)      $��30 days��6'   5*      �� 5,       �� Orientation$ 
Horizontal=�6!   $��0,0,0,10q�6:   .+�5-      �� $��	Quantity:��6$   $
��Bold"�65   $��0,0,15,5q�6G   �� 5.      -   5        	 ��Name$	 QuantityTextBox��5.   "   $��50��6;   ��6F   �� *
Quantity+5/      �� 50      $ 
Horizontal=�6!   .+�51      �� 
���  ��$��13(�65   $��In-stock quantity:��6C   $
��Bold"�6]   $��0,0,15,5q�6o   �� 52      -   5        $ instock��52   C   
���  ��$��13(�65   $
��Bold"�6n   $��0,0,15,5q�6�   ��6T   �� *
Quantity+6�   53      �� 56      $��Personal Information��6    $
��Bold"�6<   $��18(�6N   $��	0,20,0,10q�6\   �� 58      $��
Full Name:��6    $
��Bold"�62   $��0,0,0,5q�6D   �� 59      -   5        $	 FullNameTextBox��59      $	��250��67   $
��Left=�6C   $��0,0,0,10q�6^   $��25��6p   �� 5;      $��Address:��6    $
��Bold"�60   $��0,0,0,5q�6B   �� 5<      -   5        $	 AddressTextBox��5<      $	��250��66   $
��Left=�6B   $��0,0,0,10q�6]   $��25��6o   �� 5>      $��Phone Number:��6    $
��Bold"�65   $��0,0,0,5q�6G   �� 5?      -   5        $	 PhoneNumberTextBox��5?      $	��250��6:   $
��Left=�6F   $��0,0,0,10q�6a   $��25��6s   5@      5A      �� 5D      $��2��6   $ 
Horizontal=�6&   $��Right=�6?   $��0,20,0,0q�6[   .+�5E      �� -	   5        $��Medium"�5E      $��Cancel��6I   $	��100��6Z   $��	0,30,10,0q�6f   �� 5F      -
   5        $��Medium"�5F      $��25��6C   $��Confirm Booking��6O   $��	0,30,30,0q�6i   $	��120��6|   ���#007ACC
�������6�   5G      5H      5I      5J      