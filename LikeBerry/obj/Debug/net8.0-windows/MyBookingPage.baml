   M S B A M L   `   `   `  ����D  @LikeBerry, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:LikeBerry	LikeBerry  5   	       LikeBerry.MyBookingPage   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:LikeBerry     5   	     � Title$  My Bookings��5   	   $	��600��6   $
��1200��6*    � WindowStartupLocation$ CenterScreen=�5	   	    � 	Resources 5      "   LikeBerry.ImagePathConverter   ImagePathConverter��%   &	          5   
   6#   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� ���#007ACC$��60��6&   $	��Top=�62   $��0��6J   ._�5      �� $��My Bookings��6   $��Center=�6+   $��Center=�6F   $��24(�5      $
��Bold"�6&   
�������68   5      �� 5   
    �� Orientation$ 
Horizontal=�6   $��10q�6.   $��1��6:   .+�5      �� $��Status:��6   $��Center=�6'   $��0,0,5,0q�6B   �� 5      -   5        $��statusFilter��5      $	��120��6+   $��Center=�67   .@�5      �� $��All Statuses��6   �� 5      $��	Submitted��6   �� 5       $��Approved��6   �� 5!      $��Denied��6   5"      5#      �� 5%   
   $��2��6   $��10q�6$   $
��Auto=�60   .��5&      �� -   5        $��myBookingList��5&      ���#007ACC$��1q�6B   
������� �� View 5'      �� 5(      .P�5)      �� $��Image��6)    �� Width$	 100��68    �� CellTemplate 5*      �� 5+   "   .R�5,   &   �� $	��120��6�   $	��110��6�   ��6,   �� *BookingDetails[0].Book.Img+ �� 	Converter# [  6�   5-   #   5/      �� 50      $��	Book name��6)   $	 150��6}   	 �� DisplayMemberBinding	 6<   �� *!BookingDetails[0].Book.BookName+6}   �� 51      $��	Full Name��6)   $	 100��6x   	 6<   �� *BookingDetails[0].FullName+6x   �� 52      $��Phone Number��6)   $	 100��6~   	 6?   �� *BookingDetails[0].PhoneNumber+6~   �� 53      $��Address��6)   $	 180��6u   	 6:   �� *BookingDetails[0].Address+6u   �� 54      $��Booking Date��6)   $	 150��6l   	 6?   �� *BookingDate+6l   �� 55      $��Return Date��6)   $	 100��6�   	 6>   �� *
ReturnDate+
 �� StringFormat$
 {0:yyyy-MM-dd}��6�   �� 56      $��Quantity��6)   $ 70��6w   	 6;   �� *BookingDetails[0].Quantity+6w   �� 57      $��Status��6)   $	 100��69    58      �� 59   "   .R�5:   &   �� ��60   �� *Status+��5;   *   �� 5<   .    �� 
TargetType" ��64   .�5=   2   ��  �� Property � �� �� Value$
 Bold"�6O    �� Triggers 5?   2   �� 5@   6    �� Binding 6B   �� *Status+ �� Value Approved6]   .a�5A   :   ��  � ��
 � � �6W   5B   7   �� 5C   6    6B   �� *Status+ 	Submitted6]   .a�5D   :   ��  � �� �#DFBA256W   5E   7   �� 5F   6    6B   �� *Status+
 Denied6]   .a�5G   :   ��  � ��
 �  ��6W   5H   7   5J   /   5L   '   5M   #   5O      �� 5P      $��Actions��6)   $	 100��6:    5Q      �� 5R   "   .R�5S   &   �� $ 
Horizontal=�61   .+�5T   *   �� -   5        $��50��5T   1   $��Cancel��6<   $��0,0,5,0q�6M   ��5U   .   �� 5V   2   " ��68   .�5W   6   ��  � y�$ 	Collapsed=�6S    5X   6   �� 5Y   :    6F   �� *Status+ 	Submitted6a   .a�5Z   >   ��  � y�$ Visible=�6[   5[   ;   5]   3   5_   +   5`   '   5a   #   5c      5d      5f      5g      5h      5i      