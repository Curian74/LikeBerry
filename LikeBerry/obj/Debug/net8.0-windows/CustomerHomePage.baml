   M S B A M L   `   `   `  ����D  @LikeBerry, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:LikeBerry	LikeBerry  5   	        LikeBerry.CustomerHomePage   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:LikeBerry     5   	     � Title$  LikeBerry Library��5   	   $	��600��6#   $
��1200��60    � WindowStartupLocation$ CenterScreen=�5	   	    � 
ResizeMode$ 	CanResize=�5
   	    � 	Resources 5      "   LikeBerry.ImagePathConverter   ImagePathConverter��%   &	          5   
   6#   5      .��5      � .+�5   
   �� ���#007ACC$��60��6&   $	��Top=�62   ._�5      � .+�5      �� $x��rhttps://static.wikia.nocookie.net/plantsvszombies/images/f/fc/Blueberryimage.png/revision/latest?cb=20150924171150��6   $
��Left=�5      $��Center=�63   $	��108��6N   $��88��6[   $��10,0,0,0q�6f   �� 5      $��Welcome to LikeBerry��6   $��Center=�68   $��Center=�6S   $��20(�5      $
��Bold"�6"   
�������64   �� 5      -   5        $��Logout��5   .   $��Right=�6?   $��Center=�6[   $��0,0,20,0q�5      $
��10,5q�6#   �� 5      -   5        $��My Bookings��5   1   $��Right=�6G   $��Center=�6c   $��	0,0,100,0q�5      $
��10,5q�6$   5      5      �� 5   
   $��0��6   $��20,80,20,20q�6"   .@�5!      �� $��Books��6   .��5"      �  � RowDefinitions 5#      �� 5$      $
��Auto �6(   �� 5%      $��* �6(   .+�5)      ��  �� Orientation$ 
Horizontal=�6!   $��10q�6:   .+�5*      �� $��Search:��6$   $��Center=�63   $��0,0,10,0q�6N   �� 5+      -   5        $��
searchBook��5+   "   $	��200��64   $��Center=�6@   6[   �� 5,      $��Genre:��6$   $��Center=�62   $��10,0,5,0q�6M   �� 5-      -   5        $��genreFilter��5-   #   $	��120��66   $��Center=�6B   ��5.      �� 5/   "   .R�50   &   �� ��60   �� *	GenreName+51   #   53      �� 54      $��Author:��6$   $��Center=�63   $��10,0,5,0q�6N   �� 55      -   5        $��authorFilter��55   #   $	��120��67   $��Center=�6C   ��56      �� 57   "   .R�58   &   �� ��60   �� *
AuthorName+59   #   5;      �� 5<      -   5        $��20��5<   !   $��50��6-   $��	100,0,0,0q�68   .��6f   	Refresh6o   5=      �� 5?      $��Medium"�6    
���  ��$��Italic$�6E   $��10,0,0,0q�6X   $��1��6j   /-*Note: Click on the book to select that book*6�   �� 5B      $��1��6#   $��25q�60   $
��Auto=�6<   .��5C      �� -   5        $��bookList��5C   #   ���#007ACC$��1q�5D   9   
������� �� View 5E      �� 5F   "   .P�5G   &   �� $��ID��65    �� Width$ 50��6i    �� DisplayMemberBinding 6A   �� *BookId+6i   �� 5H   &   $��Image��65   $	 120��6D   	 �� CellTemplate	 5I   *   �� 5J   .   .R�5K   2   �� $	��120��6~   $	��110��6�   ��68   �� *Img+
 �� 	Converter#
 [  6�   5L   /   5N   '   �� 5O   &   $��	Book name��65   $	 250��6r    6H   �� *
BookName+6r   �� 5P   &   $
��ISBN��65   $	 140��6i    6C   �� *Isbn+6i   �� 5Q   &   $��Author��65   $	 140��6x    6E   �� *Author.AuthorName+6x   �� 5R   &   $��Genre��65   $	 100��6u    6D   �� *Genre.GenreName+6u   �� 5S   &   $��Quantity��65   $ 70��6x    6G   �� *InstockQuantity+6x   �� 5T   &   $��
Issue Date��65   $ 80��6�    6I   �� *	IssueDate+ �� StringFormat$ {0:yyyy-MM-dd}��6�   �� 5U   &   $��Actions��65   $	 100��6F   	 5V   *   �� 5W   .   .R�5X   2   �� -   5        $��80��5X   9   $��Book Now��6Z   5Y   /   5[   '   5\   #   5^      5_      5`      5a      5b      5c      5d      