   M S B A M L   `   `   `  ����K  GTS.FW.Wpf, Version=1.0.9127.30920, Culture=neutral, PublicKeyToken=nullJ3clr-namespace:TS.FW.Wpf.Controls;assembly=TS.FW.WpfTS.FW.Wpf.Controls  5   	   I ETS.FW.Wpf.Test, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:TS.FW.Wpf.Test.ViewModelsTS.FW.Wpf.Test.ViewModels 5   	   ;"clr-namespace:TS.FW.Wpf.Test.ViewsTS.FW.Wpf.Test.Views 5   	   $   TS.FW.Wpf.Test.Views.MainView2   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Cctr3clr-namespace:TS.FW.Wpf.Controls;assembly=TS.FW.Wpf     5   	   6vm'clr-namespace:TS.FW.Wpf.Test.ViewModels     5   	   4local"clr-namespace:TS.FW.Wpf.Test.Views     5   	      xMainViewModel  � DataContext#  �   5	       � Title$ 	MainView2��5
   	   $	��450��6   $	��800��6(    � 	Resources 5      .  (TS.FW.Wpf.Test.ViewModels.MainViewModel2��%   &	          5   
   6   5      .��5      �  � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   �� .��5      �� ��6   �� *
TestList+��5      �� 5      .R�5      !   TS.FW.Wpf.Controls.Button3D  $��30��6'   $��5q�63   5      5       5!      � 5#   
   $��1��6    � RowDefinitions 5$      �� 5%      $��50 �6    �� 5&      $��50 �6    �� 5'      $��50 �6    �� 5(      .+�5+        $��0��6   $��등록��6(   $	��ADD��6I      Icon$
 Save=�6`   ��65   �� 6`     5,      $��1��6   $��삭제��6(   $	��RED��6I   ��65   �� 6I     5.      $��2��6   $��
등록 100��6(   $��ADD_100��6M   ��69   �� 6M   5/      �� 51   
   $��2��6   .��52      �� ��6   �� *	TestList2+��53      �� 54      .R�55          TS.FW.Wpf.Controls.Label3D  $��30��6&   $��5q�62   56      58      59      � 5;   
   $��3��6    5<      �� 5=      $��50 �6    �� 5>      $��50 �6    �� 5?      $��50 �6    �� 5@      .+�5C        $��0��6   $��등록��6(   $
��2ADD��6I   ��65   �� 6I     5D      $��1��6   $��삭제��6(   $
��2RED��6I   ��65   �� 6I     5F      $��2��6   $��
등록 100��6(   $��2ADD_100��6M   ��69   �� 6M   5G      �� 5I   
   $��4��6   .��5J      �� ��6   �� *	TestList3+��5K      �� 5L      .R�5M      �� $��30��6!   $��5q�6-   5N      5P      5Q      � 5S   
   $��5��6    5T      �� 5U      $��50 �6    �� 5V      $��50 �6    �� 5W      $��50 �6    �� 5X      .+�5[        $��0��6   $��등록��6(   $
��3ADD��6I   ��65   �� 6I     5\      $��1��6   $��삭제��6(   $
��3RED��6I   ��65   �� 6I     5^      $��2��6   $��
등록 100��6(   $��3ADD_100��6M   ��69   �� 6M   5_      5`      5a      