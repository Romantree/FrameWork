   M S B A M L   `   `   `  ����K  GTS.FW.Wpf, Version=1.0.9127.30920, Culture=neutral, PublicKeyToken=nullJ3clr-namespace:TS.FW.Wpf.Controls;assembly=TS.FW.WpfTS.FW.Wpf.Controls  5   	   I ETS.FW.Wpf.Test, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:TS.FW.Wpf.Test.ViewModelsTS.FW.Wpf.Test.ViewModels 5   	   F1clr-namespace:TS.FW.Wpf.Helper;assembly=TS.FW.WpfTS.FW.Wpf.Helper  5   	   ;"clr-namespace:TS.FW.Wpf.Test.ViewsTS.FW.Wpf.Test.Views 5   	   #   TS.FW.Wpf.Test.Views.MainView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Cctr3clr-namespace:TS.FW.Wpf.Controls;assembly=TS.FW.Wpf     5   	   6vm'clr-namespace:TS.FW.Wpf.Test.ViewModels     5   	   ?h1clr-namespace:TS.FW.Wpf.Helper;assembly=TS.FW.Wpf     5   	   4local"clr-namespace:TS.FW.Wpf.Test.Views     5   	      xMainViewModel  � DataContext#  �   5
        xMainWindow#��[ 5   	    � 	Resources 5      -  'TS.FW.Wpf.Test.ViewModels.MainViewModel��%   &	          5   
   6   5      .��5      �  � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      �� 5      $��80 �6   .+�5   
       TS.FW.Wpf.Controls.Label3D  -   5        $��0��5      $��WPF Control Test Program��6#   $��15(�6F   $
��Bold"�6T   
���iii�
�������6�   !   TS.FW.Wpf.Controls.MenuView  5   
   $��1��6      ItemsSource 6$   �� *
MenuList+!   TS.FW.Wpf.Controls.MenuList  5   
   $��2��6      Orientation$ 
Horizontal=�6k   
�������   ItemsSource 6$   �� *
MenuList+   SelectedMenu 6E   �� *SelectedMenu+   	MenuStyle 5      �� 5       �� 
TargetType'   !TS.FW.Wpf.Controls.ToggleButton3D"  6   .�5      �� 	 �� Property	 � ��
 �� Value$	
 250��6.   �� 5      	 � ��$

 Auto��6/   �� 5       	 � ��$
 5q�6/   �� 5!      	 � ��$
 18(�61   �� 5"      	 � ��$

 Bold"�63   �� 5#      	 �@ 
IconHeight$
 60Q�63   �� 5$      	 �@ 
IconMargin$
 20q�63   �� 5%      	 � ��

 �iii�63   �� 5&      	 � ��
 63   �� *
IsSelect+  xOnOffColorConverter �� 	Converter# [  �� ConverterParameter$
 MENU��5'      5)      5*      5+      