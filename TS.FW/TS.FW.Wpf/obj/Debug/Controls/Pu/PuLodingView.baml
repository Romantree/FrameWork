   M S B A M L   `   `   `  ����K  GTS.FW.Wpf, Version=1.0.9127.30919, Culture=neutral, PublicKeyToken=nullF1clr-namespace:TS.FW.Wpf.Helper;assembly=TS.FW.WpfTS.FW.Wpf.Helper  5   	   7 clr-namespace:TS.FW.Wpf.ControlsTS.FW.Wpf.Controls  5   	   =#clr-namespace:TS.FW.Wpf.Controls.PuTS.FW.Wpf.Controls.Pu  5   	   (    "TS.FW.Wpf.Controls.Pu.PuLodingView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   ?h1clr-namespace:TS.FW.Wpf.Helper;assembly=TS.FW.Wpf     5   	   0ctr clr-namespace:TS.FW.Wpf.Controls     5   	   5local#clr-namespace:TS.FW.Wpf.Controls.Pu     5   	     � WindowStyle$
  None=�5
   	    � WindowState$ Normal=�6    � 
ResizeMode$ NoResize=�61    � ShowInTaskbar .   � Topmost . $	��110��5   	   $	��500��6    � AllowsTransparency . 
������  � 	Resources 5         
xMainColor � Color � Offset  xProgressBar	 �� 
TargetType
 ��Name �� CornerRadius��%I  &	        &	 i     
� 5   
   .��5      � $ 	#FFD1CCCC��6   $ 1Q�6-   � 5      $ White��6   5      �� 5   
   "	 �6/   .R�5      � .+�5      �� $
 
PART_Track��6   $ 5��6-   #��z��6>   �� 5      $
 PART_Indicator��6   $ 5��61   #��z��6B   $
��Left=�6l   5      5      5      .��5      !   TS.FW.Wpf.Controls.Border3D  #��[  6      CornerRadius$ 5��6<   .��5   
   �  � RowDefinitions 5      �� 5      �� 5      $��45 �6    .+�5!      � $��
10 10 10 5q�6    5"      �� 5#      $��20 �6$   �� 5$      .+�5'      �� $
��Bold"�63   $��12(�6E   
�������$��Bottom=�6e   ��6   �� *Title+6e   �� 5)      $��1��6    �� Orientation$ 
Horizontal=�6*   .+�5*      �� $��20(�6L   $
��Bold"�6Z   ���	#FF0E54EE$��Center=�6�   ��6    �� *Value+ �� StringFormat$
 {0}%��6�   �� 5+      $��to��6    $��20(�6*   $
��Bold"�68   ���	#FF0E54EE$	��3 0q�6a   $��Center=�6n   �� 5,      $��20(�69   $
��Bold"�6G   ���	#FF0E54EE$	��3 0q�6p   $��Center=�6}   ��6    �� *	Caption+6}   5-      �� 5/      $��1��6   $ 
Horizontal=�6*   $��Right=�6C   $��Bottom=�6_   .+�50      $   TS.FW.Wpf.Controls.IconControl  $��1��6&      Icon$ Custom=�63     appbar_timer   Visual# [ 6A   $��16��6h   $��16��6s   
�������$	��5 0q�6�   �� 51      $��1��6    $
��Bold"�6�   $��12(�6�   
���������6-   �� %   TS.FW.Wpf.Helper.DateTimeHelper
   Ins �� Source# Z  �� Path$ WatchSec�$ 
{0:f0} Sec�� �� Mode$ OneWay=�6�   52      53        55      $��1��6   $��3q�6(   $ 5��63   
�������.��56      � 
������ ���	#FF1C438E$��0q�6N   $��2q�6b    � Minimum$ 0Q�6m    � Maximum$	 100Q�6y   #��[ 6�    � Value 6�   �� *Value+6�   57      58      59      5:      