   M S B A M L   `   `   `  ����K  GTS.FW.Wpf, Version=1.0.9127.30919, Culture=neutral, PublicKeyToken=null7 clr-namespace:TS.FW.Wpf.ControlsTS.FW.Wpf.Controls  5      &     TS.FW.Wpf.Controls.CheckButton3D   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:TS.FW.Wpf.Controls     5      -   5          d�Name$  xRoot��5      |�.  $��Right=�6X   ��5	      �� 5
   
    �� 
TargetType" d�6    � 	Resources 5      -  'System.Windows.Media.Effects.BlurEffect   xLayoutEffect   Radius   
KernelType��%9   &	          5      $ 2Q�63   $ Gaussian=�6>   5      .R�5      �� #��z��6   
������ $��0q�6Y    �� CornerRadius 5      �� *CornerRadius+ �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType&    TS.FW.Wpf.Controls.CheckButton3D"  ._�5      � .+�5      �� 	 ��Name$	 xBlack��6   $��0 0 1 1q�6-   
���   �
 J� Effect#
 [  6�    6[   �� *CornerRadius+ �� $ FindAncestor=�"  6�   �� 5      $	 xWhite��6   $��1 1 0 0q�6-   
�������#
 [  6�   $��1q�6   6[   �� *CornerRadius+ �� $ FindAncestor=�"  6  �� 5      #��z��6'    �� ContentStringFormat �� ContentStringFormat# z 5   '   #��z��5   '   #��z��5   '   #��z��5   '   #��z��5   '   #��z��5   '   #��z��5   '   #��z��5   '   #��z��5   '   ��5   '   �� *ContentMargin+ �� $ FindAncestor=�"  5   '   �� 5!       ��Name$ xCheck��6    #��z��5"       $
��Left=�6D   $��5q�6_    J� IsHitTestVisible .  $y�	Collapsed=�6�   ��5!   0   �� *
IconSize+ �� $ FindAncestor=�"   �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� J� OpacityMask 5#      !� 5$       j� Stretch$ Uniform=�6*    !� Visual 5%   "   �� 5&   &   $��76��6-   $��76��68    J� Clip4 ��        � ��    � �� ���     ���        .+�5'   *   F� $��34.2877��6/   $��29.4585��6?    �� Left$ 22��6P   
 �� Top$ 24.5415��6a    �� Stretch$
 Fill=�6v   
���   �����p��fffff�@@�,�UX=cь%(<yv���Zl+�Ѩ�[���ќ�Զ1Q��fffff�@@���O@x}р��@x}�`F���������������@��р�@��р�����2
 �,��2
��7р�O��7р�O@x}6�   5(   '   5*      5,      �� 5-      $ xUncheck��6    #��z��5.       $
��Left=�6D   $��5q�6_    .  ��5-   2   �� *
IconSize+ �� $ FindAncestor=�"  $ TwoWay=�$ PropertyChanged=� 5/      !� 50      $ Uniform=�6*    51   "   �� 52   &   $��76��6-   $��76��68   4 ��        � ��    � �� ���     ���        .+�53   *   F� $��30��6/   $��30��6:   $ 23��6F   $ 23��6W   $
 Fill=�6g   
���   �a������^��^�@�(��^�@�(@�(���^@�(���^��^� ?� ?�� ?� l�� l� l�� l� ?�� ?� ?�6�   54   '   56      58      59      5:       �� Triggers 5<      P� 5=       P� Property �@d�	IsChecked P� Value$
 TrueY�6/   . �5>      ��  �� 
TargetName$ xCheck��6    �� Property � y� �� Value$ Visible=�6G   �� 5?      $ xUncheck��6    � y�$ 	Collapsed=�6I   5@      P� 5A       � �� . . �5B      �� $ xBlack��6    � ��
 �����6H   �� 5C      $ xWhite��6    � ��
 �   �6H   5D      P� 5E       �@J�IsMouseOver . . �5F      ��  �@�Cursor$
 Hand��6/   5G      5I      5K      