   M S B A M L   `   `   `  ����K  GTS.FW.Wpf, Version=1.0.9127.30919, Culture=neutral, PublicKeyToken=null7 clr-namespace:TS.FW.Wpf.ControlsTS.FW.Wpf.Controls  5   	   &     TS.FW.Wpf.Controls.ComboButton3D   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   2local clr-namespace:TS.FW.Wpf.Controls     5   	   -   5          ��Name$  xRoot��5   	   |�.  ��5	      �� 5
   
    �� 
TargetType" ��6    � 	Resources 5      -  'System.Windows.Media.Effects.BlurEffect   xLayoutEffect   Radius   
KernelType��%9   &	          5      $ 2Q�63   $ Gaussian=�6>   5      .R�5      �� #��z��6   
������ $��0q�6Y    �� CornerRadius 5      �� *CornerRadius+ �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType&    TS.FW.Wpf.Controls.ComboButton3D"  ._�5      � .+�5      �� 	 ��Name$	 	xComboBox��6   
 J� Opacity$
 0Q�62    �� 
IsEditable .  $��Center=�6Q   $��Center=�6n   #��z��6�   #��z��6�    J� IsHitTestVisible .  #��z��5      #��z��6E   ��5      �� *ItemsSource+ �� $ FindAncestor=�"   �� SelectedValue 5      �� *	Content+ �� $ FindAncestor=�"   �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=���5      �� *ItemContainerStyle+ �� $ FindAncestor=�"  �� 5       ��Name$ xBlack��6   $��0 0 1 1q�6-   
���   � J� Effect# [  6�    6[   �� *CornerRadius+ �� $ FindAncestor=�"  6�   �� 5      $ xWhite��6   $��1 1 0 0q�6-   
�������# [  6�   $��1q�6   6[   �� *CornerRadius+ �� $ FindAncestor=�"  6  �� 5       �� ContentStringFormat �� ContentStringFormat# z 5   '   #��z��5   '   #��z��5    '   #��z��5!   '   #��z��5"   '   #��z��5#   '   #��z��5$   '   #��z��5%   '   #��z��5&   '   ��5   '   �� *	Content+ �� $ FindAncestor=�"  $ TwoWay=�$ PropertyChanged=���5   '   �� *ContentMargin+ �� $ FindAncestor=�"  5&   '   �� 5(      #��z��6�   $��Right=�6�   $��5q�6   .  ��6    �� *
IconSize+ �� $ FindAncestor=�"  $ TwoWay=�$ PropertyChanged=� J� OpacityMask 5)      !� 5*       j� Stretch$ Uniform=�6*    !� Visual 5+   "   �� 5,   &   $��76��6-   $��76��68    J� Clip4 ��        � ��    � �� ���     ���        .+�5-   *   F� $
��26.5��6/   $
��30.5��6<    �� Left$ 24.75��6J   
 �� Top$ 22.75��6^    �� Stretch$
 Fill=�6q   
���   �O����P Zbр�CЇ,Ѱ�y Zb�P Zb�P Q%Ѱ�y Q%р�C0#[�P Q%6�   5.   '   50      52      53      54       �� Triggers 56      P� 57       P� Property � �� P� Value . . �58      ��  �� 
TargetName$ xBlack��6    �� Property � ��  �� Value
  �����6H   �� 59      $ xWhite��6    � ��
  �   �6H   �� 5:       �@J�Opacity$	  0.8Q�60   5;      P� 5<       �@J�IsMouseOver . . �5=      ��  �@�Cursor$
  Hand��6/   �� 5>       �@J�Opacity$	  0.9Q�60   5?      P� 5@       � {� .  . �5A      ��  �@J�Opacity$	  0.5Q�60   5B      5D      5F      