   M S B A M L   `   `   `  ����D  @LikeBerry, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:LikeBerry	LikeBerry  5   	       LikeBerry.AddAuthorPage   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:LikeBerry     5   	     � Title$  Add New Author��5   	   $	��450��6    $	��500��6-    � WindowStartupLocation$ CenterScreen=�5	   	   .��5
      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0,0,0,20q�6   ���#007ACC$��60��68   $	��Top=�6D   $��0��6\   ._�5      �� $��Add New Author��6   $��Center=�6.   $��Center=�6I   $��24(�5      $
��Bold"�6   
�������60   5      �� 5   
   $��1��6   $
��Auto=�6$   .��5      �� .+�5      �  � ColumnDefinitions 5      �� 5      $
��Auto �6+   �� 5      $��* �6+    5      �� 5      $
��Auto �6(   �� 5      $
��Auto �6(   .+�5!      �� $��Author Name:��6    $��0��64   $��0��6A   $��	0,0,10,10q�6Q   $��Center=�6d   �� 5"      -   5        $��txtAuthorName��5"   *   $��25��6   $��0��6?   $��1��6L   $��0,0,0,10q�6\   5#      5$      5%      �� 5&   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   $��0,20,0,0q�6W   .+�5'      �� -   5        $
��Save��5'      $��80��6$   $��0,0,10,0q�6/   6A   �� 5(      -   5        $��Cancel��5(      $��80��6&   61   5)      5*      5+      