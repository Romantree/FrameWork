   M S B A M L   `   `   `  ����K  GTS.FW.Wpf, Version=1.0.9127.30919, Culture=neutral, PublicKeyToken=null7 clr-namespace:TS.FW.Wpf.ControlsTS.FW.Wpf.Controls  5   	   =#clr-namespace:TS.FW.Wpf.Controls.PuTS.FW.Wpf.Controls.Pu  5   	   "    TS.FW.Wpf.Controls.Pu.MsgBox   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   0ctr clr-namespace:TS.FW.Wpf.Controls     5   	   5local#clr-namespace:TS.FW.Wpf.Controls.Pu     5   	   -   5          �Name$  xRoot��5	   	   $	��450��6    � SizeToContent$ Height=�6$   |�.  � WindowStyle$
 None=�5
   	    � WindowState$ Normal=�6    � 
ResizeMode$ NoResize=�61    � ShowInTaskbar .   � Topmost . 
������  � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=�5   K   	 � 	Resources	 5         xBackground
 �� Color  xTitleBackground �� EndPoint �� 
StartPoint � Color � Offset  xErrorBackground  xValueBackground  xBtnBackground �� 	Transform �� Angle �� CenterX �� CenterY!   TS.FW.Wpf.Controls.Button3D �� 
TargetType �� Property �� Value��%�  &	        &	 #     &	 �     &	 �    &	     '  B    �� 5   
   $
 	#FF808088��6.   �� 5   
   $ 0.5,1&�67   $ 0.5,0&�6H   .��5      � $ #415369��6   $ 0Q�6+   � 5      $ #39495C��6   $	 0.5Q�6+   � 5      $ #293442��6   $ 1Q�6+   5      �� 5   
   $ 0.5,1&�67   $ 0.5,0&�6H   .��5      � $ #FF0000��6   $ 0Q�6+   � 5      $ #D92000��6   $	 0.5Q�6+   � 5      $ #C93442��6   $ 1Q�6+   5      
� 5   
   .��5      � $ 	#FFD1CCCC��6   $ 1Q�6-   � 5      $ White��6   5      �� 5   
   $ 0.5,1&�65   $ 0.5,0&�6F    5      V� 5      .~�5      �� �� 5       �� 5!      $ -5Q�6&   $ 0Q�61   $ 0Q�6=   T� 5"      5#      .��5%      � $ 	#FFD6D6D6��6   $
 -0.4Q�6-   � 5&      $ 	#FF535353��6   $ 1Q�6-   5'      �� 5(   
   "  6   .�5)      ��  � ��$	 100��6&   �� 5*       �@ CornerRadius$ 5��6-   �� 5+       � ��$ 15(�6)   �� 5,       � ��$
 Bold"�6+   �� 5-       � ��$ 2q�6'   �� 5.       � ��
 �����6+   5/      50      .��52      !   TS.FW.Wpf.Controls.Border3D     CornerRadius$ 5��6   #��[  6$   .��53   
   � $��2q�6    � RowDefinitions 54      �� 55      $��30 �6    �� 56      $	��120��6    �� 57      $��40 �6    .+�5:          TS.FW.Wpf.Controls.Label3D      TS.FW.Wpf.Controls.Label3D   	MouseMove$ Label3D_MouseMove��5;      $��0��5:      $��Message Box��6'   $��15(�6=   $
��Bold"�6K   
�������   CornerRadius$ 5��6�   ��6]   �� *
TitleColor+5;        5>      $��1��6      	IsPressed . $��2q�69   $ 5��6D   #��[ 6U   
���   �$
��Bold"�6�   $��15(�6�   .��5?      �� $��Disabled=�6   $
��Auto=�6H   $��5q�6k   .��5@      �� $
��Wrap=�6    ��64   �� *	Message+5A      5B      �� 5D      $��2��6    �� Orientation$ 
Horizontal=�6&   $��Right=�6?   y�6[   �� *YesNo+.+�5E        !   TS.FW.Wpf.Controls.Button3D   Click$ Button3D_Click��6@   $	��YES��6   
��� � �6@     5F      $ Button3D_Click��6=   $��NO��6   
���  ��6=   5G        5I      $ Button3D_Click��6�   $��2��6   $��OK��6(   #��[ 65   $��Right=�6b   y�6~   �� *Ok+6�   5J      5K      5L      