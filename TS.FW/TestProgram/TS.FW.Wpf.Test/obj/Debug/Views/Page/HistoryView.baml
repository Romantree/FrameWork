   M S B A M L   `   `   `  ����K  GTS.FW.Wpf, Version=1.0.9127.30920, Culture=neutral, PublicKeyToken=nullJ3clr-namespace:TS.FW.Wpf.Controls;assembly=TS.FW.WpfTS.FW.Wpf.Controls  5      I ETS.FW.Wpf.Test, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:TS.FW.Wpf.Test.Views.PageTS.FW.Wpf.Test.Views.Page 5      +   %TS.FW.Wpf.Test.Views.Page.HistoryView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Cctr3clr-namespace:TS.FW.Wpf.Controls;assembly=TS.FW.Wpf     5      9local'clr-namespace:TS.FW.Wpf.Test.Views.Page     5      .��5
      �   � ColumnDefinitions  5   
   �� 5      �� 5      $	��200 �6   .+�5   
   !   TS.FW.Wpf.Controls.MenuView  $��0��6      ItemsSource 6'   �� *
MenuList+!   TS.FW.Wpf.Controls.MenuList  5   
   $��1��6      Orientation$ Vertical=�6n   
�������   ItemsSource 6'   �� *
MenuList+   SelectedMenu 6H   �� *SelectedMenu+   	MenuStyle 5      �� 5       �� 
TargetType'   !TS.FW.Wpf.Controls.ToggleButton3D"  6   .�5      ��  �� Property � �� �� Value$
 Auto��6.   �� 5       � ��$ 35��6/   �� 5       � ��$ 2q�6/   �� 5       � ��$ 18(�61   �� 5       � ��$
 Bold"�63   �� 5       �@ 
IconHeight$ 60Q�63   �� 5       �@ 
IconMargin$ 20q�63   �� 5       � ��
 �iii�63   �� 5       � �� 63   �� *
IsSelect+   xOnOffColorConverter	 �� 	Converter#	 [  
 �� ConverterParameter$

 MENU��5      5!      �� 5$   
   -   5        ��5$      �� *List+ �� SelectedItem 60   �� *
Selected+6R   !   TS.FW.Wpf.Controls.Button3D  5&   
   $��Right=�6   $	��Top=�63   $��50��6K   $	��200��6W   $
��TEST��6w   ��6c   �� 6w   5'      5(      